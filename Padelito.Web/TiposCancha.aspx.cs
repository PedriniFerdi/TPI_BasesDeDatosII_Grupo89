using System;
using Padelito.Dominio.Entidades;
using Padelito.Negocio.Servicios;

namespace Padelito.Web
{
    public partial class TiposCancha : System.Web.UI.Page
    {
        private readonly TiposCanchaNegocio _tiposCanchaNegocio = new TiposCanchaNegocio();

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
                Padelito.Dominio.Entidades.TiposCancha tipoCancha = ObtenerTipoCanchaDesdeFormulario();
                _tiposCanchaNegocio.Guardar(tipoCancha);

                LimpiarFormulario();
                CargarGrilla();
                MostrarMensaje("Tipo de cancha guardado correctamente.", false);
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

        protected void gvTiposCancha_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            try
            {
                int idTipoCancha = Convert.ToInt32(e.CommandArgument);

                if (e.CommandName == "EditarTipoCancha")
                {
                    CargarTipoCanchaParaEditar(idTipoCancha);
                }

                if (e.CommandName == "EliminarTipoCancha")
                {
                    _tiposCanchaNegocio.Eliminar(idTipoCancha);
                    LimpiarFormulario();
                    CargarGrilla();
                    MostrarMensaje("Tipo de cancha eliminado.", false);
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }

        private void CargarGrilla()
        {
            gvTiposCancha.DataSource = _tiposCanchaNegocio.Listar();
            gvTiposCancha.DataBind();
        }

        private void CargarTipoCanchaParaEditar(int idTipoCancha)
        {
            Padelito.Dominio.Entidades.TiposCancha tipoCancha = _tiposCanchaNegocio.ObtenerPorId(idTipoCancha);

            if (tipoCancha == null)
            {
                MostrarMensaje("No se encontro el tipo de cancha seleccionado.", true);
                return;
            }

            hfIdTipoCancha.Value = tipoCancha.IdTipoCancha.ToString();
            txtDescripcion.Text = tipoCancha.Descripcion;
        }

        private Padelito.Dominio.Entidades.TiposCancha ObtenerTipoCanchaDesdeFormulario()
        {
            int idTipoCancha = 0;
            int.TryParse(hfIdTipoCancha.Value, out idTipoCancha);

            return new Padelito.Dominio.Entidades.TiposCancha
            {
                IdTipoCancha = idTipoCancha,
                Descripcion = txtDescripcion.Text.Trim()
            };
        }

        private void LimpiarFormulario()
        {
            hfIdTipoCancha.Value = string.Empty;
            txtDescripcion.Text = string.Empty;
        }

        private void MostrarMensaje(string mensaje, bool esError)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.CssClass = esError ? "text-danger" : "text-success";
        }
    }
}
