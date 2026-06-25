using System;

namespace Padelito.Dominio.Entidades
{
    public class ReporteReserva
    {
        public int IdReserva { get; set; }
        public DateTime FechaReserva { get; set; }
        public string Cliente { get; set; }
        public string Cancha { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public string EstadoReserva { get; set; }
        public decimal PrecioBase { get; set; }
        public decimal PrecioFinal { get; set; }

        public string Horario
        {
            get
            {
                return HoraInicio.ToString(@"hh\:mm") + " - " + HoraFin.ToString(@"hh\:mm");
            }
        }
    }
}
