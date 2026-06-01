<%@ Page Title="" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="Medicos.aspx.cs" Inherits="Presentación.Medicos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tabla-personalizada th {
            background-color: #f8f9fa;
            color: #495057;
            font-weight: 600;
        }

        .badge-activo {
            background-color: #e8f5e9;
            color: #2e7d32;
            padding: 0.35em 0.65em;
            border-radius: 50rem;
            font-size: 0.85em;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="server">

    <div class="card card-custom p-4 shadow-sm border-0">
        <div class="d-flex justify-content-between align-items-center mb-4">
            <div>
                <h2 class="fw-bold mb-1">Médicos</h2>
                <p class="text-muted mb-0">Gestión de médicos y sus especialidades.</p>
            </div>

            <a href="NuevoMedico.aspx" class="btn btn-primary d-flex align-items-center gap-2">
                <i class="bi bi-person-plus-fill"></i>Nuevo Médico
            </a>
        </div>


        <div class="row mb-3">
            <div class="col-md-4">
                <div class="input-group">
                    <span class="input-group-text bg-white border-end-0"><i class="bi bi-search text-muted"></i></span>
                    <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control border-start-0" placeholder="Buscar por apellido o matrícula..." AutoPostBack="true" OnTextChanged="txtBuscar_TextChanged" />
                </div>
            </div>
        </div>

        <div class="table-responsive">
            <asp:GridView ID="dgvMedicos" runat="server"
                CssClass="table table-hover align-middle tabla-personalizada"
                AutoGenerateColumns="false"
                GridLines="None"
                DataKeyNames="Id">
                <Columns>
                    <asp:TemplateField HeaderText="Nombre Completo">

                        <ItemTemplate>
                            <%# Eval("Usuario.Apellido") %>, <%# Eval("Usuario.Nombre") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField HeaderText="Matrícula" DataField="Matricula" ItemStyle-CssClass="fw-semibold" />

                    <asp:TemplateField HeaderText="Especialidad">
                        <ItemTemplate>
                            <%# Eval("Especialidad.Nombre") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Email">
                        <ItemTemplate>
                            <%# Eval("Usuario.Email") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%--                    <asp:TemplateField HeaderText="Teléfono">
                        <ItemTemplate>
                            <%# Eval("Usuario.Telefono") %>
                        </ItemTemplate>
                    </asp:TemplateField>--%>

                    <asp:TemplateField HeaderText="Estado">
                        <ItemTemplate>
                            <span class="badge-activo">Activo</span>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <div class="d-flex gap-2">
                                <asp:LinkButton ID="btnEditar" runat="server" CssClass="btn btn-sm btn-outline-secondary" CommandName="Editar" CommandArgument='<%# Eval("Id") %>'>
                                    <i class="bi bi-pencil"></i>
                                </asp:LinkButton>
                                <asp:LinkButton ID="btnBaja" runat="server" CssClass="btn btn-sm btn-outline-danger" CommandName="Eliminar" CommandArgument='<%# Eval("Id") %>'>
                                    <i class="bi bi-trash"></i>
                                </asp:LinkButton>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
        </div>
    </div>

</asp:Content>
