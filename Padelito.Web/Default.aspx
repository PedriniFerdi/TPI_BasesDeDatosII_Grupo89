<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Padelito.Web.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="pnlSinPermiso" runat="server" CssClass="alert alert-warning" Visible="false">
        No tenes permiso para acceder al modulo solicitado.
    </asp:Panel>

    <section class="dashboard-hero mb-4">
        <div class="d-flex flex-column flex-lg-row justify-content-between gap-3">
            <div>
                <h2 class="h3">Panel General</h2>
                <p>Revise la actividad del club y acceda al flujo principal de reservas, pagos, reportes y auditoria.</p>
            </div>
            <div class="d-flex align-items-start gap-2">
                <a class="btn btn-success" href="Reservas.aspx">Nueva reserva</a>
                <a class="btn btn-outline-success" href="Pagos.aspx">Registrar pago</a>
            </div>
        </div>
    </section>

    <section class="row g-3 mb-4" aria-label="Resumen general">
        <div class="col-sm-6 col-xl-3">
            <div class="metric-card">
                <span class="metric-icon">CL</span>
                <div>
                    <p class="metric-label">Clientes activos</p>
                    <p class="metric-value"><asp:Literal ID="litClientesActivos" runat="server" /></p>
                    <p class="metric-note">Personas activas asociadas a clientes</p>
                </div>
            </div>
        </div>

        <div class="col-sm-6 col-xl-3">
            <div class="metric-card">
                <span class="metric-icon">CA</span>
                <div>
                    <p class="metric-label">Canchas activas</p>
                    <p class="metric-value"><asp:Literal ID="litCanchasActivas" runat="server" /></p>
                    <p class="metric-note">Desde VW_CanchasActivas</p>
                </div>
            </div>
        </div>

        <div class="col-sm-6 col-xl-3">
            <div class="metric-card">
                <span class="metric-icon">RE</span>
                <div>
                    <p class="metric-label">Reservas del dia</p>
                    <p class="metric-value"><asp:Literal ID="litReservasDelDia" runat="server" /></p>
                    <p class="metric-note"><asp:Literal ID="litFechaActualSistema" runat="server" /></p>
                </div>
            </div>
        </div>

        <div class="col-sm-6 col-xl-3">
            <div class="metric-card">
                <span class="metric-icon">PG</span>
                <div>
                    <p class="metric-label">Ingresos registrados</p>
                    <p class="metric-value"><asp:Literal ID="litIngresosRegistrados" runat="server" /></p>
                    <p class="metric-note"><asp:Literal ID="litPagosRegistrados" runat="server" /> pagos cargados</p>
                </div>
            </div>
        </div>
    </section>

    <div class="row g-4">
        <section class="col-xl-5">
            <div class="panel-card h-100">
                <div class="d-flex justify-content-between align-items-center mb-3">
                    <div>
                        <h3 class="h5 mb-1">Accesos rapidos</h3>
                        <p class="text-secondary mb-0">Operaciones principales del sistema</p>
                    </div>
                </div>

                <div class="row g-3">
                    <div class="col-sm-6">
                        <a class="quick-card" href="Reservas.aspx">
                            <strong>Reservas</strong>
                            <span>Crear y cambiar estado</span>
                        </a>
                    </div>
                    <div class="col-sm-6">
                        <a class="quick-card" href="Pagos.aspx">
                            <strong>Pagos</strong>
                            <span>Registrar y consultar</span>
                        </a>
                    </div>
                    <div class="col-sm-6">
                        <a class="quick-card" href="Reportes.aspx">
                            <strong>Reportes</strong>
                            <span>Reservas por fecha</span>
                        </a>
                    </div>
                    <asp:PlaceHolder ID="pnlAccesoAdmin" runat="server">
                        <div class="col-sm-6">
                            <a class="quick-card" href="AuditoriaReservas.aspx">
                                <strong>Auditoria</strong>
                                <span>Consulta de cambios</span>
                            </a>
                        </div>
                        <div class="col-sm-6">
                            <a class="quick-card" href="Canchas.aspx">
                                <strong>Canchas</strong>
                                <span>Precios y estado</span>
                            </a>
                        </div>
                    </asp:PlaceHolder>
                    <div class="col-sm-6">
                        <a class="quick-card" href="Clientes.aspx">
                            <strong>Clientes</strong>
                            <span>ABM completo</span>
                        </a>
                    </div>
                </div>
            </div>
        </section>

        <section class="col-xl-7">
            <div class="panel-card h-100">
                <div class="d-flex flex-column flex-md-row justify-content-between gap-2 mb-3">
                    <div>
                        <h3 class="h5 mb-1">Ultimas reservas</h3>
                        <p class="text-secondary mb-0">Ultimos movimientos registrados desde la vista de reservas</p>
                    </div>
                    <a class="btn btn-sm btn-outline-success align-self-start" href="Reservas.aspx">Ver reservas</a>
                </div>

                <div class="table-responsive">
                    <asp:GridView ID="gvUltimasReservas" runat="server"
                        CssClass="table table-dashboard align-middle"
                        AutoGenerateColumns="false"
                        GridLines="None">
                        <Columns>
                            <asp:BoundField DataField="FechaReserva" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="Cliente" HeaderText="Cliente" />
                            <asp:BoundField DataField="Cancha" HeaderText="Cancha" />
                            <asp:BoundField DataField="Horario" HeaderText="Horario" />
                            <asp:BoundField DataField="EstadoReserva" HeaderText="Estado" />
                            <asp:BoundField DataField="PrecioFinal" HeaderText="Final" DataFormatString="${0:N2}" />
                        </Columns>
                        <EmptyDataTemplate>
                            <div class="alert alert-light border mb-0">No hay reservas cargadas desde el sistema.</div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
