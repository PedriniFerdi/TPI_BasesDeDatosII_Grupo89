<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Padelito.Web.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <section class="dashboard-hero mb-4">
        <div class="d-flex flex-column flex-lg-row justify-content-between gap-3">
            <div>
                <h2 class="h3">Dashboard Padelito</h2>
                <p>Panel inicial para administrar clientes, empleados, tipos de cancha y canchas desde una interfaz simple y preparada para sumar reservas, pagos y reportes.</p>
            </div>
            <div class="d-flex align-items-start gap-2">
                <a class="btn btn-success" href="Clientes.aspx">Nuevo cliente</a>
                <a class="btn btn-outline-success" href="Canchas.aspx">Ver canchas</a>
            </div>
        </div>
    </section>

    <section class="row g-3 mb-4" aria-label="Resumen general">
        <div class="col-sm-6 col-xl-3">
            <div class="metric-card">
                <span class="metric-icon">CL</span>
                <div>
                    <p class="metric-label">Clientes activos</p>
                    <p class="metric-value">--</p>
                    <p class="metric-note">Pendiente de consulta real</p>
                </div>
            </div>
        </div>

        <div class="col-sm-6 col-xl-3">
            <div class="metric-card">
                <span class="metric-icon">CA</span>
                <div>
                    <p class="metric-label">Canchas disponibles</p>
                    <p class="metric-value">--</p>
                    <p class="metric-note">Usar tabla Canchas</p>
                </div>
            </div>
        </div>

        <div class="col-sm-6 col-xl-3">
            <div class="metric-card">
                <span class="metric-icon">RE</span>
                <div>
                    <p class="metric-label">Reservas del dia</p>
                    <p class="metric-value">0</p>
                    <p class="metric-note">Modulo no desarrollado</p>
                </div>
            </div>
        </div>

        <div class="col-sm-6 col-xl-3">
            <div class="metric-card">
                <span class="metric-icon">PG</span>
                <div>
                    <p class="metric-label">Pagos registrados</p>
                    <p class="metric-value">0</p>
                    <p class="metric-note">Modulo no desarrollado</p>
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
                        <a class="quick-card" href="Clientes.aspx">
                            <strong>Clientes</strong>
                            <span>ABM</span>
                        </a>
                    </div>
                    <div class="col-sm-6">
                        <a class="quick-card" href="Empleados.aspx">
                            <strong>Empleados</strong>
                            <span>ABM</span>
                        </a>
                    </div>
                    <div class="col-sm-6">
                        <a class="quick-card" href="TiposCancha.aspx">
                            <strong>Tipos de cancha</strong>
                            <span>Catalogo</span>
                        </a>
                    </div>
                    <div class="col-sm-6">
                        <a class="quick-card" href="Canchas.aspx">
                            <strong>Canchas</strong>
                            <span>Disponibilidad</span>
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
                        <p class="text-secondary mb-0">Vista provisoria hasta desarrollar el modulo de reservas</p>
                    </div>
                    <span class="badge text-bg-success align-self-start">Placeholder</span>
                </div>

                <div class="table-responsive">
                    <table class="table table-dashboard align-middle">
                        <thead>
                            <tr>
                                <th>Cliente</th>
                                <th>Cancha</th>
                                <th>Horario</th>
                                <th>Estado</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td colspan="4">
                                    <div class="alert alert-light border mb-0">
                                        Todavia no hay reservas cargadas desde el sistema.
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
