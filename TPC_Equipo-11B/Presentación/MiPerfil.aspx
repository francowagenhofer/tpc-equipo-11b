<%@ Page Title="" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="MiPerfil.aspx.cs" Inherits="Presentación.MiPerfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="server">


    <div class="container-fluid">

        <!-- Título -->
        <div class="d-flex justify-content-between align-items-center mb-4">
            <div>
                <h2 class="fw-bold mb-1">Mi Perfil </h2>
                <small class="text-muted">Visualice y actualice su información personal. </small>
            </div>

            <asp:Label ID="lblMensaje" runat="server" CssClass="alert alert-danger d-block mt-3" Visible="false" />
        </div>

        <div class="row">
            <!-- Columna izquierda -->
            <div class="col-lg-4">
                <div class="card shadow-sm border-0 mb-4">
                    <div class="card-body text-center">

                        <asp:Image
                            ID="imgPerfil"
                            runat="server"
                            ImageUrl="~/Assets/IMG/usuario.png"
                            CssClass="rounded-circle border mb-3"
                            Width="170"
                            Height="170" />

                        <h4 class="fw-bold mb-1">
                            <asp:Label ID="lblNombreCompleto" runat="server" />
                        </h4>

                        <span class="badge bg-primary fs-6">
                            <asp:Label ID="lblRol" runat="server" />
                        </span>

                        <div class="mt-3">
                            <small class="text-muted">Usuario </small>
                            <div class="fw-semibold">
                                <asp:Label ID="lblUsername" runat="server" />
                            </div>
                        </div>
                        <hr />
                        <div class="text-start">
                            <label class="form-label">Foto de perfil</label>
                            <asp:FileUpload ID="fuImagen" runat="server" CssClass="form-control" />
                            <small class="text-muted">Formatos permitidos: JPG y PNG. Tamaño máximo: 5 MB.</small>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Columna derecha -->
            <div class="col-lg-8">
                <!-- Información Personal -->
                <div class="card shadow-sm border-0 mb-4">
                    <div class="card-header fw-bold">
                        Información Personal
                   
                    </div>

                    <div class="card-body">
                        <div class="row g-3">

                            <div class="col-md-6">

                                <label class="form-label">
                                    Nombre
                           
                                </label>

                                <asp:TextBox
                                    ID="txtNombre"
                                    runat="server"
                                    CssClass="form-control" />

                            </div>

                            <div class="col-md-6">
                                <label class="form-label">Apellido </label>
                                <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" />
                            </div>
                            <div class="col-md-6">
                                <label class="form-label">Email </label>
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
                            </div>
                            <div class="col-md-6">
                                <label class="form-label">Teléfono</label>
                                <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" />
                            </div>

                            <div class="col-md-6">
                                <label class="form-label">Usuario </label>
                                <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" />
                            </div>
                        </div>
                    </div>
                </div>

                <!-- Médico -->
                <asp:Panel ID="pnlMedico" runat="server" Visible="false">
                    <div class="card shadow-sm border-0 mb-4">
                        <div class="card-header fw-bold">
                            Información Profesional
                       
                        </div>

                        <div class="card-body">
                            <div class="row g-3">
                                <div class="col-md-6">
                                    <label class="form-label">Matrícula </label>
                                    <asp:TextBox
                                        ID="txtMatricula"
                                        runat="server"
                                        CssClass="form-control" />

                                </div>
                                <div class="col-md-6">
                                    <label class="form-label">Especialidad </label>
                                    <asp:DropDownList
                                        ID="ddlEspecialidad"
                                        runat="server"
                                        CssClass="form-select" />
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>

                <!-- Paciente -->
                <asp:Panel ID="pnlPaciente" runat="server" Visible="false">

                    <div class="card shadow-sm border-0 mb-4">
                        <div class="card-header fw-bold">
                            Información del Paciente
                       
                        </div>

                        <div class="card-body">
                            <div class="row g-3">
                                <div class="col-md-4">
                                    <label class="form-label">DNI</label>
                                    <asp:TextBox
                                        ID="txtDni"
                                        runat="server"
                                        CssClass="form-control" />
                                </div>
                                <div class="col-md-4">
                                    <label class="form-label">Fecha Nacimiento </label>
                                    <asp:TextBox
                                        ID="txtFechaNacimiento"
                                        runat="server"
                                        CssClass="form-control"
                                        TextMode="Date" />
                                </div>
                                <div class="col-md-4">
                                    <label class="form-label">Género </label>
                                    <asp:DropDownList ID="ddlGenero" runat="server" CssClass="form-select" />
                                </div>
                                <div class="col-md-6">
                                    <label class="form-label">Obra Social</label>
                                    <asp:DropDownList ID="ddlObraSocial" runat="server" CssClass="form-select" />
                                </div>
                                <div class="col-md-6">
                                    <label class="form-label">Dirección</label>
                                    <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" />
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>

                <asp:CheckBox
                    ID="chkCambiarPassword"
                    runat="server"
                    Text=" Cambiar contraseña"
                    AutoPostBack="true"
                    OnCheckedChanged="chkCambiarPassword_CheckedChanged" />

                <!-- Seguridad -->
                <asp:Panel ID="pnlPassword" runat="server" Visible="false">
                    <div class="card shadow-sm border-0">
                        <div class="card-header fw-bold">
                            Seguridad
                        </div>
                        <div class="card-body">
                            <div class="row g-3">
                                <div class="col-md-6">
                                    <label class="form-label">Nueva Contraseña </label>
                                    <asp:TextBox
                                        ID="txtPassword"
                                        runat="server"
                                        CssClass="form-control"
                                        TextMode="Password" />
                                </div>
                                <div class="col-md-6">
                                    <label class="form-label">Confirmar Contraseña</label>
                                    <asp:TextBox
                                        ID="txtConfirmarPassword"
                                        runat="server"
                                        CssClass="form-control"
                                        TextMode="Password" />
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>

                <div class="text-end mt-4">
                    <asp:Button
                        ID="btnGuardar"
                        runat="server"
                        CssClass="btn btn-primary btn-lg"
                        Text="Guardar Cambios"
                        OnClick="btnGuardar_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
