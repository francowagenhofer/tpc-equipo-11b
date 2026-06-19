<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="Presentación.Registro" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Registro - Clínica</title>

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

                        <div class="etiqueta">Alta de usuario</div>

                        <h2 class="titulo-izquierdo">Crear nueva<br />
                            cuenta
                        </h2>

                        <p class="descripcion-izquierda">
                            Registrate en el sistema para acceder a la gestión de turnos, pacientes y servicios médicos.
                        </p>

                        <div class="linea-separadora"></div>

                        <div class="item-info">
                            <div class="punto"></div>
                            <div class="texto-item">
                                <strong>Acceso personalizado</strong>
                                Cada usuario tiene permisos según su rol.
                            </div>
                        </div>

                        <div class="item-info">
                            <div class="punto"></div>
                            <div class="texto-item">
                                <strong>Gestión segura</strong>
                                Datos protegidos dentro del sistema.
                            </div>
                        </div>

                        <div class="item-info">
                            <div class="punto"></div>
                            <div class="texto-item">
                                <strong>Historial completo</strong>
                                Integración con módulos clínicos.
                            </div>
                        </div>

                    </div>

                    <div class="pie-izquierdo">
                        © 2026 Sistema Clínica
                    </div>

                </div>

                <!-- PANEL DERECHO -->
                <div class="panel-derecho">

                    <div class="etiqueta-formulario">
                        Registro de usuario
                    </div>

                    <h3 class="titulo-formulario">Crear cuenta
                    </h3>

                    <p class="subtitulo-formulario">
                        Completá los datos para registrarte en el sistema.
                    </p>

                    <div class="campo">
                        <asp:TextBox ID="txtNombre" runat="server"
                            CssClass="form-control"
                            placeholder="Nombre" />
                    </div>

                    <div class="campo">
                        <asp:TextBox ID="txtApellido" runat="server"
                            CssClass="form-control"
                            placeholder="Apellido" />
                    </div>

                    <div class="campo">
                        <asp:TextBox ID="txtEmail" runat="server"
                            CssClass="form-control"
                            placeholder="Correo electrónico" />
                    </div>

                    <div class="campo">
                        <asp:TextBox ID="txtUsuario" runat="server"
                            CssClass="form-control"
                            placeholder="Usuario" />
                    </div>

                    <div class="campo">
                        <asp:TextBox ID="txtPassword" runat="server"
                            TextMode="Password"
                            CssClass="form-control"
                            placeholder="Contraseña" />
                    </div>

                    <asp:Button ID="btnRegistro"
                        runat="server"
                        Text="Crear cuenta"
                        CssClass="boton-principal" />

                    <div class="link-formulario">
                        ¿Ya tenés cuenta?
                    <a href="Login.aspx">Iniciar sesión</a>
                    </div>

                </div>

            </div>

        </div>

    </form>

</body>
</html>


