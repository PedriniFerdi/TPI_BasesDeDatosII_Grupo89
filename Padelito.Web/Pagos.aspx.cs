using System;
using System.Globalization;
using System.Web.UI.WebControls;
using Padelito.Dominio.Entidades;
using Padelito.Negocio.Servicios;

namespace Padelito.Web
{
    public partial class Pagos : System.Web.UI.Page
    {
        private readonly PagoNegocio _pagoNegocio = new PagoNegocio();
        private readonly ReservaNegocio _reservaNegocio = new ReservaNegocio();
        private readonly MetodoPagoNegocio _metodoPagoNegocio = new MetodoPagoNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCombos();
                CargarGrilla();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Pago pago = ObtenerPagoDesdeFormulario();
                _pagoNegocio.Guardar(pago);

                LimpiarFormulario();
                CargarGrilla();
                MostrarMensaje("Pago registrado correctamente.", false);
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            MostrarMensaje(string.Empty, false);
        }

        private void CargarCombos()
        {
            CargarReservas();
            CargarMetodosPago();
        }

        private void CargarReservas()
        {
            ddlReserva.Items.Clear();
            ddlReserva.Items.Insert(0, new ListItem("Seleccione...", "0"));

            foreach (ReservaDetalle reserva in _reservaNegocio.ListarDetalle())
            {
                string texto = string.Format(
                    "#{0} - {1} - {2} - {3:dd/MM/yyyy} - ${4:N2}",
                    reserva.IdReserva,
                    reserva.Cliente,
                    reserva.Cancha,
                    reserva.FechaReserva,
                    reserva.PrecioFinal);

                ddlReserva.Items.Add(new ListItem(texto, reserva.IdReserva.ToString()));
            }
        }

        private void CargarMetodosPago()
        {
            ddlMetodoPago.DataSource = _metodoPagoNegocio.Listar();
            ddlMetodoPago.DataTextField = "Descripcion";
            ddlMetodoPago.DataValueField = "IdMetodoPago";
            ddlMetodoPago.DataBind();
            ddlMetodoPago.Items.Insert(0, new ListItem("Seleccione...", "0"));
        }

        private void CargarGrilla()
        {
            gvPagos.DataSource = _pagoNegocio.ListarDetalle();
            gvPagos.DataBind();
        }

        private Pago ObtenerPagoDesdeFormulario()
        {
            int idReserva = 0;
            int.TryParse(ddlReserva.SelectedValue, out idReserva);

            int idMetodoPago = 0;
            int.TryParse(ddlMetodoPago.SelectedValue, out idMetodoPago);

            decimal monto;
            if (!decimal.TryParse(txtMonto.Text.Trim(), NumberStyles.Number, CultureInfo.CurrentCulture, out monto) &&
                !decimal.TryParse(txtMonto.Text.Trim(), NumberStyles.Number, CultureInfo.InvariantCulture, out monto))
            {
                throw new ArgumentException("El monto debe ser un numero valido.");
            }

            return new Pago
            {
                IdReserva = idReserva,
                IdMetodoPago = idMetodoPago,
                Monto = monto,
                Observacion = txtObservacion.Text.Trim()
            };
        }

        private void LimpiarFormulario()
        {
            ddlReserva.SelectedValue = "0";
            ddlMetodoPago.SelectedValue = "0";
            txtMonto.Text = string.Empty;
            txtObservacion.Text = string.Empty;
        }

        private void MostrarMensaje(string mensaje, bool esError)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.CssClass = esError ? "text-danger" : "text-success";
        }
    }
}
