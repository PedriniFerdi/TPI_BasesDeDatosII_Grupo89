using System;
using Padelito.Dominio.Entidades;
using Padelito.Negocio.Servicios;

namespace Padelito.Web
{
    public partial class Clientes : System.Web.UI.Page
    {
        private readonly ClienteNegocio _clienteNegocio = new ClienteNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            SeguridadSesion.RequiereRol(Page, SeguridadSesion.RolAdministrador, SeguridadSesion.RolEmpleado);

            if (!IsPostBack)
            {
                CargarGrilla();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                var cliente = ObtenerClienteDesdeFormulario();
                _clienteNegocio.Guardar(cliente);

                LimpiarFormulario();
                CargarGrilla();
                MostrarMensaje("Cliente guardado correctamente.", false);
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

        protected void gvClientes_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int idCliente = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "EditarCliente")
            {
                CargarClienteParaEditar(idCliente);
            }

            if (e.CommandName == "EliminarCliente")
            {
                _clienteNegocio.EliminarLogico(idCliente);
                CargarGrilla();
                MostrarMensaje("Cliente dado de baja.", false);
            }
        }

        private void CargarGrilla()
        {
            gvClientes.DataSource = _clienteNegocio.Listar();
            gvClientes.DataBind();
        }

        private void CargarClienteParaEditar(int idCliente)
        {
            Cliente cliente = _clienteNegocio.ObtenerPorId(idCliente);

            if (cliente == null)
            {
                MostrarMensaje("No se encontro el cliente seleccionado.", true);
                return;
            }

            hfIdCliente.Value = cliente.IdCliente.ToString();
            txtNombre.Text = cliente.Nombre;
            txtApellido.Text = cliente.Apellido;
            txtTelefono.Text = cliente.Telefono;
            txtEmail.Text = cliente.Email;
            chkActivo.Checked = cliente.Activo;
        }

        private Cliente ObtenerClienteDesdeFormulario()
        {
            int idCliente = 0;
            int.TryParse(hfIdCliente.Value, out idCliente);

            return new Cliente
            {
                IdCliente = idCliente,
                Nombre = txtNombre.Text.Trim(),
                Apellido = txtApellido.Text.Trim(),
                Telefono = txtTelefono.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Activo = chkActivo.Checked
            };
        }

        private void LimpiarFormulario()
        {
            hfIdCliente.Value = string.Empty;
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
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
