using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Padelito.Dominio.Entidades;

namespace Padelito.Web
{
    public static class SeguridadSesion
    {
        public const string RolAdministrador = "Administrador";
        public const string RolEmpleado = "Empleado";

        public static bool EstaLogueado()
        {
            return HttpContext.Current != null &&
                   HttpContext.Current.Session != null &&
                   HttpContext.Current.Session["UsuarioId"] != null;
        }

        public static string RolActual()
        {
            if (HttpContext.Current == null || HttpContext.Current.Session == null)
            {
                return string.Empty;
            }

            object rol = HttpContext.Current.Session["RolDescripcion"];
            return rol == null ? string.Empty : rol.ToString();
        }

        public static void IniciarSesion(Usuario usuario)
        {
            HttpContext.Current.Session["UsuarioId"] = usuario.IdUsuario;
            HttpContext.Current.Session["NombreUsuario"] = usuario.NombreUsuario;
            HttpContext.Current.Session["EmpleadoNombreCompleto"] = usuario.EmpleadoNombreCompleto;
            HttpContext.Current.Session["RolDescripcion"] = usuario.RolDescripcion;
        }

        public static void CerrarSesion()
        {
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session.Abandon();
        }

        public static void RequiereLogin(Page page)
        {
            if (!EstaLogueado())
            {
                page.Response.Redirect("~/Login.aspx", true);
            }
        }

        public static void RequiereRol(Page page, params string[] rolesPermitidos)
        {
            RequiereLogin(page);

            string rol = RolActual();
            bool tienePermiso = rolesPermitidos.Any(r => string.Equals(r, rol, StringComparison.OrdinalIgnoreCase));

            if (!tienePermiso)
            {
                page.Response.Redirect("~/Default.aspx?sinPermiso=1", true);
            }
        }

        public static bool EsAdministrador()
        {
            return string.Equals(RolActual(), RolAdministrador, StringComparison.OrdinalIgnoreCase);
        }
    }
}
