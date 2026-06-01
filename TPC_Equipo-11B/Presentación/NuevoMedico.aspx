<%@ Page Title="" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="NuevoMedico.aspx.cs" Inherits="Presentación.NuevoMedico" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="server">

    <div class="card card-custom p-4 shadow-sm border-0 mx-auto" style="max-width: 600px;">
        <h2 class="fw-bold mb-1">Registrar Nuevo Médico</h2>
        <p class="text-muted mb-4">Completa la información para dar de alta al profesional.</p>

        
        <asp:Label ID="lblMensaje" runat="server" CssClass="alert d-block text-center" Visible="false"></asp:Label>

        <div class="row g-3">
            
            <div class="col-md-6">
                <label class="form-label fw-semibold">Nombre</label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Ej. Gregory" required />
            </div>
            <div class="col-md-6">
                <label class="form-label fw-semibold">Apellido</label>
                <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" placeholder="Ej. House" required />
            </div>

            <div class="col-md-6">
                <label class="form-label fw-semibold">Matrícula</label>
                <asp:TextBox ID="txtMatricula" runat="server" CssClass="form-control" placeholder="Ej. MN-12345" required />
            </div>
            <div class="col-md-6">
                <label class="form-label fw-semibold">Teléfono</label>
                <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" placeholder="Ej. 11-4444-5555" />
            </div>

            <div class="col-12">
                <label class="form-label fw-semibold">Email</label>
                <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" CssClass="form-control" placeholder="nombre@clinica.com" required />
            </div>

            <hr class="my-4 text-muted">
            <h5 class="fw-bold text-secondary mb-2">Credenciales de Acceso</h5>

            
            <div class="col-md-6">
                <label class="form-label fw-semibold">Nombre de Usuario</label>
                <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Usuario para login" required />
            </div>
            <div class="col-md-6">
                <label class="form-label fw-semibold">Contraseña</label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Contraseña de acceso" required />
            </div>

            
            <div class="col-12 d-flex justify-content-end gap-2 mt-4">
                <a href="Medicos.aspx" class="btn btn-outline-secondary">Cancelar</a>
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar Médico" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
            </div>
        </div>
    </div>

</asp:Content>