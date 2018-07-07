using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreativaSL.Dll.StephManager.Global
{
    public class ReporteComprasPorProveedor
    {
        
        public ReporteComprasPorProveedor()
        {
            IDReporte = 0;
            FechaInicio = DateTime.MinValue;
            FechaFin = DateTime.MinValue;
            Completo = false;
            Detalle = new List<ReporteComprasPorProveedorDetalle>();
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


        public List<ReporteComprasPorProveedorDetalle> Detalle { get; set; }
    }
}
