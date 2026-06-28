<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Padelito.Web.Login" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Ingreso - Padelito</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/dashboard.css" rel="stylesheet" />
</head>
<body class="login-page">
    <form id="form1" runat="server">
        <main class="login-shell">
            <section class="login-card">
                <div class="login-brand">
                    <span class="brand-mark">P</span>
                    <div>
                        <strong>Padelito</strong>
                        <small>Gestion de canchas</small>
                    </div>
                </div>

                <div class="mb-4">
                    <h1 class="h3 mb-2">Ingresar al sistema</h1>
                    <p class="text-secondary mb-0">Acceso para administradores y empleados.</p>
                </div>

                <asp:Label ID="lblMensaje" runat="server" EnableViewState="false" />

                <div class="mb-3">
                    <label class="form-label" for="txtNombreUsuario">Usuario</label>
                    <asp:TextBox ID="txtNombreUsuario" runat="server" CssClass="form-control" MaxLength="20" />
                </div>

                <div class="mb-4">
                    <label class="form-label" for="txtContrasenia">Contraseña</label>
                    <asp:TextBox ID="txtContrasenia" runat="server" CssClass="form-control" TextMode="Password" MaxLength="15" />
                </div>

                <asp:Button ID="btnIngresar" runat="server" Text="Ingresar" CssClass="btn btn-success w-100" OnClick="btnIngresar_Click" />

                <div class="login-help">
                    <strong>Usuarios de prueba</strong>
                    <span>admin / Admin123!</span>
                    <span>empleado1 / Empleado123!</span>
                </div>
            </section>
        </main>
    </form>
</body>
</html>
