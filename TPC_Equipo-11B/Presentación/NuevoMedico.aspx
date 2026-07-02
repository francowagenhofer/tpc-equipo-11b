<%@ Page Title="Médico" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="NuevoMedico.aspx.cs" Inherits="Presentación.NuevoMedico" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
        .obras-sociales-container {
            border: 1px solid #dee2e6;
            border-radius: .5rem;
            padding: 12px 16px;
            background: #f8f9fa;
            max-height: 180px;
            overflow-y: auto;
        }

            .obras-sociales-container table {
                width: 100%;
            }

            .obras-sociales-container td {
                width: 50%;
                padding: 4px 8px;
                vertical-align: top;
            }

            .obras-sociales-container input[type=checkbox] {
                margin-right: .45rem;
                transform: scale(1.05);
            }

            .obras-sociales-container label {
                font-weight: 500;
                cursor: pointer;
            }

        .card-body .form-control[type=file] {
            margin-top: .35rem;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="server">

    <div class="card shadow-sm border-0 mx-auto" style="max-width: 750px;">

        <div class="card-body p-4">

            <h2 class="fw-bold mb-1">
                <asp:Label ID="lblTitulo" runat="server" Text="Registrar Nuevo Médico" />
            </h2>

            <p class="text-muted mb-4">
                <asp:Label ID="lblSubtitulo" runat="server"
                    Text="Completa la información para dar de alta al profesional." />
            </p>

            <asp:Label ID="lblMensaje"
                runat="server"
                CssClass="alert d-block text-center"
                Visible="false">
            </asp:Label>

            <h5 class="fw-bold mb-3">Información Personal</h5>

            <div class="row g-4 align-items-start">

                <!-- Datos personales -->
                <div class="col-md-6">

                    <div class="mb-3">

                        <label class="form-label fw-semibold">
                            Nombre
                        </label>

                        <asp:TextBox
                            ID="txtNombre"
                            runat="server"
                            CssClass="form-control"
                            MaxLength="100" />

                    </div>

                    <div>

                        <label class="form-label fw-semibold">
                            Apellido
                        </label>

                        <asp:TextBox
                            ID="txtApellido"
                            runat="server"
                            CssClass="form-control"
                            MaxLength="100" />

                    </div>

                </div>

                <!-- Imagen -->
                <div class="col-md-6">

                    <div class="card border-0 bg-light h-100">

                        <div class="card-body">

                            <label class="form-label fw-semibold">
                                Foto de Perfil
                            </label>

                            <asp:FileUpload
                                ID="fuImagen"
                                runat="server"
                                CssClass="form-control" />

                            <small class="text-muted d-block mt-2">Opcional. Formatos permitidos: JPG, JPEG o PNG.
                            </small>

                        </div>

                    </div>
                </div>
            </div>



            <hr class="my-4" />
            <h5 class="fw-bold mb-3">Información de Contacto</h5>

            <div class="row g-3">

                <div class="col-md-6">
                    <label class="form-label fw-semibold">Teléfono</label>
                    <asp:TextBox ID="txtTelefono"
                        runat="server"
                        CssClass="form-control"
                        MaxLength="30" />
                </div>

                <div class="col-md-6">
                    <label class="form-label fw-semibold">Correo Electrónico</label>
                    <asp:TextBox ID="txtEmail"
                        runat="server"
                        TextMode="Email"
                        CssClass="form-control"
                        MaxLength="150" />
                </div>
            </div>

            <hr class="my-4" />

            <h5 class="fw-bold mb-3">Información de Profesional</h5>

            <div class="row g-3">
                <div class="col-md-6">
                    <label class="form-label fw-semibold">Matrícula</label>
                    <asp:TextBox ID="txtMatricula"
                        runat="server"
                        CssClass="form-control"
                        MaxLength="50" />
                </div>

                <div class="col-md-6">
                    <label class="form-label fw-semibold">Especialidad</label>

                    <asp:DropDownList ID="ddlEspecialidad"
                        runat="server"
                        CssClass="form-select">
                    </asp:DropDownList>
                </div>

                <div class="col-12">
                    <label class="form-label fw-semibold">Obras Sociales que atiende </label>

                    <div class="obras-sociales-container">
                        <asp:CheckBoxList
                            ID="cblObrasSociales"
                            runat="server"
                            RepeatColumns="2"
                            RepeatDirection="Vertical"
                            RepeatLayout="Table">
                        </asp:CheckBoxList>
                    </div>
                    <small class="text-muted">Seleccione una o más obras sociales.</small>
                </div>
            </div>

            <hr class="my-4" />

            <div class="alert alert-warning d-flex align-items-center gap-2">
                <i class="bi bi-shield-lock-fill"></i>
                <span>Las credenciales de acceso son información sensible.</span>
            </div>

            <h5 class="fw-bold mb-3">Credenciales de Acceso</h5>

            <div class="row g-3">
                <div class="col-lg-4">
                    <label class="form-label fw-semibold">Nombre de Usuario</label>
                    <asp:TextBox ID="txtUsername"
                        runat="server"
                        CssClass="form-control"
                        MaxLength="50" autocomplete="off" />
                </div>
                <div class="col-lg-4">
                    <label class="form-label fw-semibold">Contraseña</label>
                    <asp:TextBox ID="txtPassword"
                        runat="server"
                        TextMode="Password"
                        CssClass="form-control"
                        MaxLength="50" autocomplete="new-password" />
                </div>
                <div class="col-lg-4">
                    <label class="form-label fw-semibold">Confirmar Contraseña</label>
                    <asp:TextBox ID="txtConfirmarPassword"
                        runat="server"
                        TextMode="Password"
                        CssClass="form-control"
                        MaxLength="50" autocomplete="new-password" />
                </div>
            </div>

            <div class="d-flex justify-content-end gap-2 mt-4">
                <a href="Medicos.aspx" class="btn btn-outline-secondary">Cancelar </a>

                <asp:Button ID="btnGuardar"
                    runat="server"
                    Text="Guardar Médico"
                    CssClass="btn btn-primary"
                    OnClick="btnGuardar_Click" />

            </div>
        </div>
    </div>
</asp:Content>
