using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Padelito.Dominio.Entidades;
using Padelito.Negocio.Servicios;

namespace Padelito.Web
{
    public partial class Reservas : System.Web.UI.Page
    {
        private readonly ReservaNegocio _reservaNegocio = new ReservaNegocio();
        private readonly ClienteNegocio _clienteNegocio = new ClienteNegocio();
        private readonly EmpleadoNegocio _empleadoNegocio = new EmpleadoNegocio();
        private readonly TurnosDisponiblesNegocio _turnosDisponiblesNegocio = new TurnosDisponiblesNegocio();
        private readonly PromocionNegocio _promocionNegocio = new PromocionNegocio();
        private readonly EstadoReservaNegocio _estadoReservaNegocio = new EstadoReservaNegocio();

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
                Reserva reserva = ObtenerReservaDesdeFormulario();
                _reservaNegocio.Guardar(reserva);

                LimpiarFormulario();
                CargarGrilla();
                MostrarMensaje("Reserva guardada correctamente.", false);
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

        protected void gvReservas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName != "CambiarEstado")
                {
                    return;
                }

                int idReserva = Convert.ToInt32(e.CommandArgument);
                GridViewRow fila = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                DropDownList ddlEstadoGrilla = (DropDownList)fila.FindControl("ddlEstadoGrilla");

                int idEstadoReserva = 0;
                int.TryParse(ddlEstadoGrilla.SelectedValue, out idEstadoReserva);

                _reservaNegocio.CambiarEstado(idReserva, idEstadoReserva);
                CargarGrilla();
                MostrarMensaje("Estado de reserva actualizado.", false);
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }

        private void CargarCombos()
        {
            CargarClientes();
            CargarTurnosDisponibles();
            CargarEmpleados();
            CargarEstados();
            CargarPromociones();
        }

        private void CargarClientes()
        {
            ddlCliente.Items.Clear();
            ddlCliente.Items.Insert(0, new ListItem("Seleccione...", "0"));

            foreach (Cliente cliente in _clienteNegocio.Listar())
            {
                if (cliente.Activo)
                {
                    ddlCliente.Items.Add(new ListItem(cliente.Apellido + ", " + cliente.Nombre, cliente.IdCliente.ToString()));
                }
            }
        }

        private void CargarTurnosDisponibles()
        {
            ddlTurnoDisponible.Items.Clear();
            ddlTurnoDisponible.Items.Insert(0, new ListItem("Seleccione...", "0"));

            foreach (Padelito.Dominio.Entidades.TurnosDisponibles turno in _turnosDisponiblesNegocio.Listar())
            {
                if (turno.Activo)
                {
                    ddlTurnoDisponible.Items.Add(new ListItem(
                        turno.NombreCancha + " - " + turno.Horario,
                        turno.IdTurnoDisponible.ToString()));
                }
            }
        }

        private void CargarEmpleados()
        {
            ddlEmpleado.Items.Clear();
            ddlEmpleado.Items.Insert(0, new ListItem("Seleccione...", "0"));

            foreach (Empleado empleado in _empleadoNegocio.Listar())
            {
                if (empleado.Activo)
                {
                    ddlEmpleado.Items.Add(new ListItem(empleado.Apellido + ", " + empleado.Nombre, empleado.IdEmpleado.ToString()));
                }
            }
        }

        private void CargarEstados()
        {
            ddlEstadoReserva.DataSource = _estadoReservaNegocio.Listar();
            ddlEstadoReserva.DataTextField = "Descripcion";
            ddlEstadoReserva.DataValueField = "IdEstadoReserva";
            ddlEstadoReserva.DataBind();
            ddlEstadoReserva.Items.Insert(0, new ListItem("Seleccione...", "0"));
        }

        private void CargarPromociones()
        {
            ddlPromocion.Items.Clear();
            ddlPromocion.Items.Insert(0, new ListItem("Sin promocion", "0"));

            foreach (Promocion promocion in _promocionNegocio.Listar())
            {
                if (promocion.Activa)
                {
                    ddlPromocion.Items.Add(new ListItem(promocion.Nombre, promocion.IdPromocion.ToString()));
                }
            }
        }

        private void CargarGrilla()
        {
            gvReservas.DataSource = _reservaNegocio.ListarDetalle();
            gvReservas.DataBind();
            CargarEstadosEnGrilla();
        }

        private void CargarEstadosEnGrilla()
        {
            var estados = _estadoReservaNegocio.Listar();

            foreach (GridViewRow fila in gvReservas.Rows)
            {
                DropDownList ddlEstadoGrilla = (DropDownList)fila.FindControl("ddlEstadoGrilla");

                if (ddlEstadoGrilla != null)
                {
                    ddlEstadoGrilla.DataSource = estados;
                    ddlEstadoGrilla.DataTextField = "Descripcion";
                    ddlEstadoGrilla.DataValueField = "IdEstadoReserva";
                    ddlEstadoGrilla.DataBind();
                    ddlEstadoGrilla.Items.Insert(0, new ListItem("Seleccione...", "0"));
                }
            }
        }

        private Reserva ObtenerReservaDesdeFormulario()
        {
            int idCliente = 0;
            int.TryParse(ddlCliente.SelectedValue, out idCliente);

            int idTurnoDisponible = 0;
            int.TryParse(ddlTurnoDisponible.SelectedValue, out idTurnoDisponible);

            int idEmpleado = 0;
            int.TryParse(ddlEmpleado.SelectedValue, out idEmpleado);

            int idEstadoReserva = 0;
            int.TryParse(ddlEstadoReserva.SelectedValue, out idEstadoReserva);

            int idPromocion = 0;
            int.TryParse(ddlPromocion.SelectedValue, out idPromocion);

            DateTime fechaReserva;
            if (!DateTime.TryParseExact(txtFechaReserva.Text.Trim(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out fechaReserva))
            {
                throw new ArgumentException("La fecha de reserva debe ser valida.");
            }

            return new Reserva
            {
                IdCliente = idCliente,
                IdTurnoDisponible = idTurnoDisponible,
                IdEmpleado = idEmpleado,
                IdEstadoReserva = idEstadoReserva,
                IdPromocion = idPromocion > 0 ? (int?)idPromocion : null,
                FechaReserva = fechaReserva
            };
        }

        private void LimpiarFormulario()
        {
            ddlCliente.SelectedValue = "0";
            ddlTurnoDisponible.SelectedValue = "0";
            ddlEmpleado.SelectedValue = "0";
            ddlEstadoReserva.SelectedValue = "0";
            ddlPromocion.SelectedValue = "0";
            txtFechaReserva.Text = string.Empty;
        }

        private void MostrarMensaje(string mensaje, bool esError)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.CssClass = esError ? "text-danger" : "text-success";
        }
    }
}
