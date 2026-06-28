using System;
using System.Globalization;
using Padelito.Dominio.Entidades;
using Padelito.Negocio.Servicios;

namespace Padelito.Web
{
    public partial class Canchas : System.Web.UI.Page
    {
        private readonly CanchaNegocio _canchaNegocio = new CanchaNegocio();
        private readonly TiposCanchaNegocio _tiposCanchaNegocio = new TiposCanchaNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            SeguridadSesion.RequiereRol(Page, SeguridadSesion.RolAdministrador);

            if (!IsPostBack)
            {
                CargarTiposCancha();
                CargarGrilla();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Cancha cancha = ObtenerCanchaDesdeFormulario();
                _canchaNegocio.Guardar(cancha);

                LimpiarFormulario();
                CargarGrilla();
                MostrarMensaje("Cancha guardada correctamente.", false);
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

        protected void gvCanchas_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            try
            {
                int idCancha = Convert.ToInt32(e.CommandArgument);

                if (e.CommandName == "EditarCancha")
                {
                    CargarCanchaParaEditar(idCancha);
                }

                if (e.CommandName == "EliminarCancha")
                {
                    _canchaNegocio.EliminarLogico(idCancha);
                    LimpiarFormulario();
                    CargarGrilla();
                    MostrarMensaje("Cancha dada de baja.", false);
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }

        private void CargarTiposCancha()
        {
            ddlTipoCancha.DataSource = _tiposCanchaNegocio.Listar();
            ddlTipoCancha.DataTextField = "Descripcion";
            ddlTipoCancha.DataValueField = "IdTipoCancha";
            ddlTipoCancha.DataBind();
            ddlTipoCancha.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccione...", "0"));
        }

        private void CargarGrilla()
        {
            gvCanchas.DataSource = _canchaNegocio.Listar();
            gvCanchas.DataBind();
        }

        private void CargarCanchaParaEditar(int idCancha)
        {
            Cancha cancha = _canchaNegocio.ObtenerPorId(idCancha);

            if (cancha == null)
            {
                MostrarMensaje("No se encontro la cancha seleccionada.", true);
                return;
            }

            hfIdCancha.Value = cancha.IdCancha.ToString();
            txtNombre.Text = cancha.Nombre;
            ddlTipoCancha.SelectedValue = cancha.IdTipoCancha.ToString();
            txtPrecioHora.Text = cancha.PrecioHora.ToString("N2", CultureInfo.CurrentCulture);
            chkActiva.Checked = cancha.Activa;
        }

        private Cancha ObtenerCanchaDesdeFormulario()
        {
            int idCancha = 0;
            int.TryParse(hfIdCancha.Value, out idCancha);

            int idTipoCancha = 0;
            int.TryParse(ddlTipoCancha.SelectedValue, out idTipoCancha);

            decimal precioHora;
            if (!decimal.TryParse(txtPrecioHora.Text.Trim(), NumberStyles.Number, CultureInfo.CurrentCulture, out precioHora))
            {
                throw new ArgumentException("El precio por hora debe ser un numero valido.");
            }

            return new Cancha
            {
                IdCancha = idCancha,
                Nombre = txtNombre.Text.Trim(),
                IdTipoCancha = idTipoCancha,
                PrecioHora = precioHora,
                Activa = chkActiva.Checked
            };
        }

        private void LimpiarFormulario()
        {
            hfIdCancha.Value = string.Empty;
            txtNombre.Text = string.Empty;
            ddlTipoCancha.SelectedValue = "0";
            txtPrecioHora.Text = string.Empty;
            chkActiva.Checked = true;
        }

        private void MostrarMensaje(string mensaje, bool esError)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.CssClass = esError ? "text-danger" : "text-success";
        }
    }
}
