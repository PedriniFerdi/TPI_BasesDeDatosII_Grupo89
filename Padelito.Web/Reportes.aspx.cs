using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI.WebControls;
using Padelito.Dominio.Entidades;
using Padelito.Negocio.Servicios;

namespace Padelito.Web
{
    public partial class Reportes : System.Web.UI.Page
    {
        private readonly ReporteNegocio _reporteNegocio = new ReporteNegocio();
        private readonly EstadoReservaNegocio _estadoReservaNegocio = new EstadoReservaNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarEstados();
                CargarFechasIniciales();
                CargarReporte();
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarReporte();
        }

        private void CargarEstados()
        {
            ddlEstadoReserva.DataSource = _estadoReservaNegocio.Listar();
            ddlEstadoReserva.DataTextField = "Descripcion";
            ddlEstadoReserva.DataValueField = "IdEstadoReserva";
            ddlEstadoReserva.DataBind();
            ddlEstadoReserva.Items.Insert(0, new ListItem("Todos", "0"));
        }

        private void CargarFechasIniciales()
        {
            DateTime hoy = DateTime.Today;
            txtFechaDesde.Text = hoy.ToString("yyyy-MM-dd");
            txtFechaHasta.Text = hoy.ToString("yyyy-MM-dd");
        }

        private void CargarReporte()
        {
            try
            {
                DateTime fechaDesde = ObtenerFecha(txtFechaDesde.Text, "desde");
                DateTime fechaHasta = ObtenerFecha(txtFechaHasta.Text, "hasta");
                int? idEstadoReserva = ObtenerEstadoSeleccionado();

                List<ReporteReserva> reservas = _reporteNegocio.ListarReservasPorFecha(
                    fechaDesde,
                    fechaHasta,
                    idEstadoReserva);

                gvReporteReservas.DataSource = reservas;
                gvReporteReservas.DataBind();
                MostrarTotales(reservas);
                MostrarMensaje(string.Empty, false);
            }
            catch (Exception ex)
            {
                gvReporteReservas.DataSource = null;
                gvReporteReservas.DataBind();
                MostrarTotales(new List<ReporteReserva>());
                MostrarMensaje(ex.Message, true);
            }
        }

        private static DateTime ObtenerFecha(string valor, string nombreCampo)
        {
            DateTime fecha;
            if (!DateTime.TryParseExact(valor.Trim(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out fecha))
            {
                throw new ArgumentException("Debe ingresar una fecha " + nombreCampo + " valida.");
            }

            return fecha;
        }

        private int? ObtenerEstadoSeleccionado()
        {
            int idEstadoReserva = 0;
            int.TryParse(ddlEstadoReserva.SelectedValue, out idEstadoReserva);

            return idEstadoReserva > 0 ? (int?)idEstadoReserva : null;
        }

        private void MostrarTotales(List<ReporteReserva> reservas)
        {
            decimal totalPrecioFinal = 0;

            foreach (ReporteReserva reserva in reservas)
            {
                totalPrecioFinal += reserva.PrecioFinal;
            }

            lblCantidadReservas.Text = reservas.Count.ToString();
            lblTotalPrecioFinal.Text = "$" + totalPrecioFinal.ToString("N2", CultureInfo.CurrentCulture);
        }

        private void MostrarMensaje(string mensaje, bool esError)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.CssClass = esError ? "d-block mt-3 text-danger" : "d-block mt-3 text-success";
        }
    }
}
