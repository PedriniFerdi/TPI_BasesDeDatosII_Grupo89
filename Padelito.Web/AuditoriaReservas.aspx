<%@ Page Title="Auditoria de reservas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AuditoriaReservas.aspx.cs" Inherits="Padelito.Web.AuditoriaReservas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="bg-white border rounded-3 p-4 mb-4">
        <h1 class="h4 mb-2">Auditoria de reservas</h1>
        <p class="text-secondary mb-0">Consulta de movimientos registrados automaticamente por los triggers de reservas.</p>
        <asp:Label ID="lblMensaje" runat="server" CssClass="d-block mt-3 text-danger" />
    </div>

    <div class="bg-white border rounded-3 p-4">
        <div class="table-responsive">
            <asp:GridView ID="gvAuditoriaReservas" runat="server"
                CssClass="table table-striped table-hover align-middle"
                AutoGenerateColumns="false"
                DataKeyNames="IdAuditoria">
                <Columns>
                    <asp:BoundField DataField="IdAuditoria" HeaderText="Auditoria" />
                    <asp:BoundField DataField="IdReserva" HeaderText="Reserva" />
                    <asp:BoundField DataField="Accion" HeaderText="Accion" />
                    <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" />
                    <asp:BoundField DataField="FechaAccion" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                    <asp:BoundField DataField="UsuarioSistema" HeaderText="Usuario SQL" />
                </Columns>
                <EmptyDataTemplate>
                    <div class="alert alert-info mb-0">Todavia no hay movimientos de auditoria registrados.</div>
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
