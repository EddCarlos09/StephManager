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
    }
}
