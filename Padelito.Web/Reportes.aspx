<%@ Page Title="Reportes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reportes.aspx.cs" Inherits="Padelito.Web.Reportes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="bg-white border rounded-3 p-4 mb-4">
        <h1 class="h4 mb-3">Reporte de reservas por fecha</h1>

        <div class="row g-3 align-items-end">
            <div class="col-md-3">
                <label class="form-label" for="<%= txtFechaDesde.ClientID %>">Fecha desde</label>
                <asp:TextBox ID="txtFechaDesde" runat="server" CssClass="form-control" TextMode="Date" />
            </div>

            <div class="col-md-3">
                <label class="form-label" for="<%= txtFechaHasta.ClientID %>">Fecha hasta</label>
                <asp:TextBox ID="txtFechaHasta" runat="server" CssClass="form-control" TextMode="Date" />
            </div>

            <div class="col-md-3">
                <label class="form-label" for="<%= ddlEstadoReserva.ClientID %>">Estado</label>
                <asp:DropDownList ID="ddlEstadoReserva" runat="server" CssClass="form-select" />
            </div>

            <div class="col-md-3">
                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-success w-100" OnClick="btnBuscar_Click" />
            </div>
        </div>

        <asp:Label ID="lblMensaje" runat="server" CssClass="d-block mt-3 text-danger" />
    </div>

    <div class="row g-3 mb-4">
        <div class="col-md-6">
            <div class="bg-white border rounded-3 p-4">
                <span class="text-secondary d-block">Cantidad de reservas</span>
                <asp:Label ID="lblCantidadReservas" runat="server" CssClass="h3 mb-0 d-block" Text="0" />
            </div>
        </div>

        <div class="col-md-6">
            <div class="bg-white border rounded-3 p-4">
                <span class="text-secondary d-block">Total precio final</span>
                <asp:Label ID="lblTotalPrecioFinal" runat="server" CssClass="h3 mb-0 d-block" Text="$0,00" />
            </div>
        </div>
    </div>

    <div class="bg-white border rounded-3 p-4">
        <div class="table-responsive">
            <asp:GridView ID="gvReporteReservas" runat="server"
                CssClass="table table-striped table-hover align-middle"
                AutoGenerateColumns="false"
                DataKeyNames="IdReserva">
                <Columns>
                    <asp:BoundField DataField="IdReserva" HeaderText="Reserva" />
                    <asp:BoundField DataField="FechaReserva" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="Cliente" HeaderText="Cliente" />
                    <asp:BoundField DataField="Cancha" HeaderText="Cancha" />
                    <asp:BoundField DataField="Horario" HeaderText="Horario" />
                    <asp:BoundField DataField="EstadoReserva" HeaderText="Estado" />
                    <asp:BoundField DataField="PrecioBase" HeaderText="Precio base" DataFormatString="${0:N2}" />
                    <asp:BoundField DataField="PrecioFinal" HeaderText="Precio final" DataFormatString="${0:N2}" />
                </Columns>
                <EmptyDataTemplate>
                    <div class="alert alert-info mb-0">No hay reservas para los filtros seleccionados.</div>
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
