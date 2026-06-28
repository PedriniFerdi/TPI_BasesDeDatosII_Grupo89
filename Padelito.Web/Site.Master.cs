using System;

namespace Padelito.Web
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SeguridadSesion.RequiereLogin(Page);
            ConfigurarUsuario();
            ConfigurarMenu();
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            SeguridadSesion.CerrarSesion();
            Response.Redirect("~/Login.aspx", true);
        }

        private void ConfigurarUsuario()
        {
            string nombreUsuario = Convert.ToString(Session["NombreUsuario"]);
            string empleado = Convert.ToString(Session["EmpleadoNombreCompleto"]);
            string rol = Convert.ToString(Session["RolDescripcion"]);

            litNombreUsuario.Text = string.IsNullOrWhiteSpace(empleado) ? nombreUsuario : empleado;
            litRolUsuario.Text = rol;
            lblInicialesUsuario.Text = ObtenerIniciales(nombreUsuario);
        }

        private void ConfigurarMenu()
        {
            bool esAdministrador = SeguridadSesion.EsAdministrador();

            lnkEmpleados.Visible = esAdministrador;
            lnkTiposCancha.Visible = esAdministrador;
            lnkCanchas.Visible = esAdministrador;
            lnkTurnosDisponibles.Visible = esAdministrador;
            lnkPromociones.Visible = esAdministrador;
            lnkUsuarios.Visible = esAdministrador;
            lnkAuditoria.Visible = esAdministrador;
        }

        private static string ObtenerIniciales(string nombreUsuario)
        {
            if (string.IsNullOrWhiteSpace(nombreUsuario))
            {
                return "US";
            }

            return nombreUsuario.Trim().Length == 1
                ? nombreUsuario.Trim().Substring(0, 1).ToUpperInvariant()
                : nombreUsuario.Trim().Substring(0, 2).ToUpperInvariant();
        }
    }
}
