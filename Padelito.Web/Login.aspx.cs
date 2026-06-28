using System;
using Padelito.Dominio.Entidades;
using Padelito.Negocio.Servicios;

namespace Padelito.Web
{
    public partial class Login : System.Web.UI.Page
    {
        private readonly UsuarioNegocio _usuarioNegocio = new UsuarioNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && SeguridadSesion.EstaLogueado())
            {
                Response.Redirect("~/Default.aspx", true);
            }
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario usuario = _usuarioNegocio.Login(txtNombreUsuario.Text, txtContrasenia.Text);

                if (!EsRolPermitido(usuario.RolDescripcion))
                {
                    MostrarMensaje("El rol del usuario no esta habilitado para ingresar.", true);
                    return;
                }

                SeguridadSesion.IniciarSesion(usuario);
                Response.Redirect("~/Default.aspx", true);
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }

        private static bool EsRolPermitido(string rol)
        {
            return string.Equals(rol, SeguridadSesion.RolAdministrador, StringComparison.OrdinalIgnoreCase) ||
                   string.Equals(rol, SeguridadSesion.RolEmpleado, StringComparison.OrdinalIgnoreCase);
        }

        private void MostrarMensaje(string mensaje, bool esError)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.CssClass = esError ? "alert alert-danger d-block" : "alert alert-success d-block";
        }
    }
}
