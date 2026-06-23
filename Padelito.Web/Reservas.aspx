<%@ Page Title="Reservas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reservas.aspx.cs" Inherits="Padelito.Web.Reservas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row g-4">
        <div class="col-lg-4">
            <div class="bg-white border rounded-3 p-4">
                <h1 class="h4 mb-3">Nueva reserva</h1>

                <div class="mb-3">
                    <label class="form-label" for="<%= ddlCliente.ClientID %>">Cliente</label>
                    <asp:DropDownList ID="ddlCliente" runat="server" CssClass="form-select" />
                </div>

                <div class="mb-3">
                    <label class="form-label" for="<%= ddlTurnoDisponible.ClientID %>">Turno disponible</label>
                    <asp:DropDownList ID="ddlTurnoDisponible" runat="server" CssClass="form-select" />
                </div>

                <div class="mb-3">
                    <label class="form-label" for="<%= ddlEmpleado.ClientID %>">Empleado</label>
                    <asp:DropDownList ID="ddlEmpleado" runat="server" CssClass="form-select" />
                </div>

                <div class="mb-3">
                    <label class="form-label" for="<%= ddlEstadoReserva.ClientID %>">Estado inicial</label>
                    <asp:DropDownList ID="ddlEstadoReserva" runat="server" CssClass="form-select" />
                </div>

                <div class="mb-3">
                    <label class="form-label" for="<%= ddlPromocion.ClientID %>">Promocion</label>
                    <asp:DropDownList ID="ddlPromocion" runat="server" CssClass="form-select" />
                </div>

                <div class="mb-3">
                    <label class="form-label" for="<%= txtFechaReserva.ClientID %>">Fecha de reserva</label>
                    <asp:TextBox ID="txtFechaReserva" runat="server" CssClass="form-control" TextMode="Date" />
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
                    <h2 class="h4 mb-0">Reservas registradas</h2>
                    <asp:Label ID="lblMensaje" runat="server" CssClass="text-success" />
                </div>

                <div class="table-responsive">
                    <asp:GridView ID="gvReservas" runat="server"
                        CssClass="table table-striped table-hover align-middle"
                        AutoGenerateColumns="false"
                        DataKeyNames="IdReserva"
                        OnRowCommand="gvReservas_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="IdReserva" HeaderText="ID" />
                            <asp:BoundField DataField="FechaReserva" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="Cliente" HeaderText="Cliente" />
                            <asp:BoundField DataField="Cancha" HeaderText="Cancha" />
                            <asp:BoundField DataField="TipoCancha" HeaderText="Tipo" />
                            <asp:BoundField DataField="Horario" HeaderText="Horario" />
                            <asp:BoundField DataField="Empleado" HeaderText="Empleado" />
                            <asp:BoundField DataField="EstadoReserva" HeaderText="Estado" />
                            <asp:BoundField DataField="Promocion" HeaderText="Promocion" />
                            <asp:BoundField DataField="PrecioBase" HeaderText="Base" DataFormatString="{0:C2}" />
                            <asp:BoundField DataField="PrecioFinal" HeaderText="Final" DataFormatString="{0:C2}" />
                            <asp:BoundField DataField="FechaCreacion" HeaderText="Creada" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                            <asp:TemplateField HeaderText="Cambiar estado">
                                <ItemTemplate>
                                    <div class="d-flex gap-2">
                                        <asp:DropDownList ID="ddlEstadoGrilla" runat="server" CssClass="form-select form-select-sm" />
                                        <asp:LinkButton ID="btnCambiarEstado" runat="server"
                                            Text="Aplicar"
                                            CssClass="btn btn-sm btn-outline-primary"
                                            CommandName="CambiarEstado"
                                            CommandArgument='<%# Eval("IdReserva") %>' />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <div class="alert alert-info mb-0">No hay reservas cargadas.</div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
