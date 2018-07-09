using CreativaSL.Dll.StephManager.Global;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreativaSL.Dll.StephManager.Datos
{
    
    public class ReporteFaltas_Datos
    {
        public int GenerarReporteFaltas(string Conexion, DateTime FechaInicio, DateTime FechaFin, string IDUsuario)
        {
            try
            {
                int IDReporte = -1;
                object[] Parametros = { FechaInicio, FechaFin, IDUsuario };
                object Result = SqlHelper.ExecuteScalar(Conexion, "Reportes.spCSLDB_set_GenerarReporteFaltas", Parametros);
                if (Result != null)
                {
                    int.TryParse(Result.ToString(), out IDReporte);
                }
                return IDReporte;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ReporteFaltas ObtenerDetalleReporteFaltas(string Conexion, int IDReporte)
        {
            try
            {
                ReporteFaltas Resultado = new ReporteFaltas();
                DataSet Ds = SqlHelper.ExecuteDataset(Conexion, "Reportes.spCSLDB_get_ReporteFaltasXID", IDReporte);
                if (Ds != null)
                {
                    if (Ds.Tables.Count == 2)
                    {
                        DataTableReader Dr = Ds.Tables[0].CreateDataReader();
                        while (Dr.Read())
                        {
                            Resultado.FechaInicio = !Dr.IsDBNull(Dr.GetOrdinal("FechaInicio")) ? Dr.GetDateTime(Dr.GetOrdinal("FechaInicio")) : DateTime.MinValue;
                            Resultado.FechaFin = !Dr.IsDBNull(Dr.GetOrdinal("FechaFin")) ? Dr.GetDateTime(Dr.GetOrdinal("FechaFin")) : DateTime.MinValue;
                            break;
                        }
                        Dr.Close();

                        List<ReporteFaltasDetalle> Lista = new List<ReporteFaltasDetalle>();
                        ReporteFaltasDetalle Item;
                        DataTableReader Dr2 = Ds.Tables[1].CreateDataReader();
                        while (Dr2.Read())
                        {
                            Item = new ReporteFaltasDetalle();
                            Item.IDSucursal = !Dr2.IsDBNull(Dr2.GetOrdinal("id_sucursal")) ? Dr2.GetString(Dr2.GetOrdinal("id_sucursal")) : string.Empty;
                            Item.Sucursal = !Dr2.IsDBNull(Dr2.GetOrdinal("nombreSucursal")) ? Dr2.GetString(Dr2.GetOrdinal("nombreSucursal")) : string.Empty;
                            Item.IDEmpleado = !Dr2.IsDBNull(Dr2.GetOrdinal("id_empleado")) ? Dr2.GetString(Dr2.GetOrdinal("id_empleado")) : string.Empty;
                            Item.Empleado = !Dr2.IsDBNull(Dr2.GetOrdinal("nombreCompleto")) ? Dr2.GetString(Dr2.GetOrdinal("nombreCompleto")) : string.Empty;
                            Item.Fecha = !Dr2.IsDBNull(Dr2.GetOrdinal("fecha")) ? Dr2.GetDateTime(Dr2.GetOrdinal("fecha")) : DateTime.MinValue;
                            Lista.Add(Item);
                        }
                        Dr2.Close();

                        Resultado.Detalle = Lista;
                        Resultado.Completo = true;
                    }
                }
                return Resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ReporteFaltas> ObtenerReportesFaltas(string Conexion)
        {
            try
            {
                List<ReporteFaltas> Lista = new List<ReporteFaltas>();
                ReporteFaltas Item;
                SqlDataReader Dr = SqlHelper.ExecuteReader(Conexion, "Reportes.spCSLDB_get_ReporteFaltasGrid");
                while (Dr.Read())
                {
                    Item = new ReporteFaltas();
                    Item.IDReporte = !Dr.IsDBNull(Dr.GetOrdinal("IDReporte")) ? Dr.GetInt32(Dr.GetOrdinal("IDReporte")) : 0;
                    Item.FechaInicio = !Dr.IsDBNull(Dr.GetOrdinal("FechaInicio")) ? Dr.GetDateTime(Dr.GetOrdinal("FechaInicio")) : DateTime.MinValue;
                    Item.FechaFin = !Dr.IsDBNull(Dr.GetOrdinal("FechaFin")) ? Dr.GetDateTime(Dr.GetOrdinal("FechaFin")) : DateTime.MinValue;
                    Lista.Add(Item);
                }
                Dr.Close();
                return Lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }// 

        public List<ReporteFaltas> ObtenerReporteFaltasBusqueda(string Conexion, DateTime Fecha)
        {
            try
            {
                List<ReporteFaltas> Lista = new List<ReporteFaltas>();
                ReporteFaltas Item;
                object[] Parametros = { Fecha };
                SqlDataReader Dr = SqlHelper.ExecuteReader(Conexion, "Reportes.spCSLDB_get_ReporteFaltasSearchGrid", Parametros);
                while (Dr.Read())
                {
                    Item = new ReporteFaltas();
                    Item.IDReporte = !Dr.IsDBNull(Dr.GetOrdinal("IDReporte")) ? Dr.GetInt32(Dr.GetOrdinal("IDReporte")) : 0;
                    Item.FechaInicio = !Dr.IsDBNull(Dr.GetOrdinal("FechaInicio")) ? Dr.GetDateTime(Dr.GetOrdinal("FechaInicio")) : DateTime.MinValue;
                    Item.FechaFin = !Dr.IsDBNull(Dr.GetOrdinal("FechaFin")) ? Dr.GetDateTime(Dr.GetOrdinal("FechaFin")) : DateTime.MinValue;
                    Lista.Add(Item);
                }
                Dr.Close();
                return Lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
