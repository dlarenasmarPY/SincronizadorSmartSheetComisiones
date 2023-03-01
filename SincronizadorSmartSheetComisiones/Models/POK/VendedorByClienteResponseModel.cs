using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SincronizadorSmartSheetComisiones.Models.POK
{
    public class VendedorByClienteResponseModel
    {

        public int id_proyecto { get; set; }
        public string glosa_proyecto { get; set; }
        public Vendedor vendedor { get; set; }


        public class Vendedor
        {
            public VendedorSeguimientoReciente vendedor_seguimiento_reciente { get; set; }
            public VendedorCotizacionReciente vendedor_cotizacion_reciente { get; set; }
            public VendedorReservaReciente vendedor_reserva_reciente { get; set; }
            public VendedorPromesaReciente vendedor_promesa_reciente { get; set; }
        }

        public class VendedorCotizacionReciente
        {
            public int id_cotizacion { get; set; }
            public string rut_largo_vendedor_cotizacion { get; set; }
            public string rut_corto_vendedor_cotizacion { get; set; }
            public string dv_vendedor_cotizacion { get; set; }
            public string nombre_vendedor_cotizacion { get; set; }
            public string apellido_paterno_vendedor_cotizacion { get; set; }
            public string apellido_materno_vendedor_cotizacion { get; set; }
            public string telefono_vendedor_cotizacion { get; set; }
            public string email_vendedor_cotizacion { get; set; }
            public DateTime fecha_cotizacion { get; set; }
        }

        public class VendedorPromesaReciente
        {
            public int id_promesa { get; set; }
            public string rut_largo_vendedor_promesa { get; set; }
            public string rut_corto_vendedor_promesa { get; set; }
            public string dv_vendedor_promesa { get; set; }
            public string nombre_vendedor_promesa { get; set; }
            public string apellido_paterno_vendedor_promesa { get; set; }
            public string apellido_materno_vendedor_promesa { get; set; }
            public string email_vendedor_promesa { get; set; }
            public DateTime fecha_promesa { get; set; }
        }

        public class VendedorReservaReciente
        {
            public int id_reserva { get; set; }
            public string rut_largo_vendedor_reserva { get; set; }
            public string rut_corto_vendedor_reserva { get; set; }
            public string dv_vendedor_reserva { get; set; }
            public string nombre_vendedor_reserva { get; set; }
            public string apellido_paterno_vendedor_reserva { get; set; }
            public string apellido_materno_vendedor_reserva { get; set; }
            public string telefono_vendedor_reserva { get; set; }
            public string email_vendedor_reserva { get; set; }
            public DateTime fecha_reserva { get; set; }
        }

        public class VendedorSeguimientoReciente
        {
            public int id_seguimiento { get; set; }
            public string rut_largo_vendedor_seguimiento { get; set; }
            public string rut_corto_vendedor_seguimiento { get; set; }
            public string dv_vendedor_seguimiento { get; set; }
            public string nombre_vendedor_seguimiento { get; set; }
            public string apellido_paterno_vendedor_seguimiento { get; set; }
            public string apellido_materno_vendedor_seguimiento { get; set; }
            public string telefono_vendedor_seguimiento { get; set; }
            public string email_vendedor_seguimiento { get; set; }
            public DateTime fecha_seguimiento { get; set; }
        }

    }
}
