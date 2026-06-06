<%@ Page Title="Nuevo Paciente" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="NuevoPaciente.aspx.cs" Inherits="Presentación.NuevoPaciente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="server">

    <div class="card card-custom p-4 shadow-sm border-0 mx-auto" style="max-width: 600px;">
        <h2 class="fw-bold mb-1">Registrar Nuevo Paciente</h2>
        <p class="text-muted mb-4">Ingresa los datos personales del paciente.</p>

        
        <asp:Label ID="lblMensaje" runat="server" CssClass="alert d-block text-center" Visible="false"></asp:Label>

        <div class="row g-3">
           
            <div class="col-md-6">
                <label class="form-label fw-semibold">Nombre</label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Ej. Juan" required />
            </div>
            <div class="col-md-6">
                <label class="form-label fw-semibold">Apellido</label>
                <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" placeholder="Ej. Pérez" required />
            </div>

            
            <div class="col-md-6">
                <label class="form-label fw-semibold">DNI</label>
                <asp:TextBox ID="txtDNI" runat="server" CssClass="form-control" placeholder="Número de documento" required />
            </div>
            <div class="col-md-6">
                <label class="form-label fw-semibold">Fecha de Nacimiento</label>
                <asp:TextBox ID="txtFechaNacimiento" runat="server" TextMode="Date" CssClass="form-control" required />
            </div>

            
            <div class="col-md-6">
                <label class="form-label fw-semibold">Teléfono</label>
                <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" placeholder="Ej. 11-5555-6666" />
            </div>
            <div class="col-md-6">
                <label class="form-label fw-semibold">Email</label>
                <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" CssClass="form-control" placeholder="paciente@correo.com" required />
            </div>

            
            <div class="col-12">
                <label class="form-label fw-semibold">Dirección</label>
                <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" placeholder="Calle, número, localidad" />
            </div>

            
            <div class="col-md-6">
                <label class="form-label fw-semibold">Obra Social</label>
                <asp:DropDownList ID="ddlObraSocial" runat="server" CssClass="form-select" required></asp:DropDownList>
            </div>
            <div class="col-md-6">
                <label class="form-label fw-semibold">Género</label>
                <asp:DropDownList ID="ddlGenero" runat="server" CssClass="form-select" required></asp:DropDownList>
            </div>

            
            <div class="col-12 d-flex justify-content-end gap-2 mt-4">
                <a href="Pacientes.aspx" class="btn btn-outline-secondary">Cancelar</a>
                <asp:Button ID="btnGuardar" runat="server" Text="Registrar Paciente" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
            </div>
        </div>
    </div>

</asp:Content>