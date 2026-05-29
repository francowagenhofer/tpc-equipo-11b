<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Presentación.Login" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Login - Clínica</title>

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="Assets/CSS/master.css" rel="stylesheet" />
    <link href="Assets/CSS/login.css" rel="stylesheet" />

</head>

<body>

    <form id="form1" runat="server">

        <div class="contenedor-login">

            <div class="login-card">

                <h3 class="text-center mb-3">Iniciar Sesión</h3>

                <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control mb-2" placeholder="Usuario" />
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control mb-3" placeholder="Contraseña" />

                <asp:Button ID="btnLogin" runat="server" Text="Ingresar" CssClass="btn-login" />

            </div>

        </div>

    </form>

</body>
</html>
