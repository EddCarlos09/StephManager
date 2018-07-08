using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreativaSL.Dll.StephManager.Global
{
    
    public class ReporteFaltas
    {

        public ReporteFaltas()
        {
            IDReporte = 0;
            FechaInicio = DateTime.MinValue;
            FechaFin = DateTime.MinValue;
            Completo = false;
            Detalle = new List<ReporteFaltasDetalle>();
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


        public List<ReporteFaltasDetalle> Detalle { get; set; }
    }
}
