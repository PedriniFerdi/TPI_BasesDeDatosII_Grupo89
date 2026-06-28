using System;
using Padelito.Negocio.Servicios;

namespace Padelito.Web
{
    public partial class AuditoriaReservas : System.Web.UI.Page
    {
        private readonly AuditoriaReservaNegocio _auditoriaReservaNegocio = new AuditoriaReservaNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            SeguridadSesion.RequiereRol(Page, SeguridadSesion.RolAdministrador);

            if (!IsPostBack)
            {
                CargarAuditoria();
            }
        }

        private void CargarAuditoria()
        {
            try
            {
                gvAuditoriaReservas.DataSource = _auditoriaReservaNegocio.Listar();
                gvAuditoriaReservas.DataBind();
                MostrarMensaje(string.Empty, false);
            }
            catch (Exception ex)
            {
                gvAuditoriaReservas.DataSource = null;
                gvAuditoriaReservas.DataBind();
                MostrarMensaje(ex.Message, true);
            }
        }

        private void MostrarMensaje(string mensaje, bool esError)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.CssClass = esError ? "d-block mt-3 text-danger" : "d-block mt-3 text-success";
        }
    }
}
