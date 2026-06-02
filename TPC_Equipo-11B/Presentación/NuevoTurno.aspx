<%@ Page Title="Nuevo Turno" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="NuevoTurno.aspx.cs" Inherits="Presentación.NuevoTurno" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="server">

    <div class="card card-custom p-4 shadow-sm border-0 mx-auto" style="max-width: 600px;">
        <h2 class="fw-bold mb-1">Registrar Nuevo Turno</h2>
        <p class="text-muted mb-4">Selecciona paciente, médico y la fecha del turno.</p>

        
        <asp:Label ID="lblMensaje" runat="server" CssClass="alert d-block text-center" Visible="false"></asp:Label>

        <div class="row g-3">
            
            <div class="col-12">
                <label class="form-label fw-semibold">Paciente</label>
                <asp:DropDownList ID="ddlPaciente" runat="server" CssClass="form-select" required></asp:DropDownList>
            </div>

           
            <div class="col-12">
                <label class="form-label fw-semibold">Médico</label>
                <asp:DropDownList ID="ddlMedico" runat="server" CssClass="form-select" required></asp:DropDownList>
            </div>

            
            <div class="col-12">
                <label class="form-label fw-semibold">Fecha y Hora</label>
                <asp:TextBox ID="txtFechaHora" runat="server" TextMode="DateTimeLocal" CssClass="form-control" required />
            </div>

            
            <div class="col-12 d-flex justify-content-end gap-2 mt-4">
                <a href="Turnos.aspx" class="btn btn-outline-secondary">Cancelar</a>
                <asp:Button ID="btnGuardar" runat="server" Text="Confirmar Turno" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
            </div>
        </div>
    </div>

</asp:Content>