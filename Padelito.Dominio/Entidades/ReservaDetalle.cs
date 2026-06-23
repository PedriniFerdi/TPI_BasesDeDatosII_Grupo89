using System;

namespace Padelito.Dominio.Entidades
{
    public class ReservaDetalle
    {
        public int IdReserva { get; set; }
        public DateTime FechaReserva { get; set; }
        public string Cliente { get; set; }
        public string Cancha { get; set; }
        public string TipoCancha { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public string Empleado { get; set; }
        public string EstadoReserva { get; set; }
        public string Promocion { get; set; }
        public decimal PrecioBase { get; set; }
        public decimal PrecioFinal { get; set; }
        public DateTime FechaCreacion { get; set; }

        public string Horario
        {
            get
            {
                return HoraInicio.ToString(@"hh\:mm") + " - " + HoraFin.ToString(@"hh\:mm");
            }
        }
    }
}
