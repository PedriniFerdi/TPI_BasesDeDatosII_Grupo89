<%@ Page Title="Pagos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Pagos.aspx.cs" Inherits="Padelito.Web.Pagos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row g-4">
        <div class="col-lg-4">
            <div class="bg-white border rounded-3 p-4">
                <h1 class="h4 mb-3">Registrar pago</h1>

                <div class="mb-3">
                    <label class="form-label" for="<%= ddlReserva.ClientID %>">Reserva</label>
                    <asp:DropDownList ID="ddlReserva" runat="server" CssClass="form-select" />
                </div>

                <div class="mb-3">
                    <label class="form-label" for="<%= ddlMetodoPago.ClientID %>">Metodo de pago</label>
                    <asp:DropDownList ID="ddlMetodoPago" runat="server" CssClass="form-select" />
                </div>

                <div class="mb-3">
                    <label class="form-label" for="<%= txtMonto.ClientID %>">Monto</label>
                    <asp:TextBox ID="txtMonto" runat="server" CssClass="form-control" TextMode="Number" step="0.01" min="0" />
                </div>

                <div class="mb-3">
                    <label class="form-label" for="<%= txtObservacion.ClientID %>">Observacion</label>
                    <asp:TextBox ID="txtObservacion" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" MaxLength="255" />
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
                    <h2 class="h4 mb-0">Pagos registrados</h2>
                    <asp:Label ID="lblMensaje" runat="server" CssClass="text-success" />
                </div>

                <div class="table-responsive">
                    <asp:GridView ID="gvPagos" runat="server"
                        CssClass="table table-striped table-hover align-middle"
                        AutoGenerateColumns="false"
                        DataKeyNames="IdPago">
                        <Columns>
                            <asp:BoundField DataField="IdPago" HeaderText="ID" />
                            <asp:BoundField DataField="IdReserva" HeaderText="Reserva" />
                            <asp:BoundField DataField="Cliente" HeaderText="Cliente" />
                            <asp:BoundField DataField="Cancha" HeaderText="Cancha" />
                            <asp:BoundField DataField="FechaReserva" HeaderText="Fecha reserva" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="MetodoPago" HeaderText="Metodo" />
                            <asp:BoundField DataField="Monto" HeaderText="Monto" DataFormatString="{0:C2}" />
                            <asp:BoundField DataField="FechaPago" HeaderText="Fecha pago" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                            <asp:BoundField DataField="Observacion" HeaderText="Observacion" />
                        </Columns>
                        <EmptyDataTemplate>
                            <div class="alert alert-info mb-0">No hay pagos cargados.</div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
