using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreativaSL.Dll.StephManager.Global
{
    public class ReporteProductosVendidos
    {

        public ReporteProductosVendidos()
        {
            IDReporte = 0;
            FechaInicio = DateTime.MinValue;
            FechaFin = DateTime.MinValue;
            Completo = false;
            Detalle = new List<ReporteProductosVendidosDetalle>();
        }

        public int IDReporte { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime FechaInicio { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime FechaFin { get; set; }

        public bool Completo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ReporteProductosVendidosDetalle> Detalle { get; set; }
    }
}
