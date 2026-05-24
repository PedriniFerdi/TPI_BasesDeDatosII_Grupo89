namespace Padelito.Dominio.Entidades
{
    public class Cancha
    {
        public int IdCancha { get; set; }
        public string Nombre { get; set; }
        public int IdTipoCancha { get; set; }
        public string TipoCanchaDescripcion { get; set; }
        public decimal PrecioHora { get; set; }
        public bool Activa { get; set; }
    }
}
