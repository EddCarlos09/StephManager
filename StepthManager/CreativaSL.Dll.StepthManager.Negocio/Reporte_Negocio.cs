using CreativaSL.Dll.StephManager.Datos;
using CreativaSL.Dll.StephManager.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreativaSL.Dll.StephManager.Negocio
{
    public class Reporte_Negocio
    {
        public object[] ObtenerReporteMaterialesProduccion(Sucursal _datos)
        {
            try
            {
                Reporte_Datos repDatos = new Reporte_Datos();
                return repDatos.ObtenerReporteMaterialesProduccion(_datos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ReporteServiciosRealizados> ObtenerReporteServiciosRealizados(string Conexion, DateTime FechaInicio, DateTime FechaFin)
        {
            try
            {
                Reporte_Datos Datos = new Reporte_Datos();
                return Datos.ObtenerReporteServiciosRealizados(Conexion, FechaInicio, FechaFin);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// <summary>
        /// Método para obtener el reporte de consumo de material
        /// </summary>
        /// <param name="Conexion">Cadena de conexión a la BD</param>
        /// <param name="IDSucursal">Identificador de la sucursal a la que se generará el reporte</param>
        /// <param name="FechaInicio">Fecha de inicio del período</param>
        /// <param name="FechaFin">Fecha de término del período</param>
        /// <returns>Retorna una lista con el detalle de consumo de material de la sucursal seleccionada.</returns>
        public List<ReporteConsumoMaterial> ObtenerReporteConsumoMaterial(string Conexion, string IDSucursal, DateTime FechaInicio, DateTime FechaFin)
        {
            try
            {
                Reporte_Datos Datos = new Reporte_Datos();
                return Datos.ObtenerReporteConsumoMaterial(Conexion, IDSucursal, FechaInicio, FechaFin);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





        public int GenerarReporteProductosVendidos(string Conexion, DateTime FechaInicio, DateTime FechaFin, string IDUsuario)
        {
            try
            {
                Reporte_Datos Datos = new Reporte_Datos();
                return Datos.GenerarReporteProductosVendidos(Conexion, FechaInicio, FechaFin, IDUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ReporteProductosVendidos ObtenerDetalleReporteProductosVendidos(string Conexion, int IDReporte)
        {
            try
            {
                Reporte_Datos Datos = new Reporte_Datos();
                return Datos.ObtenerDetalleReporteProductosVendidos(Conexion, IDReporte);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ReporteProductosVendidos> ObtenerReportesProductosVendidos(string Conexion)
        {
            try
            {
                Reporte_Datos Datos = new Reporte_Datos();
                return Datos.ObtenerReportesProductosVendidos(Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //TrabajosRealizados
        public int GenerarReporteTrabajosRealizados(string Conexion, DateTime FechaInicio, DateTime FechaFin, string IDUsuario)
        {
            try
            {
                Reporte_Datos Datos = new Reporte_Datos();
                return Datos.GenerarReporteTrabajosRealizados(Conexion, FechaInicio, FechaFin, IDUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ReporteTrabajosRealizados ObtenerDetalleReporteTrabajosRealizados(string Conexion, int IDReporte)
        {
            try
            {
                Reporte_Datos Datos = new Reporte_Datos();
                return Datos.ObtenerDetalleReporteTrabajosRealizados(Conexion, IDReporte);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ReporteTrabajosRealizados> ObtenerReportesTrabajosRealizados(string Conexion)
        {
            try
            {
                Reporte_Datos Datos = new Reporte_Datos();
                return Datos.ObtenerReporteTrabajosRealizados(Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //

    }
}
