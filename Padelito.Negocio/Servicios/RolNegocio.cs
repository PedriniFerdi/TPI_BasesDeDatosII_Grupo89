using System.Collections.Generic;
using Padelito.Datos.Repositorios;
using Padelito.Dominio.Entidades;

namespace Padelito.Negocio.Servicios
{
    public class RolNegocio
    {
        private readonly RolDatos _rolDatos = new RolDatos();

        public List<Rol> Listar()
        {
            return _rolDatos.Listar();
        }
    }
}
