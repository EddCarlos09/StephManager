﻿using CreativaSL.Dll.StephManager.Datos;
using CreativaSL.Dll.StephManager.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreativaSL.Dll.StephManager.Negocio
{
    public class Reporte_NegocioTrabajosRealizados
    {
        /// <summary>
        /// Método para generar el reporte de trabajos realizados
        /// </summary>
        /// <param name="Conexion">Cadena de conexión a la BD</param>
        /// <param name="FechaInicio">Fecha de inicio del período</param>
        /// <param name="FechaFin">Fecha de término del período</param>
        /// <param name="IDUsuario">Identificador del usuraio</param>
        /// <returns>Retorna el identificador del reporte seleccionado.</returns>
        public int GenerarReporteTrabajosRealizados(string Conexion, DateTime FechaInicio, DateTime FechaFin, string IDUsuario)
        {
            try
            {
                Reporte_DatosTrabajosRealizados Datos = new Reporte_DatosTrabajosRealizados();
                return Datos.GenerarReporteTrabajosRealizados(Conexion, FechaInicio, FechaFin, IDUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Método para obtener el detalle de los trabajos realizados
        /// </summary>
        /// <param name="Conexion">Cadena de conexión a la BD</param>
        /// <param name="IDReporte">ID del Reporte al que se quiere acceder</param>
        /// <returns>Retorna una lista con el detalle del trabajo realizado seleccionado.</returns>
        public ReporteTrabajosRealizados ObtenerDetalleReporteTrabajosRealizados(string Conexion, int IDReporte)
        {
            try
            {
                Reporte_DatosTrabajosRealizados Datos = new Reporte_DatosTrabajosRealizados();
                return Datos.ObtenerDetalleReporteTrabajosRealizados(Conexion, IDReporte);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Método para obtener los trabajos realizados
        /// </summary>
        /// <param name="Conexion">Cadena de conexión a la BD</param>
        /// <returns>Retorna una lista con el IDReporte, la fecha de Inicio y la fecha de termino</returns>
        public List<ReporteTrabajosRealizados> ObtenerReportesTrabajosRealizados(string Conexion)
        {
            try
            {
                Reporte_DatosTrabajosRealizados Datos = new Reporte_DatosTrabajosRealizados();
                return Datos.ObtenerReporteTrabajosRealizados(Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
