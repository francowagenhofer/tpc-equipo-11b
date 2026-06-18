<%@ Page Title="Pacientes" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="Pacientes.aspx.cs" Inherits="Presentación.Pacientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tabla-pacientes th {
            background-color: #f8f9fa;
            color: #495057;
            font-weight: 600;
        }
        .badge-obra-social {
            background-color: #e3f2fd;
            color: #0d47a1;
            padding: 0.35em 0.65em;
            border-radius: 50rem;
            font-size: 0.85em;
            font-weight: 600;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="server">

    <div class="card card-custom p-4 shadow-sm border-0">
        <div class="d-flex justify-content-between align-items-center mb-4">
            <div>
                <h2 class="fw-bold mb-1">Pacientes</h2>
                <p class="text-muted mb-0">Gestión y registro de pacientes del sistema.</p>
            </div>
            
            <div class="d-flex align-items-center gap-3">
                <div class="form-check form-switch mb-0">
                    <asp:CheckBox ID="chkSoloActivos" runat="server" 
                        Text=" Solo Activos" 
                        AutoPostBack="true" 
                        OnCheckedChanged="chkSoloActivos_CheckedChanged" 
                        Checked="true" 
                        CssClass="form-check-input" />
                </div>

                <a href="NuevoPaciente.aspx" class="btn btn-primary d-flex align-items-center gap-2">
                    <i class="bi bi-person-plus-fill"></i> Registrar Paciente
                </a>
            </div>
        </div>

        <div class="table-responsive">
            <asp:GridView ID="dgvPacientes" runat="server" 
                CssClass="table table-hover align-middle tabla-pacientes" 
                AutoGenerateColumns="false" 
                GridLines="None"
                DataKeyNames="Id"
                OnRowCommand="dgvPacientes_RowCommand">
                <Columns>
                    <asp:BoundField HeaderText="DNI" DataField="DNI" ItemStyle-CssClass="fw-bold text-secondary" />
                    
                    <asp:TemplateField HeaderText="Nombre Completo">
                        <ItemTemplate>
                            <span class="fw-semibold"><%# Eval("Usuario.Apellido") %>, <%# Eval("Usuario.Nombre") %></span>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Email">
                        <ItemTemplate>
                            <%# Eval("Usuario.Email") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Teléfono">
                        <ItemTemplate>
                            <%# Eval("Usuario.Telefono") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Obra Social">
                        <ItemTemplate>
                            <span class="badge-obra-social">
                                <%# Eval("ObraSocial") != null ? Eval("ObraSocial.Nombre") : "Sin Obra Social" %>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Género">
                        <ItemTemplate>
                            <%# Eval("Genero") != null ? Eval("Genero.Descripcion") : "No especificado" %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Estado">
                        <ItemTemplate>
                            <span class='<%# (bool)Eval("Activo") ? "badge bg-success" : "badge bg-danger" %>'>
                                <%# (bool)Eval("Activo") ? "Activo" : "Inactivo" %>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <div class="d-flex gap-2">
                                <asp:PlaceHolder ID="phActivo" runat="server" Visible='<%# (bool)Eval("Activo") %>'>
                                    <asp:LinkButton ID="btnBaja" runat="server" 
                                        CssClass="btn btn-sm btn-outline-danger" 
                                        CommandName="Eliminar" 
                                        CommandArgument='<%# Eval("Id") %>'
                                        OnClientClick="return confirm('¿Seguro que desea dar de baja a este paciente?');">
                                        <i class="bi bi-trash"></i> Dar de Baja
                                    </asp:LinkButton>
                                </asp:PlaceHolder>

                                <asp:PlaceHolder ID="phInactivo" runat="server" Visible='<%# !(bool)Eval("Activo") %>'>
                                    <asp:LinkButton ID="btnReactivar" runat="server" 
                                        CssClass="btn btn-sm btn-outline-success" 
                                        CommandName="Reactivar" 
                                        CommandArgument='<%# Eval("Id") %>'
                                        OnClientClick="return confirm('¿Seguro que desea reactivar a este paciente?');">
                                        <i class="bi bi-arrow-counterclockwise"></i> Reactivar
                                    </asp:LinkButton>
                                </asp:PlaceHolder>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>

</asp:Content>
