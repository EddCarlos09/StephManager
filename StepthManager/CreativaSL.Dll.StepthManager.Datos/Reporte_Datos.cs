﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using CreativaSL.Dll.StephManager.Global;
using System.Data.SqlClient;

namespace CreativaSL.Dll.StephManager.Datos
{
    public class Reporte_Datos
    {
        public object[] ObtenerReporteMaterialesProduccion(Sucursal _datos)
        {
            try
            {
                List<ReporteMaterialesProduccion> Lista01 = new List<ReporteMaterialesProduccion>();
                List<ReporteMaterialesProduccion> Lista02 = new List<ReporteMaterialesProduccion>();
                DataSet ds = SqlHelper.ExecuteDataset(_datos.Conexion, "spCSLDB_get_ReporteMaterialesProduccion", _datos.IDSucursal);
                if(ds != null)
                {
                    if(ds.Tables.Count == 2)
                    {
                        DataTableReader dr = ds.Tables[0].CreateDataReader();                        
                        ReporteMaterialesProduccion Item01;
                        while(dr.Read())
                        {
                            Item01 = new ReporteMaterialesProduccion();
                            Item01.IDSucursal = dr.GetString(dr.GetOrdinal("id_sucursal"));
                            Item01.NombreSucursal = dr.GetString(dr.GetOrdinal("nombreSucursal"));
                            Item01.ClaveProducto = dr.GetString(dr.GetOrdinal("clave"));
                            Item01.NombreProducto = dr.GetString(dr.GetOrdinal("nombre"));
                            Item01.Cantidad = dr.GetInt32(dr.GetOrdinal("Cantidad"));
                            Lista01.Add(Item01);
                        }

                        DataTableReader dr2 = ds.Tables[1].CreateDataReader();
                        
                        ReporteMaterialesProduccion Item02;
                        while (dr2.Read())
                        {
                            Item02 = new ReporteMaterialesProduccion();
                            Item02.IDSucursal = dr2.GetString(dr2.GetOrdinal("id_sucursal"));
                            Item02.NombreSucursal = dr2.GetString(dr2.GetOrdinal("nombreSucursal"));
                            Item02.IDEmpleado = dr2.GetString(dr2.GetOrdinal("id_sucursal"));
                            Item02.NombreEmpleado = dr2.GetString(dr2.GetOrdinal("Empleado"));
                            Item02.ClaveProducto = dr2.GetString(dr2.GetOrdinal("clave"));
                            Item02.NombreProducto = dr2.GetString(dr2.GetOrdinal("nombre"));
                            Item02.Cantidad = dr2.GetInt32(dr2.GetOrdinal("Cantidad"));
                            Lista02.Add(Item02);
                        }
                    }
                }
                object[] Resultado = { Lista01, Lista02 };
                return Resultado;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<ReporteServiciosRealizados> ObtenerReporteServiciosRealizados(string Conexion, DateTime FechaInicio, DateTime FechaFin)
        {
            try
            {
                object[] Parametros = { FechaInicio, FechaFin };
                List<ReporteServiciosRealizados> Lista = new List<ReporteServiciosRealizados>();
                ReporteServiciosRealizados Item;
                SqlDataReader Dr = SqlHelper.ExecuteReader(Conexion, "Reportes.spCSLDB_get_ReporteTrabajosRealizados", Parametros);
                while(Dr.Read())
                {
                    Item = new ReporteServiciosRealizados();
                    Item.IDSucursal = !Dr.IsDBNull(Dr.GetOrdinal("IDSucursal")) ? Dr.GetString(Dr.GetOrdinal("IDSucursal")) : string.Empty;
                    Item.NombreSucursal = !Dr.IsDBNull(Dr.GetOrdinal("NombreSucursal")) ? Dr.GetString(Dr.GetOrdinal("NombreSucursal")) : string.Empty;
                    Item.IDEmpleado = !Dr.IsDBNull(Dr.GetOrdinal("IDEmpleado")) ? Dr.GetString(Dr.GetOrdinal("IDEmpleado")) : string.Empty;
                    Item.NombreEmpleado = !Dr.IsDBNull(Dr.GetOrdinal("NombreEmpleado")) ? Dr.GetString(Dr.GetOrdinal("NombreEmpleado")) : string.Empty;
                    Item.IDServicio = !Dr.IsDBNull(Dr.GetOrdinal("IDServicio")) ? Dr.GetString(Dr.GetOrdinal("IDServicio")) : string.Empty;
                    Item.NombreServicio = !Dr.IsDBNull(Dr.GetOrdinal("NombreServicio")) ? Dr.GetString(Dr.GetOrdinal("NombreServicio")) : string.Empty;
                    Item.Cantidad = !Dr.IsDBNull(Dr.GetOrdinal("CantidadServicios")) ? Dr.GetInt32(Dr.GetOrdinal("CantidadServicios")) : 0;
                    Lista.Add(Item);
                }
                Dr.Close();
                return Lista;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<ReporteConsumoMaterial> ObtenerReporteConsumoMaterial(string Conexion, string IDSucursal, DateTime FechaInicio, DateTime FechaFin)
        {
            try
            {
                object[] Parametros = { IDSucursal, FechaInicio, FechaFin };
                List<ReporteConsumoMaterial> Lista = new List<ReporteConsumoMaterial>();
                ReporteConsumoMaterial Item;
                SqlDataReader Dr = SqlHelper.ExecuteReader(Conexion, "Reportes.spCSLDB_get_ReporteConsumoMaterial", Parametros);
                while (Dr.Read())
                {
                    Item = new ReporteConsumoMaterial();
                    Item.Tipo = !Dr.IsDBNull(Dr.GetOrdinal("Tipo")) ? Dr.GetInt32(Dr.GetOrdinal("Tipo")) : 0;
                    Item.IDGeneral = !Dr.IsDBNull(Dr.GetOrdinal("IDGeneral")) ? Dr.GetString(Dr.GetOrdinal("IDGeneral")) : string.Empty;
                    Item.Nombre = !Dr.IsDBNull(Dr.GetOrdinal("Nombre")) ? Dr.GetString(Dr.GetOrdinal("Nombre")) : string.Empty;
                    Item.IDProducto = !Dr.IsDBNull(Dr.GetOrdinal("IDProducto")) ? Dr.GetString(Dr.GetOrdinal("IDProducto")) : string.Empty;
                    Item.NombreProducto = !Dr.IsDBNull(Dr.GetOrdinal("Producto")) ? Dr.GetString(Dr.GetOrdinal("Producto")) : string.Empty;
                    Item.Clave = !Dr.IsDBNull(Dr.GetOrdinal("Clave")) ? Dr.GetString(Dr.GetOrdinal("Clave")) : string.Empty;
                    Item.Fecha = !Dr.IsDBNull(Dr.GetOrdinal("Fecha")) ? Dr.GetDateTime(Dr.GetOrdinal("Fecha")) : DateTime.MinValue;
                    Item.Produccion = !Dr.IsDBNull(Dr.GetOrdinal("Produccion")) ? Dr.GetBoolean(Dr.GetOrdinal("Produccion")) : false;
                    Item.CumpleMetrica = !Dr.IsDBNull(Dr.GetOrdinal("CumpleMetrica")) ? Dr.GetBoolean(Dr.GetOrdinal("CumpleMetrica")) : false;
                    
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
