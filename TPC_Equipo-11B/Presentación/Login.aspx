<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Presentación.Login" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Login - Clínica</title>

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="~/Assets/CSS/Autenticacion.css" rel="stylesheet" runat="server" />
</head>

<body>

    <form id="form1" runat="server">

        <div class="contenedor-autenticacion">


            <div class="tarjeta-autenticacion">

                <!-- PANEL IZQUIERDO -->
                <div class="panel-izquierdo">

                    <div class="logo-sistema">
                        <div class="logo-contenedor">
                            <img src="<%= ResolveUrl("~/Assets/IMG/logo-clinica.png") %>"
                                alt="Logo Sistema Clínica"
                                class="logo-imagen" />
                        </div>

                        <div class="logo-texto">
                            <span class="logo-nombre">Sistema Clínica</span>
                            <span class="logo-subtitulo">Gestión integral de la salud</span>
                        </div>
                    </div>

                    <div class="cuerpo-izquierdo">

                        <div class="etiqueta">Plataforma de gestión</div>

                        <h2 class="titulo-izquierdo">Gestión clínica<br />
                            centralizada
                        </h2>

                        <p class="descripcion-izquierda">
                            Sistema diseñado para la administración integral de turnos, pacientes, médicos y usuarios.
                        </p>

                        <div class="linea-separadora"></div>

                        <div class="item-info">
                            <div class="punto"></div>
                            <div class="texto-item">
                                <strong>Turnos</strong>
                                Organización de citas médicas en tiempo real.
                            </div>
                        </div>

                        <div class="item-info">
                            <div class="punto"></div>
                            <div class="texto-item">
                                <strong>Pacientes</strong>
                                Gestión completa de historiales clínicos.
                            </div>
                        </div>

                        <div class="item-info">
                            <div class="punto"></div>
                            <div class="texto-item">
                                <strong>Acceso seguro</strong>
                                Control por roles de usuario.
                            </div>
                        </div>

                    </div>

                    <div class="pie-izquierdo">
                        © 2026 Sistema Clínica
                    </div>

                </div>

                <!-- PANEL DERECHO -->
                <div class="panel-derecho">

                    <asp:Panel ID="pnlAvisoAcceso"
                        runat="server"
                        CssClass="alert alert-warning mb-4"
                        Visible="false">

                        <i class="bi bi-shield-lock me-2"></i>
                        Debe iniciar sesión para acceder al sistema.

                    </asp:Panel>

                    <div class="etiqueta-formulario">
                        Acceso al sistema
                    </div>

                    <h3 class="titulo-formulario">Iniciar sesión
                    </h3>

                    <p class="subtitulo-formulario">
                        Ingresá tu usuario y contraseña para acceder.
                    </p>

                    <div class="campo">
                        <asp:TextBox ID="txtUsername"
                            runat="server"
                            CssClass="form-control"
                            placeholder="Usuario"
                            MaxLength="50" />
                    </div>

                    <div class="campo">
                        <asp:TextBox ID="txtPassword"
                            runat="server"
                            TextMode="Password"
                            CssClass="form-control"
                            placeholder="Contraseña"
                            MaxLength="50" />
                    </div>

                    <div class="campo text-center">
                        <asp:Label ID="lblMensajeError"
                            runat="server"
                            CssClass="text-danger"
                            Visible="false" />
                    </div>

                    <asp:Button ID="btnLogin"
                        runat="server"
                        Text="Ingresar"
                        CssClass="boton-principal" OnClick="btnLogin_Click" />

                    <div class="link-formulario">
                        ¿No tenés cuenta?
                    <a href="Registro.aspx">Registrarse</a>
                    </div>

                </div>

            </div>

        </div>

    </form>

</body>
</html>
