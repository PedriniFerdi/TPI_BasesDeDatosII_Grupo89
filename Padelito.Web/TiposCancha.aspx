<%@ Page Title="Tipos de cancha" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TiposCancha.aspx.cs" Inherits="Padelito.Web.TiposCancha" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row g-4">
        <div class="col-lg-4">
            <div class="bg-white border rounded-3 p-4">
                <h1 class="h4 mb-3">Tipo de cancha</h1>

                <asp:HiddenField ID="hfIdTipoCancha" runat="server" />

                <div class="mb-3">
                    <label class="form-label" for="<%= txtDescripcion.ClientID %>">Descripcion</label>
                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" MaxLength="80" />
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
                    <h2 class="h4 mb-0">Tipos de cancha registrados</h2>
                    <asp:Label ID="lblMensaje" runat="server" CssClass="text-success" />
                </div>

                <asp:GridView ID="gvTiposCancha" runat="server"
                    CssClass="table table-striped table-hover align-middle"
                    AutoGenerateColumns="false"
                    DataKeyNames="IdTipoCancha"
                    OnRowCommand="gvTiposCancha_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="IdTipoCancha" HeaderText="ID" />
                        <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" />
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnEditar" runat="server"
                                    Text="Editar"
                                    CssClass="btn btn-sm btn-outline-primary me-1"
                                    CommandName="EditarTipoCancha"
                                    CommandArgument='<%# Eval("IdTipoCancha") %>' />
                                <asp:LinkButton ID="btnEliminar" runat="server"
                                    Text="Eliminar"
                                    CssClass="btn btn-sm btn-outline-danger"
                                    CommandName="EliminarTipoCancha"
                                    CommandArgument='<%# Eval("IdTipoCancha") %>'
                                    OnClientClick="return confirm('Desea eliminar este tipo de cancha?');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <div class="alert alert-info mb-0">No hay tipos de cancha cargados.</div>
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
