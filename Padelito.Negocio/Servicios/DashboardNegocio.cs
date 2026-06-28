using Padelito.Datos.Repositorios;
using Padelito.Dominio.Entidades;

namespace Padelito.Negocio.Servicios
{
    public class DashboardNegocio
    {
        private readonly DashboardDatos _dashboardDatos = new DashboardDatos();

        public DashboardResumen ObtenerResumen()
        {
            return _dashboardDatos.ObtenerResumen();
        }
    }
}
