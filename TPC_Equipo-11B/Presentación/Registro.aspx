<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="Presentación.Registro" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Registro - Clínica</title>

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="Assets/CSS/Acceso.css" rel="stylesheet" />
</head>

<body>

    <form id="form1" runat="server">

        <div class="contenedor-acceso">

            <div class="tarjeta-acceso">

                <h3 class="titulo-acceso">Sistema Clínica</h3>
           
                <p class="text-center text-muted mb-3">Crear cuenta</p>

                <div class="mb-2">
                    <asp:TextBox ID="txtNombre" runat="server"
                        CssClass="form-control"
                        placeholder="Nombre" />
                </div>

                <div class="mb-2">
                    <asp:TextBox ID="txtApellido" runat="server"
                        CssClass="form-control"
                        placeholder="Apellido" />
                </div>

                <div class="mb-2">
                    <asp:TextBox ID="txtEmail" runat="server"
                        CssClass="form-control"
                        placeholder="Email" />
                </div>

                <div class="mb-2">
                    <asp:TextBox ID="txtUsuario" runat="server"
                        CssClass="form-control"
                        placeholder="Usuario" />
                </div>

                <div class="mb-3">
                    <asp:TextBox ID="txtPassword" runat="server"
                        TextMode="Password"
                        CssClass="form-control"
                        placeholder="Contraseña" />
                </div>

                <asp:Button ID="btnRegistro" runat="server"
                    Text="Registrarse"
                    CssClass="boton-acceso" />

                <div class="link-acceso">
                    ¿Ya tenés cuenta?
                <a href="Login.aspx">Iniciar sesión</a>
                </div>

            </div>
        </div>

    </form>

</body>
</html>
