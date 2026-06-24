using System.Collections.Generic;
using Padelito.Datos.Repositorios;
using Padelito.Dominio.Entidades;

namespace Padelito.Negocio.Servicios
{
    public class MetodoPagoNegocio
    {
        private readonly MetodoPagoDatos _metodoPagoDatos = new MetodoPagoDatos();

        public List<MetodoPago> Listar()
        {
            return _metodoPagoDatos.Listar();
        }
    }
}
