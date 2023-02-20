using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SincronizadorSmartSheetComisiones.Models.POK
{
    public class UsuarioModel
    {
        public int dni { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string email { get; set; }
        public string telefono { get; set; }
        public int tipoVendedor { get; set; }
        public string imagen { get; set; }
        public DateTime? fechaCreacion { get; set; }
        public string fechaEdicion { get; set; }
    }
}
