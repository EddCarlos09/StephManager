﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using CreativaSL.Dll.StephManager.Global;
using CreativaSL.Dll.StephManager.Negocio;
using CreativaSL.Dll.Validaciones;
using StephManager.ClasesAux;
using System.Globalization;

namespace StephManager
{
    public partial class frmNotificacionesProdStockMinimo : Form
    {
        #region Constructores

        public frmNotificacionesProdStockMinimo()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                LogError.AddExcFileTxt(ex, "frmNotificacionesProdStockMinimo ~ frmNotificacionesProdStockMinimo()");
            }
        }

        #endregion

        #region Métodos

        private void IniciarForm()
        {
            try
            {
                this.LlenarGridCatProductosStockMinimo(false);
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

        private void LlenarGridCatProductosStockMinimo(bool Band)
        {
            try
            {
                Producto DatosAux = new Producto { Conexion = Comun.Conexion,  IDSucursal = Comun.IDSucursalCaja, BuscarTodos = Band };
                Producto_Negocio PN = new Producto_Negocio();
                PN.ObtenerCatProductosStockMinimo(DatosAux);
                this.dgvProductos.AutoGenerateColumns = false;
                this.dgvProductos.DataSource = DatosAux.TablaDatos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ActualizarVisto()
        {
            try
            {
                NotificacionesSistemas NS = new NotificacionesSistemas();
                NotificacionesSistemas_Negocio NSN = new NotificacionesSistemas_Negocio();
                NS.Conexion = Comun.Conexion;
                NS.IDSucursal = Comun.IDSucursalCaja;
                NS.Visto = true;
                NS.IDTipoNotificacion = 1;
                NS.IDUsuario = Comun.IDUsuario;
                NSN.ActualizarNotificacionVisto(NS);
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
                LogError.AddExcFileTxt(ex, "frmNotificacionesProdStockMinimo ~ btnSalir_Click");
                MessageBox.Show(Comun.MensajeError, Comun.Sistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmNotificacionesProdStockMinimo_Load(object sender, EventArgs e)
        {
            try
            {
                this.ActualizarVisto();
                this.IniciarForm();                
            }
            catch (Exception ex)
            {
                LogError.AddExcFileTxt(ex, "frmNotificacionesProdStockMinimo ~ frmNotificacionesProdStockMinimo_Load");
                MessageBox.Show(Comun.MensajeError, Comun.Sistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    }
}
