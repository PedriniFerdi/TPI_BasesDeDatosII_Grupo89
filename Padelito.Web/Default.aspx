<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Padelito.Web.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="p-4 bg-white border rounded-3">
        <h1 class="h3 mb-3">Sistema Padelito</h1>
        <p class="mb-3">Base inicial del trabajo practico de Bases de Datos 2.</p>
        <div class="d-flex gap-2">
            <a class="btn btn-success" href="Clientes.aspx">Administrar clientes</a>
            <a class="btn btn-outline-success" href="Empleados.aspx">Administrar empleados</a>
        </div>
    </div>
</asp:Content>
