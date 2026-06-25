<%@ Page Title="Canchas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Canchas.aspx.cs" Inherits="Padelito.Web.Canchas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row g-4">
        <div class="col-lg-4">
            <div class="bg-white border rounded-3 p-4">
                <h1 class="h4 mb-3">Cancha</h1>

                <asp:HiddenField ID="hfIdCancha" runat="server" />

                <div class="mb-3">
                    <label class="form-label" for="<%= txtNombre.ClientID %>">Nombre</label>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" MaxLength="80" />
                </div>

                <div class="mb-3">
                    <label class="form-label" for="<%= ddlTipoCancha.ClientID %>">Tipo de cancha</label>
                    <asp:DropDownList ID="ddlTipoCancha" runat="server" CssClass="form-select" />
                </div>

                <div class="mb-3">
                    <label class="form-label" for="<%= txtPrecioHora.ClientID %>">Precio por hora</label>
                    <asp:TextBox ID="txtPrecioHora" runat="server" CssClass="form-control" />
                </div>

                <div class="form-check mb-3">
                    <asp:CheckBox ID="chkActiva" runat="server" CssClass="form-check-input" Checked="true" />
                    <label class="form-check-label" for="<%= chkActiva.ClientID %>">Activa</label>
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
                    <h2 class="h4 mb-0">Canchas registradas</h2>
                    <asp:Label ID="lblMensaje" runat="server" CssClass="text-success" />
                </div>

                <asp:GridView ID="gvCanchas" runat="server"
                    CssClass="table table-striped table-hover align-middle"
                    AutoGenerateColumns="false"
                    DataKeyNames="IdCancha"
                    OnRowCommand="gvCanchas_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="IdCancha" HeaderText="ID" />
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                        <asp:BoundField DataField="TipoCanchaDescripcion" HeaderText="Tipo" />
                        <asp:BoundField DataField="PrecioHora" HeaderText="Precio hora" DataFormatString="${0:N2}" />
                        <asp:CheckBoxField DataField="Activa" HeaderText="Activa" />
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnEditar" runat="server"
                                    Text="Editar"
                                    CssClass="btn btn-sm btn-outline-primary me-1"
                                    CommandName="EditarCancha"
                                    CommandArgument='<%# Eval("IdCancha") %>' />
                                <asp:LinkButton ID="btnEliminar" runat="server"
                                    Text="Dar de baja"
                                    CssClass="btn btn-sm btn-outline-danger"
                                    CommandName="EliminarCancha"
                                    CommandArgument='<%# Eval("IdCancha") %>'
                                    OnClientClick="return confirm('Desea dar de baja esta cancha?');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <div class="alert alert-info mb-0">No hay canchas cargadas.</div>
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
