using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SincronizadorSmartSheetComisiones.Models.POK
{
    public class LoginResponseModel
    {
        public string token { get; set; }
        public UsuarioModel usuario { get; set; }
        public int idInmobiliaria { get; set; }
    }
}
