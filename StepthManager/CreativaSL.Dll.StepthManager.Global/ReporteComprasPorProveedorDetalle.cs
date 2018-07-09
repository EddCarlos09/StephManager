using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreativaSL.Dll.StephManager.Global
{
    public class ReporteComprasPorProveedorDetalle
    {
        public ReporteComprasPorProveedorDetalle()
        {

            IDProveedor = string.Empty;
            Proveedor = string.Empty;
            Total = 0.0M;
            
        }

        public string IDProveedor { get; set; }

        /// <summary>
        /// 
        /// </summary>


        public string Proveedor { get; set; }
        /// <summary>
        /// 
        /// </summary>

        public decimal Total { get; set; }
    }
}
