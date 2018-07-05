using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreativaSL.Dll.StephManager.Global
{
    public class ReporteProductosVendidosDetalle
    {
        public ReporteProductosVendidosDetalle()
        {
            IDSucursal = string.Empty;
            Sucursal = string.Empty;
            IDProducto = string.Empty;
            Clave = string.Empty;
            Producto = string.Empty;
            Cantidad = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        public string IDSucursal { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Sucursal { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string IDProducto { get; set; }
        public string Clave { get; set; }
        public string Producto { get; set; }
        public decimal Cantidad { get; set; }
    }
}
