<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Presentación.Login" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Login - Clínica</title>

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="Assets/CSS/Acceso.css" rel="stylesheet" />
</head>

<body>

    <form id="form1" runat="server">

        <div class="contenedor-acceso">

            <div class="tarjeta-acceso">

                <h3 class="titulo-acceso">Sistema Clínica</h3>

                <p class="text-center text-muted mb-3">
                    Iniciar sesión
                </p>

                <div class="mb-3">
                    <asp:TextBox ID="TextBox1"
                        runat="server"
                        CssClass="form-control"
                        placeholder="Usuario" />
                </div>

                <div class="mb-3">
                    <asp:TextBox ID="TextBox2"
                        runat="server"
                        TextMode="Password"
                        CssClass="form-control"
                        placeholder="Contraseña" />
                </div>

                <div class="mb-3 text-center">
                    <asp:Label ID="lblMensajeError" runat="server" CssClass="text-danger" Visible="false" />
                </div>

                <asp:Button ID="Button1"
                    runat="server"
                    Text="Ingresar"
                    CssClass="boton-acceso" />

                <div class="link-acceso">
                    ¿No tenés cuenta?
            <a href="Registro.aspx">Registrarse</a>
                </div>

            </div>

        </div>
    </form>

</body>
</html>
