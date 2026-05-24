<%@ Page Title="Turnos disponibles" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TurnosDisponibles.aspx.cs" Inherits="Padelito.Web.TurnosDisponibles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row g-4">
        <div class="col-lg-4">
            <div class="bg-white border rounded-3 p-4">
                <h1 class="h4 mb-3">Turno disponible</h1>

                <asp:HiddenField ID="hfIdTurnoDisponible" runat="server" />

                <div class="mb-3">
                    <label class="form-label" for="<%= txtHoraInicio.ClientID %>">Hora inicio</label>
                    <asp:TextBox ID="txtHoraInicio" runat="server" CssClass="form-control" TextMode="Time" />
                </div>

                <div class="mb-3">
                    <label class="form-label" for="<%= txtHoraFin.ClientID %>">Hora fin</label>
                    <asp:TextBox ID="txtHoraFin" runat="server" CssClass="form-control" TextMode="Time" />
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
                    <h2 class="h4 mb-0">Turnos registrados</h2>
                    <asp:Label ID="lblMensaje" runat="server" CssClass="text-success" />
                </div>

                <asp:GridView ID="gvTurnosDisponibles" runat="server"
                    CssClass="table table-striped table-hover align-middle"
                    AutoGenerateColumns="false"
                    DataKeyNames="IdTurnoDisponible"
                    OnRowCommand="gvTurnosDisponibles_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="IdTurnoDisponible" HeaderText="ID" />
                        <asp:BoundField DataField="Horario" HeaderText="Horario" />
                        <asp:CheckBoxField DataField="Activo" HeaderText="Activo" />
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnEditar" runat="server"
                                    Text="Editar"
                                    CssClass="btn btn-sm btn-outline-primary me-1"
                                    CommandName="EditarTurnoDisponible"
                                    CommandArgument='<%# Eval("IdTurnoDisponible") %>' />
                                <asp:LinkButton ID="btnEliminar" runat="server"
                                    Text="Dar de baja"
                                    CssClass="btn btn-sm btn-outline-danger"
                                    CommandName="EliminarTurnoDisponible"
                                    CommandArgument='<%# Eval("IdTurnoDisponible") %>'
                                    OnClientClick="return confirm('Desea dar de baja este turno disponible?');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <div class="alert alert-info mb-0">No hay turnos disponibles cargados.</div>
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
