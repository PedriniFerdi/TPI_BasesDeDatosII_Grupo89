using System;
using System.Web.UI.WebControls;
using Padelito.Dominio.Entidades;
using Padelito.Negocio.Servicios;

namespace Padelito.Web
{
    public partial class Usuarios : System.Web.UI.Page
    {
        private readonly UsuarioNegocio _usuarioNegocio = new UsuarioNegocio();
        private readonly RolNegocio _rolNegocio = new RolNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            SeguridadSesion.RequiereRol(Page, SeguridadSesion.RolAdministrador);

            if (!IsPostBack)
            {
                CargarEmpleadosDisponibles(0);
                CargarRoles();
                CargarGrilla();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario usuario = ObtenerUsuarioDesdeFormulario();
                _usuarioNegocio.Guardar(usuario);

                LimpiarFormulario();
                CargarEmpleadosDisponibles(0);
                CargarGrilla();
                MostrarMensaje("Usuario guardado correctamente.", false);
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            CargarEmpleadosDisponibles(0);
            MostrarMensaje(string.Empty, false);
        }

        protected void gvUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int idUsuario = Convert.ToInt32(e.CommandArgument);

                if (e.CommandName == "EditarUsuario")
                {
                    CargarUsuarioParaEditar(idUsuario);
                }

                if (e.CommandName == "EliminarUsuario")
                {
                    _usuarioNegocio.EliminarLogico(idUsuario);
                    LimpiarFormulario();
                    CargarEmpleadosDisponibles(0);
                    CargarGrilla();
                    MostrarMensaje("Usuario dado de baja.", false);
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }

        private void CargarEmpleadosDisponibles(int idEmpleadoActual)
        {
            var empleados = _usuarioNegocio.ListarEmpleadosDisponibles(idEmpleadoActual);

            ddlEmpleado.Items.Clear();
            ddlEmpleado.Items.Insert(0, new ListItem("Seleccione...", "0"));

            foreach (Empleado empleado in empleados)
            {
                ddlEmpleado.Items.Add(new ListItem(
                    empleado.Apellido + ", " + empleado.Nombre,
                    empleado.IdEmpleado.ToString()));
            }
        }

        private void CargarRoles()
        {
            ddlRol.DataSource = _rolNegocio.Listar();
            ddlRol.DataTextField = "Descripcion";
            ddlRol.DataValueField = "IdRol";
            ddlRol.DataBind();
            ddlRol.Items.Insert(0, new ListItem("Seleccione...", "0"));
        }

        private void CargarGrilla()
        {
            gvUsuarios.DataSource = _usuarioNegocio.Listar();
            gvUsuarios.DataBind();
        }

        private void CargarUsuarioParaEditar(int idUsuario)
        {
            Usuario usuario = _usuarioNegocio.ObtenerPorId(idUsuario);

            if (usuario == null)
            {
                MostrarMensaje("No se encontro el usuario seleccionado.", true);
                return;
            }

            CargarEmpleadosDisponibles(usuario.IdEmpleado);

            hfIdUsuario.Value = usuario.IdUsuario.ToString();
            txtNombreUsuario.Text = usuario.NombreUsuario;
            txtContrasenia.Attributes["value"] = usuario.Contrasenia;
            ddlEmpleado.SelectedValue = usuario.IdEmpleado.ToString();
            ddlRol.SelectedValue = usuario.IdRol.ToString();
            chkActivo.Checked = usuario.Activo;
        }

        private Usuario ObtenerUsuarioDesdeFormulario()
        {
            int idUsuario = 0;
            int.TryParse(hfIdUsuario.Value, out idUsuario);

            int idEmpleado = 0;
            int.TryParse(ddlEmpleado.SelectedValue, out idEmpleado);

            int idRol = 0;
            int.TryParse(ddlRol.SelectedValue, out idRol);

            return new Usuario
            {
                IdUsuario = idUsuario,
                NombreUsuario = txtNombreUsuario.Text.Trim(),
                Contrasenia = txtContrasenia.Text.Trim(),
                IdEmpleado = idEmpleado,
                IdRol = idRol,
                Activo = chkActivo.Checked
            };
        }

        private void LimpiarFormulario()
        {
            hfIdUsuario.Value = string.Empty;
            txtNombreUsuario.Text = string.Empty;
            txtContrasenia.Text = string.Empty;
            txtContrasenia.Attributes.Remove("value");
            ddlEmpleado.SelectedValue = "0";
            ddlRol.SelectedValue = "0";
            chkActivo.Checked = true;
        }

        private void MostrarMensaje(string mensaje, bool esError)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.CssClass = esError ? "text-danger" : "text-success";
        }
    }
}
