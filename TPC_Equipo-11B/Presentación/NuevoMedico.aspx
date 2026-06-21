<%@ Page Title="Médico" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="NuevoMedico.aspx.cs" Inherits="Presentación.NuevoMedico" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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

            <h5 class="fw-bold mb-3">Información Profesional</h5>

            <div class="row g-3">

                <div class="col-md-6">
                    <label class="form-label fw-semibold">Nombre</label>
                    <asp:TextBox ID="txtNombre"
                        runat="server"
                        CssClass="form-control"
                        MaxLength="100" />
                </div>

                <div class="col-md-6">
                    <label class="form-label fw-semibold">Apellido</label>
                    <asp:TextBox ID="txtApellido"
                        runat="server"
                        CssClass="form-control"
                        MaxLength="100" />
                </div>

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

                <div class="col-12">
                    <label class="form-label fw-semibold">Foto de Perfil</label>

                    <asp:FileUpload
                        ID="fuImagen"
                        runat="server"
                        CssClass="form-control" />

                    <small class="text-muted">Formatos permitidos: JPG, JPEG, PNG.
                    </small>
                </div>

                <asp:Image
                    ID="imgPreview"
                    runat="server"
                    CssClass="img-thumbnail mt-3"
                    Width="120"
                    Visible="false" />

            </div>

            <hr class="my-4" />

            <div class="alert alert-warning d-flex align-items-center gap-2">

                <i class="bi bi-shield-lock-fill"></i>

                <span>Las credenciales de acceso son información sensible.
                </span>

            </div>

            <h5 class="fw-bold mb-3">Credenciales de Acceso</h5>

            <div class="row g-3">

                <div class="col-md-6">
                    <label class="form-label fw-semibold">Nombre de Usuario</label>
                    <asp:TextBox ID="txtUsername"
                        runat="server"
                        CssClass="form-control"
                        MaxLength="50"  autocomplete="off"/>
                </div>

                <div class="col-md-6">
                    <label class="form-label fw-semibold">Contraseña</label>
                    <asp:TextBox ID="txtPassword"
                        runat="server"
                        TextMode="Password"
                        CssClass="form-control"
                        MaxLength="50"  autocomplete="new-password"/>
                </div>

                <div class="col-md-6">
                    <label class="form-label fw-semibold">Confirmar Contraseña</label>
                    <asp:TextBox ID="txtConfirmarPassword"
                        runat="server"
                        TextMode="Password"
                        CssClass="form-control"
                        MaxLength="50"  autocomplete="new-password"/>
                </div>

            </div>

            <div class="d-flex justify-content-end gap-2 mt-4">

                <a href="Medicos.aspx"
                    class="btn btn-outline-secondary">Cancelar
                </a>

                <asp:Button ID="btnGuardar"
                    runat="server"
                    Text="Guardar Médico"
                    CssClass="btn btn-primary"
                    OnClick="btnGuardar_Click" />

            </div>

        </div>

    </div>

</asp:Content>
