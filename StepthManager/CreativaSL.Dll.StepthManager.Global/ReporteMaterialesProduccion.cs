using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreativaSL.Dll.StephManager.Global
{
    public class ReporteMaterialesProduccion
    {
        public string IDSucursal { get; set; }
        public string NombreSucursal { get; set; }
        public string IDEmpleado { get; set; }
        public string NombreEmpleado { get; set; }
        public string ClaveProducto { get; set; }
        public string NombreProducto { get; set; }
        public int Cantidad { get; set; }        
    }
}
