using System;
using Padelito.Dominio.Entidades;
using Padelito.Negocio.Servicios;

namespace Padelito.Web
{
    public partial class Empleados : System.Web.UI.Page
    {
        private readonly EmpleadoNegocio _empleadoNegocio = new EmpleadoNegocio();

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
                var empleado = ObtenerEmpleadoDesdeFormulario();
                _empleadoNegocio.Guardar(empleado);

                LimpiarFormulario();
                CargarGrilla();
                MostrarMensaje("Empleado guardado correctamente.", false);
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

        protected void gvEmpleados_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int idEmpleado = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "EditarEmpleado")
            {
                CargarEmpleadoParaEditar(idEmpleado);
            }

            if (e.CommandName == "EliminarEmpleado")
            {
                _empleadoNegocio.EliminarLogico(idEmpleado);
                CargarGrilla();
                MostrarMensaje("Empleado dado de baja.", false);
            }
        }

        private void CargarGrilla()
        {
            gvEmpleados.DataSource = _empleadoNegocio.Listar();
            gvEmpleados.DataBind();
        }

        private void CargarEmpleadoParaEditar(int idEmpleado)
        {
            Empleado empleado = _empleadoNegocio.ObtenerPorId(idEmpleado);

            if (empleado == null)
            {
                MostrarMensaje("No se encontro el empleado seleccionado.", true);
                return;
            }

            hfIdEmpleado.Value = empleado.IdEmpleado.ToString();
            txtNombre.Text = empleado.Nombre;
            txtApellido.Text = empleado.Apellido;
            txtDNI.Text = empleado.DNI;
            txtTelefono.Text = empleado.Telefono;
            txtEmail.Text = empleado.Email;
            chkActivo.Checked = empleado.Activo;
        }

        private Empleado ObtenerEmpleadoDesdeFormulario()
        {
            int idEmpleado = 0;
            int.TryParse(hfIdEmpleado.Value, out idEmpleado);

            return new Empleado
            {
                IdEmpleado = idEmpleado,
                Nombre = txtNombre.Text.Trim(),
                Apellido = txtApellido.Text.Trim(),
                DNI = txtDNI.Text.Trim(),
                Telefono = txtTelefono.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Activo = chkActivo.Checked
            };
        }

        private void LimpiarFormulario()
        {
            hfIdEmpleado.Value = string.Empty;
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtDNI.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtEmail.Text = string.Empty;
            chkActivo.Checked = true;
        }

        private void MostrarMensaje(string mensaje, bool esError)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.CssClass = esError ? "text-danger" : "text-success";
        }
    }
}
