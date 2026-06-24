<%@ Page Title="Nuevo Paciente" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="NuevoPaciente.aspx.cs" Inherits="Presentación.NuevoPaciente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="server">

    <div class="card shadow-sm border-0 mx-auto" style="max-width: 750px;">

        <div class="card-body p-4">

            <h2 class="fw-bold mb-1">
                <asp:Label ID="lblTitulo"
                    runat="server"
                    Text="Registrar Nuevo Paciente" />
            </h2>

            <p class="text-muted mb-4">
                <asp:Label ID="lblSubtitulo"
                    runat="server"
                    Text="Completa la información para dar de alta al paciente." />
            </p>

            <asp:Label ID="lblMensaje"
                runat="server"
                CssClass="alert d-block text-center"
                Visible="false">
            </asp:Label>

            <h5 class="fw-bold mb-3">Información Personal
            </h5>

            <div class="row g-3">

                <div class="col-md-6">
                    <asp:Label runat="server" AssociatedControlID="txtNombre" CssClass="form-label fw-semibold" Text="Nombre" />
                    <asp:TextBox ID="txtNombre"
                        runat="server"
                        CssClass="form-control"
                        MaxLength="100" />
                </div>

                <div class="col-md-6">
                    <asp:Label runat="server" AssociatedControlID="txtApellido" CssClass="form-label fw-semibold" Text="Apellido" />
                    <asp:TextBox ID="txtApellido"
                        runat="server"
                        CssClass="form-control"
                        MaxLength="100" />
                </div>

                <div class="col-md-6">
                    <asp:Label runat="server" AssociatedControlID="txtDNI" CssClass="form-label fw-semibold" Text="DNI" />
                    <asp:TextBox ID="txtDNI"
                        runat="server"
                        CssClass="form-control"
                        MaxLength="8" />
                </div>

                <div class="col-md-6">
                    <asp:Label runat="server" AssociatedControlID="txtFechaNacimiento" CssClass="form-label fw-semibold" Text="Fecha de Nacimiento" />
                    <asp:TextBox ID="txtFechaNacimiento"
                        runat="server"
                        TextMode="Date"
                        CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <asp:Label runat="server" AssociatedControlID="ddlGenero" CssClass="form-label fw-semibold" Text="Género" />
                    <asp:DropDownList ID="ddlGenero"
                        runat="server"
                        CssClass="form-select">
                    </asp:DropDownList>
                </div>

                <div class="col-md-6">
                    <asp:Label runat="server" AssociatedControlID="ddlObrasSociales" CssClass="form-label fw-semibold" Text="Obra Social" />
                    <asp:DropDownList ID="ddlObrasSociales"
                        runat="server"
                        CssClass="form-select">
                    </asp:DropDownList>
                </div>

            </div>

            <hr class="my-4" />

            <h5 class="fw-bold mb-3">Información de Contacto
            </h5>

            <div class="row g-3">

                <div class="col-md-6">
                    <asp:Label runat="server" AssociatedControlID="txtTelefono" CssClass="form-label fw-semibold" Text="Teléfono" />
                    <asp:TextBox ID="txtTelefono"
                        runat="server"
                        CssClass="form-control"
                        MaxLength="30" />
                </div>

                <div class="col-md-6">
                    <asp:Label runat="server" AssociatedControlID="txtEmail" CssClass="form-label fw-semibold" Text="Correo Electrónico" />
                    <asp:TextBox ID="txtEmail"
                        runat="server"
                        TextMode="Email"
                        CssClass="form-control"
                        MaxLength="150" />
                </div>

                <div class="col-12">
                    <asp:Label runat="server" AssociatedControlID="txtDireccion" CssClass="form-label fw-semibold" Text="Dirección" />
                    <asp:TextBox ID="txtDireccion"
                        runat="server"
                        CssClass="form-control"
                        MaxLength="250" />
                </div>

                <div class="col-12">

                    <asp:Label runat="server" AssociatedControlID="fuImagen" CssClass="form-label fw-semibold" Text="Foto de Perfil" />

                    <asp:FileUpload
                        ID="fuImagen"
                        runat="server"
                        CssClass="form-control" />

                    <small class="text-muted">Formatos permitidos: JPG, JPEG y PNG.
                    </small>

                </div>

                <asp:Image ID="imgPreview" runat="server" CssClass="img-thumbnail mt-3" Width="120" Visible="false" />

            </div>


            <hr class="my-4" />

            <asp:Panel ID="pnlAltaCredenciales" runat="server">

                <h5 class="fw-bold mb-3">Credenciales de Acceso</h5>

                <div class="form-check mb-3 d-flex align-items-center gap-2 p-0">
                    <asp:CheckBox ID="chkCredencialesAutomaticas" runat="server" Checked="true" AutoPostBack="true" OnCheckedChanged="chkCredencialesAutomaticas_CheckedChanged" />
                    <label class="form-check-label fw-semibold" for="<%= chkCredencialesAutomaticas.ClientID %>">Generar credenciales automáticamente usando DNI</label>
                </div>


