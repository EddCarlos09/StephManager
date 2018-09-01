using CreativaSL.Dll.StephManager.Global;
using CreativaSL.Dll.StephManager.Negocio;
using Microsoft.Office.Interop.Excel;
using StephManager.ClasesAux;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace StephManager
{
    public partial class frmInventario : Form
    {

        Producto infoProducto = new Producto();
        #region Constructor

        public frmInventario()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                LogError.AddExcFileTxt(ex, "frmInventario ~ frmInventario()");
            }
        }

        #endregion

        #region Métodos

        private System.Data.DataTable BusquedaResultados(Producto Datos)
        {
            try
            {
                Producto_Negocio ProdNeg = new Producto_Negocio();
                ProdNeg.ObtenerProductosInventario(Datos);
                return Datos.TablaDatos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarComboSucursales()
        {
            try
            {
                Sucursal DatosSuc = new Sucursal { Conexion = Comun.Conexion, Opcion = 1 };
                Sucursal_Negocio SucNeg = new Sucursal_Negocio();
                this.cmbSucursales.DataSource = SucNeg.LlenarComboCatSucursales(DatosSuc);
                this.cmbSucursales.ValueMember = "IDSucursal";
                this.cmbSucursales.DisplayMember = "NombreSucursal";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<Producto> GenerarLista()
        {
            try
            {
                List<Producto> Lista = new List<Producto>();
                Producto Item;
                foreach (DataGridViewRow Fila in this.dgvProductos.Rows)
                {
                    Item = new Producto();
                    Item.Clave = Fila.Cells["Clave"].Value.ToString();
                    Item.NombreProducto = Fila.Cells["NombreProducto"].Value.ToString();
                    Item.NombreSucursal = Fila.Cells["NombreSucursal"].Value.ToString();
                    Item.Existencia = Convert.ToDecimal(Fila.Cells["Existencia"].Value.ToString());
                    Item.StockMinimo = Convert.ToDecimal(Fila.Cells["StockMinimo"].Value.ToString());
                    Item.StockMaximo = Convert.ToDecimal(Fila.Cells["StockMaximo"].Value.ToString());
                    Item.RequiereStock = Convert.ToBoolean(Fila.Cells["RequiereStock"].Value.ToString());
                    Lista.Add(Item);
                }
                return Lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void IniciarForm()
        {
            try
            {
                this.CargarComboSucursales();
                this.ActiveControl = this.cmbSucursales;
                this.cmbSucursales.Focus();
                if (File.Exists(Path.Combine(System.Windows.Forms.Application.StartupPath, @"Resources\Documents\" + Comun.UrlLogo)))
                {
                    this.pictureBox1.Image = Image.FromFile(Path.Combine(System.Windows.Forms.Application.StartupPath, @"Resources\Documents\" + Comun.UrlLogo));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LlenarTablaProductos(System.Data.DataTable Datos)
        {
            try
            {
                this.dgvProductos.AutoGenerateColumns = false;
                this.dgvProductos.DataSource = Datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ModificarDatos(int Row, Producto Datos)
        {
            try
            {
                //this.dgvProductos.Rows[Row].Cells["Existencia"].Value = Datos.Existencia;
                this.dgvProductos.Rows[Row].Cells["RequiereStock"].Value = Datos.RequiereStock;
                this.dgvProductos.Rows[Row].Cells["StockMaximo"].Value = Datos.StockMaximo;
                this.dgvProductos.Rows[Row].Cells["StockMinimo"].Value = Datos.StockMinimo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ModificarExistencia(int Row, Producto Datos)
        {
            try
            {
                this.dgvProductos.Rows[Row].Cells["Existencia"].Value = Datos.Existencia;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Producto ObtenerDatosProducto(int Row)
        {
            try
            {
                Producto DatosAux = new Producto();
                decimal Existencia = 0, StockMinimo = 0, StockMaximo = 0;
                bool RequiereStock = false;
                if (this.dgvProductos.Rows.Count > 0 && Row < this.dgvProductos.Rows.Count)
                {
                    DataGridViewRow Fila = this.dgvProductos.Rows[Row];
                    DatosAux.IDProducto = Fila.Cells["IDProducto"].Value.ToString();
                    DatosAux.IDSucursal = Fila.Cells["IDSucursal"].Value.ToString();
                    DatosAux.NombreProducto = Fila.Cells["NombreProducto"].Value.ToString();
                    DatosAux.NombreSucursal = Fila.Cells["NombreSucursal"].Value.ToString();
                    bool.TryParse(Fila.Cells["RequiereStock"].Value.ToString(), out RequiereStock);
                    decimal.TryParse(Fila.Cells["Existencia"].Value.ToString(), out Existencia);
                    decimal.TryParse(Fila.Cells["StockMinimo"].Value.ToString(), out StockMinimo);
                    decimal.TryParse(Fila.Cells["StockMaximo"].Value.ToString(), out StockMaximo);
                    DatosAux.RequiereStock = RequiereStock;
                    DatosAux.Existencia = Existencia;
                    DatosAux.StockMinimo = StockMinimo;
                    DatosAux.StockMaximo = StockMaximo;
                }
                return DatosAux;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Sucursal ObtenerSucursalCombo()
        {
            try
            {
                Sucursal DatosAux = new Sucursal();
                if (this.cmbSucursales.Items.Count > 0)
                {
                    if (this.cmbSucursales.SelectedIndex != -1)
                    {
                        DatosAux = (Sucursal)this.cmbSucursales.SelectedItem;
                    }
                }
                return DatosAux;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Eventos

        private void btnAjuste_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dgvProductos.SelectedRows.Count == 1)
                {
                    int RowSelected = this.dgvProductos.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                    Producto DatosAux = this.ObtenerDatosProducto(RowSelected);
                    if (!string.IsNullOrEmpty(DatosAux.IDSucursal))
                    {
                        if (!string.IsNullOrEmpty(DatosAux.IDProducto))
                        {
                            frmInventarioAjuste Ajuste = new frmInventarioAjuste(DatosAux);
                            Ajuste.ShowDialog();
                            Ajuste.Dispose();
                            if (Ajuste.DialogResult == DialogResult.OK)
                            {
                                this.ModificarExistencia(RowSelected, Ajuste.DatosActuales);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Seleccione un registro", Comun.Sistema, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Seleccione una sucursal", Comun.Sistema, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Seleccione un registro", Comun.Sistema, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                LogError.AddExcFileTxt(ex, "frmInventario ~ btnAjuste_Click");
                MessageBox.Show(Comun.MensajeError, Comun.Sistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.cmbSucursales.SelectedIndex != -1)
                {
                    Sucursal DatosSuc = this.ObtenerSucursalCombo();
                    if (!string.IsNullOrEmpty(DatosSuc.IDSucursal))
                    {
                        Producto DatosProd = new Producto { Conexion = Comun.Conexion, IDSucursal = DatosSuc.IDSucursal, NombreProducto = this.txtBusqueda.Text.Trim() };
                        this.LlenarTablaProductos(this.BusquedaResultados(DatosProd));
                    }
                    else
                    {
                        MessageBox.Show("Seleccione una sucursal", Comun.Sistema, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }                    
                }
                else
                {
                    MessageBox.Show("Seleccione una sucursal", Comun.Sistema, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                LogError.AddExcFileTxt(ex, "frmInventario ~ btnBuscar_Click");
                MessageBox.Show(Comun.MensajeError, Comun.Sistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dgvProductos.Rows.Count > 0)
                {
                    PrintDialog Aux = new PrintDialog();
                    if (Aux.ShowDialog(this) == DialogResult.OK)
                    {
                        ImpresionReporte ImpRep = new ImpresionReporte();
                        ImpRep.Run(@"..\..\Informes\ExistenciaProductos.rdlc", this.GenerarLista(), Aux.PrinterSettings);
                    }
                }
                else
                {
                    MessageBox.Show("No hay registros para impresión.", Comun.Sistema, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                LogError.AddExcFileTxt(ex, "frmInventario ~ btnImprimir_Click");
                MessageBox.Show(Comun.MensajeError, Comun.Sistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModificarDatos_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dgvProductos.SelectedRows.Count == 1)
                {
                    int RowSelected = this.dgvProductos.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                    Producto DatosAux = this.ObtenerDatosProducto(RowSelected);
                    if (!string.IsNullOrEmpty(DatosAux.IDSucursal))
                    {
                        if (!string.IsNullOrEmpty(DatosAux.IDProducto))
                        {
                            frmInventarioCambiar Modificar = new frmInventarioCambiar(DatosAux);
                            Modificar.ShowDialog();
                            Modificar.Dispose();
                            if (Modificar.DialogResult == DialogResult.OK)
                            {
                                this.ModificarDatos(RowSelected, Modificar.DatosActuales);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Seleccione un registro", Comun.Sistema, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Seleccione una sucursal", Comun.Sistema, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Seleccione un registro", Comun.Sistema, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                LogError.AddExcFileTxt(ex, "frmInventario ~ btnModificarDatos_Click");
                MessageBox.Show(Comun.MensajeError, Comun.Sistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.Cancel;
            }
            catch (Exception ex)
            {
                LogError.AddExcFileTxt(ex, "frmInventario ~ btnSalir_Click");
                MessageBox.Show(Comun.MensajeError, Comun.Sistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTranferencia_Click(object sender, EventArgs e)
        {
            try
            {
                frmInventarioTransferencia Transfer = new frmInventarioTransferencia();
                Transfer.ShowDialog();
                Transfer.Dispose();
                if (Transfer.DialogResult == DialogResult.OK)
                {
                    this.btnBuscar_Click(this.btnBuscar, new EventArgs());
                }
            }
            catch (Exception ex)
            {
                LogError.AddExcFileTxt(ex, "frmInventario ~ btnAjuste_Click");
                MessageBox.Show(Comun.MensajeError, Comun.Sistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    Producto DatosAux = this.ObtenerDatosProducto(e.RowIndex);
                    if (!string.IsNullOrEmpty(DatosAux.IDSucursal))
                    {
                        if (!string.IsNullOrEmpty(DatosAux.IDProducto))
                        {
                            frmInventarioCambiar Modificar = new frmInventarioCambiar(DatosAux);
                            Modificar.ShowDialog();
                            Modificar.Dispose();
                            if (Modificar.DialogResult == DialogResult.OK)
                            {
                                this.ModificarDatos(e.RowIndex, Modificar.DatosActuales);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Seleccione un registro", Comun.Sistema, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogError.AddExcFileTxt(ex, "frmInventario ~ dgvProductos_CellDoubleClick");
            }
        }

        private void frmInventario_Load(object sender, EventArgs e)
        {
            try
            {
                this.IniciarForm();
            }
            catch (Exception ex)
            {
                LogError.AddExcFileTxt(ex, "frmInventario ~ frmInventario_Load");
            }
        }

        #endregion

        private void btnLeerInventario_Click(object sender, EventArgs e)
        {
            try
            {
                this.ImportarExcel();
            }
                
            catch (Exception ex)
            {
                LogError.AddExcFileTxt(ex, "frmInventario ~ btnLeerInventario_Click");
                MessageBox.Show(Comun.MensajeError, Comun.Sistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /*private void LeerDocumentoExcel(string Archivo, string Hoja)
        {
            try
            {
                OleDbConnection conexion = null;
                DataSet dataSet = null;
                OleDbDataAdapter dataAdapter = null;
                string consultaHojaExcel = "Select * from [" + Hoja + "$]";
                //esta cadena es para archivos excel 2007 y 2010
                string cadenaConexionArchivoExcel = "provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + Archivo + "';Extended Properties=Excel 12.0;";
                //para archivos de 97-2003 usar la siguiente cadena
                //string cadenaConexionArchivoExcel = "provider=Microsoft.Jet.OLEDB.4.0;Data Source='" + archivo + "';Extended Properties=Excel 8.0;";
                //Validamos que el usuario ingrese el nombre de la hoja del archivo de excel a leer
                if (!string.IsNullOrEmpty(Hoja))
                {
                    try
                    {
                        //Si el usuario escribio el nombre de la hoja se procedera con la busqueda
                        conexion = new OleDbConnection(cadenaConexionArchivoExcel);//creamos la conexion con la hoja de excel
                        conexion.Open(); //abrimos la conexion
                        dataAdapter = new OleDbDataAdapter(consultaHojaExcel, conexion); //traemos los datos de la hoja y las guardamos en un dataSdapter
                        dataSet = new DataSet(); // creamos la instancia del objeto DataSet
                        dataAdapter.Fill(dataSet, Hoja);//llenamos el dataset
                        //dataGridView1.DataSource = dataSet.Tables[0]; //le asignamos al DataGridView el contenido del dataSet
                        if (dataSet.Tables.Count > 0)
                        {
                            System.Data.DataTable Aux = dataSet.Tables[0];
                        }
                        conexion.Close();//cerramos la conexion
                        //dataGridView1.AllowUserToAddRows = false;       //eliminamos la ultima fila del datagridview que se autoagrega
                    }
                    catch (Exception ex)
                    {
                        //en caso de haber una excepcion que nos mande un mensaje de error
                        MessageBox.Show("Error, Verificar el archivo o el nombre de la hoja", ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LeerDocumentoExcelInterOp(string Archivo, string Hoja)
        {
            try
            {
                Workbook exlWbook;
                Worksheet exlWsheet;
                var ExcelApp = new Excel.Application();
                exlWbook = ExcelApp.Workbooks.Open(Archivo, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                exlWsheet = (Worksheet)exlWbook.Worksheets.get_Item(1);
                Range exlRange;
                string sValor;
                exlRange = exlWsheet.UsedRange;
                System.Data.DataTable TablaInventario = new System.Data.DataTable();
                TablaInventario.Columns.Add("Clave", typeof(string));
                TablaInventario.Columns.Add("Conteo", typeof(decimal));
                for (int i = 1; i <= exlRange.Rows.Count; i++)
                {
                    sValor = "";
                    sValor += " " + (exlRange.Cells[i, 0] as Range).Value + "";

                    object[] Fila  = { (exlRange.Cells[i, 0] as Range).Value, (exlRange.Cells[i, 3] as Range).Value};
                    TablaInventario.Rows.Add(Fila);
                }
                exlWbook.Close();
                ExcelApp.Quit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void DescargarPlantilla()
        {
            try
            {
                var ExcelApp = new Excel.Application();
                ExcelApp.Visible = true;
                ExcelApp.Workbooks.Add();
                Excel._Worksheet WorkSheet = (Excel.Worksheet)ExcelApp.ActiveSheet;
                WorkSheet.Name = "Inventario";
                WorkSheet.Cells[1, "A"] = "Clave";
                WorkSheet.Cells[1, "B"] = "Producto";
                WorkSheet.Cells[1, "C"] = "Existencia";
                WorkSheet.Cells[1, "D"] = "Conteo Físico";
                WorkSheet.Cells[1, "E"] = "Diferencia";
                WorkSheet.get_Range("A1", "E1").Font.Bold = true;
                WorkSheet.get_Range("A1", "E1").Font.Color = Color.DarkRed;
                WorkSheet.get_Range("A1", "E1").Font.Size = 12;
                int RowIndex = 1;
                List<Producto> ListaProductos = this.GenerarListaMatriz();
                WorkSheet.get_Range("A" + RowIndex, "A" + ListaProductos.Count + RowIndex).NumberFormat = "@";
                foreach (Producto Item in ListaProductos)
                {
                    RowIndex++;
                    WorkSheet.Cells[RowIndex, "A"] = Item.Clave;
                    WorkSheet.Cells[RowIndex, "B"] = Item.NombreProducto;
                    WorkSheet.Cells[RowIndex, "C"] = Item.Existencia;
                    WorkSheet.Cells[RowIndex, "D"] = 0;
                    string Formula = "=R[0]C[-2] - R[0]C[-1]";
                    WorkSheet.get_Range("E" + RowIndex).FormulaR1C1 = Formula;
                    WorkSheet.get_Range("E" + RowIndex).FormulaHidden = true;
                    WorkSheet.get_Range("E" + RowIndex).Calculate();
                }
                WorkSheet.Columns[1].AutoFit();
                WorkSheet.Columns[2].AutoFit();
                WorkSheet.Columns[3].AutoFit();
                WorkSheet.Columns[4].AutoFit();
                WorkSheet.Columns[5].AutoFit();
                try
                {
                    //string Archivo = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\DatosInventario.xlsx";
                    //if (File.Exists(Archivo))
                    //{
                    //    if (MessageBox.Show("El archivo ya existe, ¿está seguro de reemplazarlo?", Comun.Sistema, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    //    {
                    //        File.Delete(Archivo);
                    //        WorkSheet.SaveAs(Archivo, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal);
                    //    }
                    //}
                    //else
                    //    WorkSheet.SaveAs(Archivo, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal);
                }
                finally
                {
                    ExcelApp.Quit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }*/

        private void btnDescargarArchivo_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarExcel())
                {
                    this.ExportarExcel();
                }
            }
            catch (Exception ex)
            {
                LogError.AddExcFileTxt(ex, "frmInventario ~ btnDescargarArchivo_Click");
                MessageBox.Show(Comun.MensajeError, Comun.Sistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<Producto> GenerarListaMatriz()
        {
            try
            {
                Producto_Negocio ProdNeg = new Producto_Negocio();
                Producto Datos = new Producto { Conexion = Comun.Conexion, IDSucursal = Comun.IDSucursalCaja };
                ProdNeg.ObtenerProductosInventarioMatriz(Datos);
                List<Producto> Lista = new List<Producto>();
                Producto Item;
                foreach (DataRow Fila in Datos.TablaDatos.Rows)
                {
                    Item = new Producto();
                    Item.Clave = Fila["Clave"].ToString();
                    Item.NombreProducto = Fila["NombreProducto"].ToString();
                    Item.Existencia = Convert.ToDecimal(Fila["Existencia"].ToString());
                    Item.IDProducto = Fila["IDProducto"].ToString();
                    Lista.Add(Item);
                }
                return Lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
       
        private void ExportarExcel()
        {
            try
            {
                this.btnDescargarArchivo.Enabled = false;
                this.lblMessage.Visible = true;
                this.lblMessage.Text = "Generando Formato. Espere un momento...." + Environment.NewLine;
                bgwFormato.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                this.lblMessage.Visible = false;
                this.lblMessage.Text = string.Empty;
                throw ex;
            }            
        }
        private void ImportarExcel()
        {
            try
            {
                OpenFileDialog openFileDialogExcel = new OpenFileDialog();
                openFileDialogExcel.Filter = "Excel Files|*.xlsx";
                openFileDialogExcel.FileName = "";
                openFileDialogExcel.Title = "Seleccione el archivo excel";
                openFileDialogExcel.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).ToString();
                if (openFileDialogExcel.ShowDialog() == DialogResult.OK)
                {
                    Microsoft.Office.Interop.Excel.Application xlsApp = new Microsoft.Office.Interop.Excel.Application();

                    Workbook xlsBook;
                    Worksheet Inventario;
                    Sheets xlHojas;

                    xlsApp.DisplayAlerts = false;
                    xlsApp.Visible = false;
                    string PathAr = openFileDialogExcel.FileName;
                    xlsBook = xlsApp.Workbooks.Open(PathAr);

                    xlHojas = xlsBook.Sheets;
                    Inventario = (Worksheet)xlHojas["Inventario"];
                    int FilaInicio = 4;

                    infoProducto.ImportarExcel = new System.Data.DataTable();
                    infoProducto.ImportarExcel.Columns.Add("IDProducto", typeof(string));
                    infoProducto.ImportarExcel.Columns.Add("Clave", typeof(string));
                    infoProducto.ImportarExcel.Columns.Add("ConteoFisico", typeof(decimal));


                    while ((Inventario.Cells[FilaInicio, 1] as Microsoft.Office.Interop.Excel.Range).Value2 != null)
                    {
                        string Codigo = "", IDProducto = "";
                        decimal ConteoFisico = 0;
                        IDProducto = (Inventario.Cells[FilaInicio, 6] as Microsoft.Office.Interop.Excel.Range).Value2.ToString();
                        Codigo = (Inventario.Cells[FilaInicio, 1] as Microsoft.Office.Interop.Excel.Range).Value2.ToString();
                        decimal.TryParse((Inventario.Cells[FilaInicio, 4] as Microsoft.Office.Interop.Excel.Range).Value2.ToString(), NumberStyles.Currency, CultureInfo.CurrentCulture, out ConteoFisico);
                        infoProducto.ImportarExcel.Rows.Add(new Object[] { IDProducto, Codigo, ConteoFisico });
                        FilaInicio++;
                    }

                    Producto AuxProducto = new Producto();
                    Producto_Negocio ProdNeg = new Producto_Negocio();
                    AuxProducto.Conexion = Comun.Conexion;
                    AuxProducto.IDSucursal = Comun.IDSucursalCaja;
                    AuxProducto.IDUsuario = Comun.IDUsuario;
                    AuxProducto.ImportarExcel = infoProducto.ImportarExcel;
                    ProdNeg.AInventarioEXCEL(AuxProducto);
                    if (AuxProducto.Completado)
                    {
                        MessageBox.Show("Datos guardados correctamente.", Comun.Sistema, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Datos no se guardaron correctamente. Intente mas tarde", Comun.Sistema, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    xlsBook.Close();
                    xlsApp.Quit();
                    releaseObject(xlHojas);
                    releaseObject(xlsBook);
                    releaseObject(Inventario);
                    releaseObject(xlsApp);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                
            }
        }
        private bool ValidarExcel()
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application xlsApp = new Microsoft.Office.Interop.Excel.Application();
                Workbook xlsBook;
                Sheets xlHojas;

                xlsApp.DisplayAlerts = false;
                xlsApp.Visible = false;
                string PathAr = Path.Combine(System.Windows.Forms.Application.StartupPath, @"Resources\Excel\StepthManager.xlsx");
                xlsBook = xlsApp.Workbooks.Open(PathAr);
                xlHojas = xlsBook.Sheets;
                xlsBook.Close(true);
                xlsApp.Quit();
                releaseObject(xlHojas);
                releaseObject(xlsBook);
                releaseObject(xlsApp);
                return true;
            }
            catch (Exception ex)
            {
                LogError.AddExcFileTxt(ex, "frmInventario ~ ValidarExcel");
                return false;
            }            
        }
        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                LogError.AddExcFileTxt(ex, "frmInventario ~ releaseObject");
                obj = null;
            }
            finally
            {
                GC.Collect();
            }
        }

        #region Proceso descarga de formato

        private void bgwFormato_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                List<Producto> ListaProductos = this.GenerarListaMatriz();
                e.Result = ListaProductos;
            }
            catch (Exception ex)
            {
                LogError.AddExcFileTxt(ex, "frmInventario ~ bgwFormato_DoWork");
                MessageBox.Show(Comun.MensajeError, Comun.Sistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bgwFormato_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                int Avance = e.ProgressPercentage;
                this.lblMessage.Text =  "Generando Formato. Espere un momento....";                
            }
            catch (Exception ex)
            {
                LogError.AddExcFileTxt(ex, "frmInventario ~ bgwFormato_ProgressChanged");
            }
        }

        private void bgwFormato_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Result != null)
                {
                    List<Producto> ListaProductos = (List<Producto>)e.Result;
                    Microsoft.Office.Interop.Excel.Application xlsApp = new Microsoft.Office.Interop.Excel.Application();
                    Workbook xlsBook;
                    Worksheet Inventario;
                    Sheets xlHojas;
                    xlsApp.DisplayAlerts = false;
                    xlsApp.Visible = false;
                    string PathAr = Path.Combine(System.Windows.Forms.Application.StartupPath, @"Resources\Excel\StepthManager.xlsx");
                    xlsBook = xlsApp.Workbooks.Open(PathAr);
                    xlHojas = xlsBook.Sheets;
                    Inventario = (Worksheet)xlHojas["Inventario"];

                    int FilaInicio = 4;
                    if (ListaProductos.Count != 0)
                    {
                        int Items = ListaProductos.Count;
                        foreach (Producto Item in ListaProductos)
                        {
                            Inventario.Cells[FilaInicio, 1] = Item.Clave;
                            Inventario.Cells[FilaInicio, 2] = Item.NombreProducto;
                            Inventario.Cells[FilaInicio, 3] = Item.Existencia;
                            Inventario.Cells[FilaInicio, 4] = 0;
                            Inventario.Cells[FilaInicio, 6] = Item.IDProducto;
                            FilaInicio++;
                        }
                    }

                    SaveFileDialog saveFileDialogExcel = new SaveFileDialog();
                    saveFileDialogExcel.Filter = "Excel Files|*.xlsx";
                    saveFileDialogExcel.FileName = "";
                    saveFileDialogExcel.Title = "Seleccione donde guardar el excel";
                    saveFileDialogExcel.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).ToString();
                    if (saveFileDialogExcel.ShowDialog() == DialogResult.OK)
                    {
                        xlsBook.SaveAs(saveFileDialogExcel.FileName);
                    }

                    xlsBook.Close();
                    xlsApp.Quit();
                    releaseObject(xlHojas);
                    releaseObject(xlsBook);
                    releaseObject(Inventario);
                    releaseObject(xlsApp);
                }
                lblMessage.Visible = false;
                lblMessage.Text = string.Empty;
                this.btnDescargarArchivo.Enabled = true;
            }
            catch (Exception ex)
            {
                lblMessage.Visible = false;
                lblMessage.Text = string.Empty;
                this.btnDescargarArchivo.Enabled = true;
                LogError.AddExcFileTxt(ex, "frmInventario ~ bgwFormato_RunWorkerCompleted");
                MessageBox.Show(Comun.MensajeError, Comun.Sistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}
