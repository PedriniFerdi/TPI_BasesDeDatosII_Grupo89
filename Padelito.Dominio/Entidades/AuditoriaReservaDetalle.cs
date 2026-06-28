using System;

namespace Padelito.Dominio.Entidades
{
    public class AuditoriaReservaDetalle
    {
        public int IdAuditoria { get; set; }
        public int IdReserva { get; set; }
        public string Accion { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaAccion { get; set; }
        public string UsuarioSistema { get; set; }
    }
}
