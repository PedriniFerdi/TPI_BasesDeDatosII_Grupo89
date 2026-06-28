using System;
using System.Globalization;
using Padelito.Dominio.Entidades;
using Padelito.Negocio.Servicios;

namespace Padelito.Web
{
    public partial class Default : System.Web.UI.Page
    {
        private readonly DashboardNegocio _dashboardNegocio = new DashboardNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            SeguridadSesion.RequiereRol(Page, SeguridadSesion.RolAdministrador, SeguridadSesion.RolEmpleado);

            if (!IsPostBack)
            {
                pnlSinPermiso.Visible = Request.QueryString["sinPermiso"] == "1";
                pnlAccesoAdmin.Visible = SeguridadSesion.EsAdministrador();
                CargarDashboard();
            }
        }

        private void CargarDashboard()
        {
            DashboardResumen resumen = _dashboardNegocio.ObtenerResumen();
            CultureInfo culturaArgentina = new CultureInfo("es-AR");

            litClientesActivos.Text = resumen.ClientesActivos.ToString();
            litCanchasActivas.Text = resumen.CanchasActivas.ToString();
            litReservasDelDia.Text = resumen.ReservasDelDia.ToString();
            litFechaActualSistema.Text = DateTime.Now.ToString("dd/MM/yyyy", culturaArgentina);
            litIngresosRegistrados.Text = resumen.IngresosRegistrados.ToString("C", culturaArgentina);
            litPagosRegistrados.Text = resumen.PagosRegistrados.ToString();

            gvUltimasReservas.DataSource = resumen.UltimasReservas;
            gvUltimasReservas.DataBind();
        }
    }
}
