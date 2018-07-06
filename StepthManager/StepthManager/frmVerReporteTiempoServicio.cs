﻿using CreativaSL.Dll.StephManager.Global;
using CreativaSL.Dll.StephManager.Negocio;
using Microsoft.Reporting.WinForms;
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
    public partial class frmVerReporteTiempoServicio : Form
    {
        public int IDReporte = -1;

        public frmVerReporteTiempoServicio(int _IDReporte)
        {
            try
            {
                InitializeComponent();
                IDReporte = _IDReporte;
            }
            catch (Exception ex)
            {
                LogError.AddExcFileTxt(ex, "frmVerReporteProductosVendidos ~ frmVerReporteProductosVendidos()");
            }
        }

        private void frmVerReporteProductosVendidos_Load(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(Path.Combine(System.Windows.Forms.Application.StartupPath, @"Resources\Documents\" + Comun.UrlLogo)))
                {
                    this.pictureBox1.Image = Image.FromFile(Path.Combine(System.Windows.Forms.Application.StartupPath, @"Resources\Documents\" + Comun.UrlLogo));
                }
                this.IniciarForm();
            }
            catch (Exception ex)
            {
                LogError.AddExcFileTxt(ex, "frmVerReporteProductosVendidos ~ frmVerReporteProductosVendidos_Load");
            }
        }

        private void IniciarForm()
        {
            try
            {
                CargarReporte();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarReporte()
        {
            try
            {
                this.GenerarReporteTiempoServicios();
                this.lblTitulo.Text = "REPORTE DE TIEMPO DE SERVICIOS";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GenerarReporteTiempoServicios()
        {
            try
            {
                reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                reportViewer1.ZoomPercent = 100;
                reportViewer1.LocalReport.DataSources.Clear();
                Reporte_Negocio Neg = new Reporte_Negocio();
                ReporteProductosVendidos Datosreporte = Neg.ObtenerDetalleReporteProductosVendidos(Comun.Conexion, IDReporte);
                
                ////reportViewer1.LocalReport.EnableExternalImages = true;
                //ReportParameter[] Parametros = new ReportParameter[5];
                //Parametros[0] = new ReportParameter("Empresa", Comun.NombreComercial);
                //Parametros[1] = new ReportParameter("Eslogan", Comun.Eslogan);
                //Parametros[2] = new ReportParameter("Direccion", Comun.Direccion);
                //Parametros[3] = new ReportParameter("TituloReporte", "REPORTE DE TIEMPO DE SERVICIOS");
                //if (File.Exists(@"Resources\Documents\" + Comun.UrlLogo.ToLower()))
                //{
                //    string Aux = new Uri(Path.Combine(System.Windows.Forms.Application.StartupPath, @"Resources\Documents\" + Comun.UrlLogo.ToLower())).AbsoluteUri;
                //    Parametros[4] = new ReportParameter("UrlLogo", new Uri(Path.Combine(System.Windows.Forms.Application.StartupPath, @"Resources\Documents\" + Comun.UrlLogo.ToLower())).AbsoluteUri);
                //}
                //else
                //    Parametros[4] = new ReportParameter("UrlLogo", new Uri(Path.Combine(System.Windows.Forms.Application.StartupPath, @"Resources\Documents\Default.jpg")).AbsoluteUri);                
                //this.reportViewer1.LocalReport.ReportEmbeddedResource = "StephManager.Informes.Reportes.TiempoServicios.rdlc";
                //reportViewer1.LocalReport.SetParameters(Parametros);
                //reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("TiempoServicios", DatosReporte.Detalle));
                //this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                throw ex;
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
                LogError.AddExcFileTxt(ex, "frmVerReporteTiempoServicios ~ btnSalir_Click");
            }
        }
    }
}
