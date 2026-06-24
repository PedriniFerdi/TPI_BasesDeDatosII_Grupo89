using System;

namespace Padelito.Dominio.Entidades
{
    public class Pago
    {
        public int IdPago { get; set; }
        public int IdReserva { get; set; }
        public int IdMetodoPago { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaPago { get; set; }
        public string Observacion { get; set; }
    }
}
