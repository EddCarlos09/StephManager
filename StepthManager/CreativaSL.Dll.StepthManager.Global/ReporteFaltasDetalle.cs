using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreativaSL.Dll.StephManager.Global
{
    public class ReporteFaltasDetalle
    {
        public ReporteFaltasDetalle()
        {
            this.IDSucursal = string.Empty;
            this.Sucursal = string.Empty;
            this.IDEmpleado = string.Empty;
            this.Empleado = string.Empty;
            this.Fecha = DateTime.MinValue;
        }

        public string IDSucursal { get; set; }

        public string Sucursal { get; set; }

        public string IDEmpleado { get; set; }

        public string Empleado { get; set; }

        public DateTime Fecha { get; set; }

    }
}
