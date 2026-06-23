using System;

namespace Padelito.Dominio.Entidades
{
    public class Reserva
    {
        public int IdReserva { get; set; }
        public int IdCliente { get; set; }
        public int IdTurnoDisponible { get; set; }
        public int IdEmpleado { get; set; }
        public int? IdPromocion { get; set; }
        public DateTime FechaReserva { get; set; }
        public int IdEstadoReserva { get; set; }
        public decimal PrecioBase { get; set; }
        public decimal PrecioFinal { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
