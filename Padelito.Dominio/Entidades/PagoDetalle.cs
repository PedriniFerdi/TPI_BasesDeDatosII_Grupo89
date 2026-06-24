using System;

namespace Padelito.Dominio.Entidades
{
    public class PagoDetalle
    {
        public int IdPago { get; set; }
        public int IdReserva { get; set; }
        public string Cliente { get; set; }
        public string Cancha { get; set; }
        public DateTime FechaReserva { get; set; }
        public string MetodoPago { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaPago { get; set; }
        public string Observacion { get; set; }
    }
}
