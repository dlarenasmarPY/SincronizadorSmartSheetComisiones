using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SincronizadorSmartSheetComisiones.Models.Fin7
{
    public class SmartSheetComisionesRequestModel
    {
        public string FechaDesde { get; set; }
        public string FechaHasta { get; set; }
        public int Zona { get; set; }
    }
}
