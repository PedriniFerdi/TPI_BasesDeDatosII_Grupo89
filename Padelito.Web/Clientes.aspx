<%@ Page Title="Clientes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Clientes.aspx.cs" Inherits="Padelito.Web.Clientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row g-4">
        <div class="col-lg-4">
            <div class="bg-white border rounded-3 p-4">
                <h1 class="h4 mb-3">Cliente</h1>

                <asp:HiddenField ID="hfIdCliente" runat="server" />

                <div class="mb-3">
                    <label class="form-label" for="<%= txtNombre.ClientID %>">Nombre</label>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" MaxLength="50" />
                </div>

                <div class="mb-3">
                    <label class="form-label" for="<%= txtApellido.ClientID %>">Apellido</label>
                    <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" MaxLength="50" />
                </div>

                <div class="mb-3">
                    <label class="form-label" for="<%= txtTelefono.ClientID %>">Telefono</label>
                    <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" MaxLength="30" />
                </div>

                <div class="mb-3">
                    <label class="form-label" for="<%= txtEmail.ClientID %>">Email</label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" MaxLength="100" />
                </div>

                <div class="form-check mb-3">
                    <asp:CheckBox ID="chkActivo" runat="server" CssClass="form-check-input" Checked="true" />
                    <label class="form-check-label" for="<%= chkActivo.ClientID %>">Activo</label>
                </div>

                <div class="d-flex gap-2">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="btnGuardar_Click" />
                    <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn btn-outline-secondary" OnClick="btnLimpiar_Click" CausesValidation="false" />
                </div>
            </div>
        </div>

        <div class="col-lg-8">
            <div class="bg-white border rounded-3 p-4">
                <div class="d-flex justify-content-between align-items-center mb-3">
                    <h2 class="h4 mb-0">Clientes registrados</h2>
                    <asp:Label ID="lblMensaje" runat="server" CssClass="text-success" />
                </div>

                <asp:GridView ID="gvClientes" runat="server"
                    CssClass="table table-striped table-hover align-middle"
                    AutoGenerateColumns="false"
                    DataKeyNames="IdCliente"
                    OnRowCommand="gvClientes_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="IdCliente" HeaderText="ID" />
                        <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                        <asp:BoundField DataField="Telefono" HeaderText="Telefono" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <asp:CheckBoxField DataField="Activo" HeaderText="Activo" />
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnEditar" runat="server"
                                    Text="Editar"
                                    CssClass="btn btn-sm btn-outline-primary me-1"
                                    CommandName="EditarCliente"
                                    CommandArgument='<%# Eval("IdCliente") %>' />
                                <asp:LinkButton ID="btnEliminar" runat="server"
                                    Text="Baja"
                                    CssClass="btn btn-sm btn-outline-danger"
                                    CommandName="EliminarCliente"
                                    CommandArgument='<%# Eval("IdCliente") %>'
                                    OnClientClick="return confirm('Desea dar de baja este cliente?');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <div class="alert alert-info mb-0">No hay clientes cargados.</div>
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
