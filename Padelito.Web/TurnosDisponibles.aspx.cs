using System;
using Padelito.Dominio.Entidades;
using Padelito.Negocio.Servicios;

namespace Padelito.Web
{
    public partial class TurnosDisponibles : System.Web.UI.Page
    {
        private readonly TurnosDisponiblesNegocio _turnosDisponiblesNegocio = new TurnosDisponiblesNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarGrilla();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Padelito.Dominio.Entidades.TurnosDisponibles turnoDisponible = ObtenerTurnoDisponibleDesdeFormulario();
                _turnosDisponiblesNegocio.Guardar(turnoDisponible);

                LimpiarFormulario();
                CargarGrilla();
                MostrarMensaje("Turno disponible guardado correctamente.", false);
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

        protected void gvTurnosDisponibles_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            try
            {
                int idTurnoDisponible = Convert.ToInt32(e.CommandArgument);

                if (e.CommandName == "EditarTurnoDisponible")
                {
                    CargarTurnoDisponibleParaEditar(idTurnoDisponible);
                }

                if (e.CommandName == "EliminarTurnoDisponible")
                {
                    _turnosDisponiblesNegocio.EliminarLogico(idTurnoDisponible);
                    LimpiarFormulario();
                    CargarGrilla();
                    MostrarMensaje("Turno disponible dado de baja.", false);
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }

        private void CargarGrilla()
        {
            gvTurnosDisponibles.DataSource = _turnosDisponiblesNegocio.Listar();
            gvTurnosDisponibles.DataBind();
        }

        private void CargarTurnoDisponibleParaEditar(int idTurnoDisponible)
        {
            Padelito.Dominio.Entidades.TurnosDisponibles turnoDisponible = _turnosDisponiblesNegocio.ObtenerPorId(idTurnoDisponible);

            if (turnoDisponible == null)
            {
                MostrarMensaje("No se encontro el turno disponible seleccionado.", true);
                return;
            }

            hfIdTurnoDisponible.Value = turnoDisponible.IdTurnoDisponible.ToString();
            txtHoraInicio.Text = turnoDisponible.HoraInicio.ToString(@"hh\:mm");
            txtHoraFin.Text = turnoDisponible.HoraFin.ToString(@"hh\:mm");
            chkActivo.Checked = turnoDisponible.Activo;
        }

        private Padelito.Dominio.Entidades.TurnosDisponibles ObtenerTurnoDisponibleDesdeFormulario()
        {
            int idTurnoDisponible = 0;
            int.TryParse(hfIdTurnoDisponible.Value, out idTurnoDisponible);

            TimeSpan horaInicio;
            if (!TimeSpan.TryParse(txtHoraInicio.Text.Trim(), out horaInicio))
            {
                throw new ArgumentException("La hora de inicio debe ser valida.");
            }

            TimeSpan horaFin;
            if (!TimeSpan.TryParse(txtHoraFin.Text.Trim(), out horaFin))
            {
                throw new ArgumentException("La hora de fin debe ser valida.");
            }

            return new Padelito.Dominio.Entidades.TurnosDisponibles
            {
                IdTurnoDisponible = idTurnoDisponible,
                HoraInicio = horaInicio,
                HoraFin = horaFin,
                Activo = chkActivo.Checked
            };
        }

        private void LimpiarFormulario()
        {
            hfIdTurnoDisponible.Value = string.Empty;
            txtHoraInicio.Text = string.Empty;
            txtHoraFin.Text = string.Empty;
            chkActivo.Checked = true;
        }

        private void MostrarMensaje(string mensaje, bool esError)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.CssClass = esError ? "text-danger" : "text-success";
        }
    }
}