<%--                <asp:Panel ID="pnlInfoCredenciales" runat="server" CssClass="alert alert-info">
                    <div class="d-flex align-items-start gap-2">
                        <i class="bi bi-info-circle-fill mt-1"></i>
                        <div>
                            <strong>Credenciales automáticas</strong>

                            <div class="small mt-1">
                                Se generará un nombre de usuario basado en el email del usuario.
                                La contraseña inicial será la fecha de alta del sistema.
                            </div>
                        </div>
                    </div>
                </asp:Panel>--%>

                <asp:Panel ID="pnlInfoCredenciales" runat="server" CssClass="alert alert-info">
                    <div class="d-flex align-items-start gap-2">
                        <i class="bi bi-info-circle-fill mt-1"></i>
                        <div>
                            <strong>Credenciales automáticas</strong>

                            <div class="small mt-1">
                                Se generará un nombre de usuario basado en el email del usuario.
                                La contraseña inicial será el DNI del paciente.
                            </div>
                        </div>
                    </div>
                </asp:Panel>

            </asp:Panel>

            <asp:Panel ID="pnlCredencialesAlta" runat="server" Visible="false">

                <div class="alert alert-warning d-flex align-items-center gap-2 mb-3">
                    <i class="bi bi-shield-lock-fill"></i>
                    <span>Las credenciales de acceso son información sensible.</span>
                </div>

                <div class="row g-3">

                    <div class="col-md-6">
                        <asp:Label runat="server" AssociatedControlID="txtUsername" CssClass="form-label fw-semibold" Text="Usuario" />
                        <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" MaxLength="50" autocomplete="off" />
                    </div>

                    <div class="col-md-6">
                        <asp:Label runat="server" AssociatedControlID="txtPassword" CssClass="form-label fw-semibold" Text="Contraseña" />
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" MaxLength="50" autocomplete="new-password" />
                    </div>

                    <div class="col-md-6">
                        <asp:Label runat="server" AssociatedControlID="txtConfirmarPassword" CssClass="form-label fw-semibold" Text="Confirmar contraseña" />
                        <asp:TextBox ID="txtConfirmarPassword" runat="server" TextMode="Password" CssClass="form-control" MaxLength="50" autocomplete="new-password" />
                    </div>

                </div>

            </asp:Panel>

            <asp:Panel ID="pnlEdicionCredenciales" runat="server" Visible="false">

                <h5 class="fw-bold mb-3">Credenciales de Acceso</h5>

                <div class="mb-3">
                    <asp:Label runat="server" AssociatedControlID="txtUsernameEdicion" CssClass="form-label fw-semibold" Text="Usuario" />
                    <asp:TextBox ID="txtUsernameEdicion" runat="server" CssClass="form-control" autocomplete="off" />
                </div>

                <div class="form-check mb-3 d-flex align-items-center gap-2 p-0">
                    <asp:CheckBox ID="chkCambiarPassword" runat="server" AutoPostBack="true" OnCheckedChanged="chkCambiarPassword_CheckedChanged" />
                    <label class="form-check-label fw-semibold" for="<%= chkCambiarPassword.ClientID %>">Cambiar contraseña</label>
                </div>

                <asp:Panel ID="pnlCambioPassword" runat="server" Visible="false">

                    <div class="alert alert-warning d-flex align-items-center gap-2 mb-3">
                        <i class="bi bi-shield-lock-fill"></i>
                        <span>Las credenciales de acceso son información sensible.</span>
                    </div>

                    <div class="row g-3">

                        <div class="col-md-6">
                            <asp:Label runat="server" AssociatedControlID="txtPasswordEdicion" CssClass="form-label fw-semibold" Text="Nueva contraseña" />
                            <asp:TextBox ID="txtPasswordEdicion" runat="server" TextMode="Password" CssClass="form-control" autocomplete="new-password" />
                        </div>

                        <div class="col-md-6">
                            <asp:Label runat="server" AssociatedControlID="txtConfirmarPasswordEdicion" CssClass="form-label fw-semibold" Text="Confirmar contraseña" />
                            <asp:TextBox ID="txtConfirmarPasswordEdicion" runat="server" TextMode="Password" CssClass="form-control" autocomplete="new-password" />
                        </div>

                    </div>

                </asp:Panel>

            </asp:Panel>


            <div class="d-flex justify-content-end gap-2 mt-4">
                <a href="Pacientes.aspx"
                    class="btn btn-outline-secondary">Cancelar
                </a>

                <asp:Button ID="btnGuardar"
                    runat="server"
                    Text="Guardar Paciente"
                    CssClass="btn btn-primary"
                    OnClick="btnGuardar_Click" />
            </div>
        </div>

    </div>

</asp:Content>
