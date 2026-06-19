using System;

namespace Padelito.Dominio.Entidades
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public int IdPersona { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaAlta { get; set; }
    }
}
