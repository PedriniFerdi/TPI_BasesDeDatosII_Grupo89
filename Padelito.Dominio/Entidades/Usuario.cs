using System;

namespace Padelito.Dominio.Entidades
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string Contrasenia { get; set; }
        public int IdEmpleado { get; set; }
        public string EmpleadoNombreCompleto { get; set; }
        public int IdRol { get; set; }
        public string RolDescripcion { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaAlta { get; set; }
    }
}
