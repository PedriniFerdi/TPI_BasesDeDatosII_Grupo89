using System.Collections.Generic;
using Padelito.Datos.Repositorios;
using Padelito.Dominio.Entidades;

namespace Padelito.Negocio.Servicios
{
    public class AuditoriaReservaNegocio
    {
        private readonly AuditoriaReservaDatos _auditoriaReservaDatos = new AuditoriaReservaDatos();

        public List<AuditoriaReservaDetalle> Listar()
        {
            return _auditoriaReservaDatos.Listar();
        }
    }
}
