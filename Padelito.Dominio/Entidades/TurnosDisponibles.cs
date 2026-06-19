using System;

namespace Padelito.Dominio.Entidades
{
    public class TurnosDisponibles
    {
        public int IdTurnoDisponible { get; set; }
        public int IdCancha { get; set; }
        public string NombreCancha { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public bool Activo { get; set; }

        public string Horario
        {
            get
            {
                return HoraInicio.ToString(@"hh\:mm") + " - " + HoraFin.ToString(@"hh\:mm");
            }
        }
    }
}
