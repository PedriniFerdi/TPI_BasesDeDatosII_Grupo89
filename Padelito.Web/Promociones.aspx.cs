using System;
using System.Globalization;
using Padelito.Dominio.Entidades;
using Padelito.Negocio.Servicios;

namespace Padelito.Web
{
    public partial class Promociones : System.Web.UI.Page
    {
        private readonly PromocionNegocio _promocionNegocio = new PromocionNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            SeguridadSesion.RequiereRol(Page, SeguridadSesion.RolAdministrador);

            if (!IsPostBack)
            {
                CargarGrilla();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Promocion promocion = ObtenerPromocionDesdeFormulario();
                _promocionNegocio.Guardar(promocion);

                LimpiarFormulario();
                CargarGrilla();
                MostrarMensaje("Promocion guardada correctamente.", false);
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

        protected void gvPromociones_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            try
            {
                int idPromocion = Convert.ToInt32(e.CommandArgument);

                if (e.CommandName == "EditarPromocion")
                {
                    CargarPromocionParaEditar(idPromocion);
                }

                if (e.CommandName == "EliminarPromocion")
                {
                    _promocionNegocio.EliminarLogico(idPromocion);
                    LimpiarFormulario();
                    CargarGrilla();
                    MostrarMensaje("Promocion dada de baja.", false);
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }

        private void CargarGrilla()
        {
            gvPromociones.DataSource = _promocionNegocio.Listar();
            gvPromociones.DataBind();
        }

        private void CargarPromocionParaEditar(int idPromocion)
        {
            Promocion promocion = _promocionNegocio.ObtenerPorId(idPromocion);

            if (promocion == null)
            {
                MostrarMensaje("No se encontro la promocion seleccionada.", true);
                return;
            }

            hfIdPromocion.Value = promocion.IdPromocion.ToString();
            txtNombre.Text = promocion.Nombre;
            txtDescripcion.Text = promocion.Descripcion;
            txtPorcentajeDescuento.Text = promocion.PorcentajeDescuento.ToString("0.00");
            txtFechaDesde.Text = promocion.FechaDesde.ToString("yyyy-MM-dd");
            txtFechaHasta.Text = promocion.FechaHasta.ToString("yyyy-MM-dd");
            chkActiva.Checked = promocion.Activa;
        }

        private Promocion ObtenerPromocionDesdeFormulario()
        {
            int idPromocion = 0;
            int.TryParse(hfIdPromocion.Value, out idPromocion);

            decimal porcentajeDescuento;
            if (!decimal.TryParse(txtPorcentajeDescuento.Text.Trim(), NumberStyles.Number, CultureInfo.CurrentCulture, out porcentajeDescuento))
            {
                throw new ArgumentException("El porcentaje de descuento debe ser un numero valido.");
            }

            DateTime fechaDesde;
            if (!DateTime.TryParseExact(txtFechaDesde.Text.Trim(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out fechaDesde))
            {
                throw new ArgumentException("La fecha desde debe ser valida.");
            }

            DateTime fechaHasta;
            if (!DateTime.TryParseExact(txtFechaHasta.Text.Trim(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out fechaHasta))
            {
                throw new ArgumentException("La fecha hasta debe ser valida.");
            }

            return new Promocion
            {
                IdPromocion = idPromocion,
                Nombre = txtNombre.Text.Trim(),
                Descripcion = txtDescripcion.Text.Trim(),
                PorcentajeDescuento = porcentajeDescuento,
                FechaDesde = fechaDesde,
                FechaHasta = fechaHasta,
                Activa = chkActiva.Checked
            };
        }

        private void LimpiarFormulario()
        {
            hfIdPromocion.Value = string.Empty;
            txtNombre.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtPorcentajeDescuento.Text = string.Empty;
            txtFechaDesde.Text = string.Empty;
            txtFechaHasta.Text = string.Empty;
            chkActiva.Checked = true;
        }

        private void MostrarMensaje(string mensaje, bool esError)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.CssClass = esError ? "text-danger" : "text-success";
        }
    }
}
