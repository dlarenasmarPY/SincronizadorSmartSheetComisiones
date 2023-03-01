using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SincronizadorSmartSheetComisiones.Models.POK
{
    public class VendedorByClienteRequestModel
    {
        public string identificador { get; set; }
        public int valorIdentificador { get; set; }
        public string? tipoCliente { get; set; }
        public int? idProyecto { get; set; }
    }
}
