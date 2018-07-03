using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreativaSL.Dll.StephManager.Global
{
    public class ReporteConsumoMaterial
    {
        public int Tipo { get; set; }
        /// <summary>
        /// IDSucursal o IDEmpleado
        /// </summary>
        public string IDGeneral { get; set; }
        public string Nombre { get; set; }
        public string IDProducto { get; set; }
        public string NombreProducto { get; set; }
        public string Clave { get; set; }
        public DateTime Fecha { get; set; }
        public bool Produccion { get; set; }
        public bool CumpleMetrica { get; set; }
        public string ImagenMetrica { get; set; }
    }
}
