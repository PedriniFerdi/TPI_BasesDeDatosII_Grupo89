using System.Collections.Generic;
using Padelito.Datos.Repositorios;
using Padelito.Dominio.Entidades;

namespace Padelito.Negocio.Servicios
{
    public class EstadoReservaNegocio
    {
        private readonly EstadoReservaDatos _estadoReservaDatos = new EstadoReservaDatos();

        public List<EstadoReserva> Listar()
        {
            return _estadoReservaDatos.Listar();
        }
    }
}
