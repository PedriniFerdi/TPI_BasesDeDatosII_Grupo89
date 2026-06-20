<%@ Page Title="Promociones" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Promociones.aspx.cs" Inherits="Padelito.Web.Promociones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row g-4">
        <div class="col-lg-4">
            <div class="bg-white border rounded-3 p-4">
                <h1 class="h4 mb-3">Promocion</h1>

                <asp:HiddenField ID="hfIdPromocion" runat="server" />

                <div class="mb-3">
                    <label class="form-label" for="<%= txtNombre.ClientID %>">Nombre</label>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" MaxLength="80" />
                </div>

                <div class="mb-3">
                    <label class="form-label" for="<%= txtDescripcion.ClientID %>">Descripcion</label>
                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" MaxLength="255" TextMode="MultiLine" Rows="3" />
                </div>

                <div class="mb-3">
                    <label class="form-label" for="<%= txtPorcentajeDescuento.ClientID %>">Porcentaje de descuento</label>
                    <asp:TextBox ID="txtPorcentajeDescuento" runat="server" CssClass="form-control" />
                </div>

                <div class="mb-3">
                    <label class="form-label" for="<%= txtFechaDesde.ClientID %>">Fecha desde</label>
                    <asp:TextBox ID="txtFechaDesde" runat="server" CssClass="form-control" TextMode="Date" />
                </div>

                <div class="mb-3">
                    <label class="form-label" for="<%= txtFechaHasta.ClientID %>">Fecha hasta</label>
                    <asp:TextBox ID="txtFechaHasta" runat="server" CssClass="form-control" TextMode="Date" />
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
                    <h2 class="h4 mb-0">Promociones registradas</h2>
                    <asp:Label ID="lblMensaje" runat="server" CssClass="text-success" />
                </div>

                <asp:GridView ID="gvPromociones" runat="server"
                    CssClass="table table-striped table-hover align-middle"
                    AutoGenerateColumns="false"
                    DataKeyNames="IdPromocion"
                    OnRowCommand="gvPromociones_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="IdPromocion" HeaderText="ID" />
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                        <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" />
                        <asp:BoundField DataField="PorcentajeDescuento" HeaderText="Descuento" DataFormatString="{0:N2}%" />
                        <asp:BoundField DataField="FechaDesde" HeaderText="Desde" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="FechaHasta" HeaderText="Hasta" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:CheckBoxField DataField="Activa" HeaderText="Activa" />
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnEditar" runat="server"
                                    Text="Editar"
                                    CssClass="btn btn-sm btn-outline-primary me-2 mb-1"
                                    Style="width: 66px;"
                                    CommandName="EditarPromocion"
                                    CommandArgument='<%# Eval("IdPromocion") %>' />
                                <asp:LinkButton ID="btnEliminar" runat="server"
                                    Text="Baja"
                                    CssClass="btn btn-sm btn-outline-danger"
                                    Style="width: 66px;"
                                    CommandName="EliminarPromocion"
                                    CommandArgument='<%# Eval("IdPromocion") %>'
                                    OnClientClick="return confirm('Desea dar de baja esta promocion?');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <div class="alert alert-info mb-0">No hay promociones cargadas.</div>
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
