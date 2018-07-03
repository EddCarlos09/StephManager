using CreativaSL.Dll.StephManager.Global;
using CreativaSL.Dll.StephManager.Negocio;
using CreativaSL.Dll.Validaciones;
using StephManager.ClasesAux;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StephManager
{
    public partial class frmNuevaNomina : Form
    {

        private int IDTipoNomina = 0;

        #region Constructores

        public frmNuevaNomina(int TipoNomina)
        {
            try
            {
                InitializeComponent();
                IDTipoNomina = TipoNomina;
            }
            catch (Exception ex)
            {
                LogError.AddExcFileTxt(ex, "frmNuevaNomina ~ frmNuevaNomina()");
            }
        }

        #endregion

        #region Métodos

        private void IniciarForm()
        {
            try
            {
                if (File.Exists(Path.Combine(System.Windows.Forms.Application.StartupPath, @"Resources\Documents\" + Comun.UrlLogo)))
                {
                    this.pictureBox1.Image = Image.FromFile(Path.Combine(System.Windows.Forms.Application.StartupPath, @"Resources\Documents\" + Comun.UrlLogo));
                }
                this.LlenarGridUsuario(false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LlenarGridUsuario(bool Band)
        {
            try
            {
                Nomina DatosAux = new Nomina { Conexion = Comun.Conexion, IDTipoNomina = this.IDTipoNomina };
                Nomina_Negocio CN = new Nomina_Negocio();
                CN.ObtenerEmpleadosNomina(DatosAux);
                this.dgvUsuario.AutoGenerateColumns = false;
                this.dgvUsuario.DataSource = DatosAux.TablaDatos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Usuario ObtenerDatosUsuario(int RowData)
        {
            try
            {
                Usuario DatosAux = new Usuario();
                if (RowData > -1) 
                {
                    DataGridViewRow FilaDatos = this.dgvUsuario.Rows[RowData];
                    DatosAux.IDEmpleado = FilaDatos.Cells["IDEmpleado"].Value.ToString();                    
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

        private void btnSalir_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.Abort;
            }
            catch (Exception ex)
            {
                LogError.AddExcFileTxt(ex, "frmNuevaNomina ~ btnSalir_Click");
                MessageBox.Show(Comun.MensajeError, Comun.Sistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmNuevaNomina_Load(object sender, EventArgs e)
        {
            try
            {
                this.IniciarForm();                
            }
            catch (Exception ex)
            {
                LogError.AddExcFileTxt(ex, "frmNuevaNomina ~ frmNuevaNomina_Load");
                MessageBox.Show(Comun.MensajeError, Comun.Sistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        private void dgvUsuario_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    if (this.dgvUsuario.Columns[e.ColumnIndex].Name == "Conceptos")
                    {
                        Usuario Datos = this.ObtenerDatosUsuario(e.RowIndex);
                        frmNominaConceptos Conceptos = new frmNominaConceptos(Datos.IDEmpleado, this.IDTipoNomina);
                        this.Visible = false;
                        Conceptos.ShowDialog();
                        Conceptos.Dispose();
                        this.Visible = true;
                        if (Conceptos.DialogResult == DialogResult.OK)
                        {
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Visible = true;
                LogError.AddExcFileTxt(ex, "frmNuevaNomina ~ dgvUsuario_CellContentClick");
            }
        }

        private void btnGenerarNomina_Click(object sender, EventArgs e)
        {
            try
            {
                frmNominaFechas EstablecerFechas = new frmNominaFechas();
                EstablecerFechas.ShowDialog();
                EstablecerFechas.Dispose();
                if (EstablecerFechas.DialogResult == DialogResult.OK)
                {
                    DateTime FI = EstablecerFechas.FechaInicio;
                    DateTime FF = EstablecerFechas.FechaFin;
                    Nomina Datos = new Nomina { Conexion = Comun.Conexion, IDUsuario = Comun.IDUsuario, FechaInicio = FI, FechaFin = FF, IDTipoNomina = this.IDTipoNomina };
                    Nomina_Negocio NN = new Nomina_Negocio();
                    NN.GenerarNomina(Datos);
                    if (Datos.Completado)
                    {
                        try
                        {
                            frmVerListados Reporte = new frmVerListados(3, Datos.IDNomina);
                            this.Visible = false;
                            Reporte.ShowDialog();
                            Reporte.Dispose();
                            this.Visible = true;
                        }
                        catch (Exception exe)
                        {
                            LogError.AddExcFileTxt(exe, "frmNuevaNomina ~ btnGenerarNomina_Click ~ ReporteNomina");
                            this.Visible = true;
                        }
                        finally
                        {
                            this.DialogResult = DialogResult.OK;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogError.AddExcFileTxt(ex, "frmNuevaNomina ~ btnGenerarNomina_Click");
                MessageBox.Show(Comun.MensajeError, Comun.Sistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }





    }
}
