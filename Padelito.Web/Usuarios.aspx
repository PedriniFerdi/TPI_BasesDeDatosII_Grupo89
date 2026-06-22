<%@ Page Title="Usuarios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="Padelito.Web.Usuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row g-4">
        <div class="col-lg-4">
            <div class="bg-white border rounded-3 p-4">
                <h1 class="h4 mb-3">Usuario</h1>

                <asp:HiddenField ID="hfIdUsuario" runat="server" />

                <div class="mb-3">
                    <label class="form-label" for="<%= txtNombreUsuario.ClientID %>">Nombre de usuario</label>
                    <asp:TextBox ID="txtNombreUsuario" runat="server" CssClass="form-control" MaxLength="20" />
                </div>

                <div class="mb-3">
                    <label class="form-label" for="<%= txtContrasenia.ClientID %>">Contraseña</label>
                    <asp:TextBox ID="txtContrasenia" runat="server" CssClass="form-control" MaxLength="15" TextMode="Password" />
                </div>

                <div class="mb-3">
                    <label class="form-label" for="<%= ddlEmpleado.ClientID %>">Empleado</label>
                    <asp:DropDownList ID="ddlEmpleado" runat="server" CssClass="form-select" />
                </div>

                <div class="mb-3">
                    <label class="form-label" for="<%= ddlRol.ClientID %>">Rol</label>
                    <asp:DropDownList ID="ddlRol" runat="server" CssClass="form-select" />
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
                    <h2 class="h4 mb-0">Usuarios registrados</h2>
                    <asp:Label ID="lblMensaje" runat="server" CssClass="text-success" />
                </div>

                <asp:GridView ID="gvUsuarios" runat="server"
                    CssClass="table table-striped table-hover align-middle"
                    AutoGenerateColumns="false"
                    DataKeyNames="IdUsuario"
                    OnRowCommand="gvUsuarios_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="IdUsuario" HeaderText="ID" />
                        <asp:BoundField DataField="NombreUsuario" HeaderText="Usuario" />
                        <asp:BoundField DataField="EmpleadoNombreCompleto" HeaderText="Empleado" />
                        <asp:BoundField DataField="RolDescripcion" HeaderText="Rol" />
                        <asp:CheckBoxField DataField="Activo" HeaderText="Activo" />
                        <asp:BoundField DataField="FechaAlta" HeaderText="Fecha alta" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnEditar" runat="server"
                                    Text="Editar"
                                    CssClass="btn btn-sm btn-outline-primary me-1"
                                    CommandName="EditarUsuario"
                                    CommandArgument='<%# Eval("IdUsuario") %>' />
                                <asp:LinkButton ID="btnEliminar" runat="server"
                                    Text="Baja"
                                    CssClass="btn btn-sm btn-outline-danger"
                                    CommandName="EliminarUsuario"
                                    CommandArgument='<%# Eval("IdUsuario") %>'
                                    OnClientClick="return confirm('Desea dar de baja este usuario?');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <div class="alert alert-info mb-0">No hay usuarios cargados.</div>
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
