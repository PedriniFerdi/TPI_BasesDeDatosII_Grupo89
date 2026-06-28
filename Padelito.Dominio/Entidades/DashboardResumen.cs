using System.Collections.Generic;

namespace Padelito.Dominio.Entidades
{
    public class DashboardResumen
    {
        public int ClientesActivos { get; set; }
        public int CanchasActivas { get; set; }
        public int ReservasDelDia { get; set; }
        public decimal IngresosRegistrados { get; set; }
        public int PagosRegistrados { get; set; }
        public List<ReservaDetalle> UltimasReservas { get; set; }

        public DashboardResumen()
        {
            UltimasReservas = new List<ReservaDetalle>();
        }
    }
}
