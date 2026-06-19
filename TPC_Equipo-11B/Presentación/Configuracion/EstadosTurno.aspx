<%@ Page Title="" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="EstadosTurno.aspx.cs" Inherits="Presentación.Configuracion.EstadosTurno" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="server">

    <div class="d-flex justify-content-between align-items-center mb-4">
        <div>
            <h2>Estados de Turno</h2>
            <p>Gestión de estados.</p>
        </div>

        <a href="#" class="btn btn-primary d-flex align-items-center gap-2">
            <i class="bi bi-calendar-plus-fill"></i>Nueva Estado
        </a>
    </div>

    <div class="row g-3 mb-4">
        <div class="col-md-4">
            <label class="form-label fw-semibold text-muted small">Buscar estado de turno</label>
            <div class="input-group">
                <span class="input-group-text bg-white border-end-0"><i class="bi bi-search text-muted"></i></span>
                <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control border-start-0" placeholder="Buscar por nombre o ID..." AutoPostBack="true" OnTextChanged="txtBuscar_TextChanged" />
            </div>
        </div>

        <div class="col-md-3">
            <label class="form-label fw-semibold text-muted small">Filtrar por Estado</label>
            <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlEstado_SelectedIndexChanged">
                <asp:ListItem Text="Todos los estados" Value="0" />
                <asp:ListItem Text="Activo" Value="1" />
                <asp:ListItem Text="No activo" Value="2" />
            </asp:DropDownList>
        </div>
    </div>

    <div class="table-responsive">
        <asp:GridView ID="dgvEstadosTurno" runat="server"
            CssClass="table table-hover align-middle tabla-personalizada"
            AutoGenerateColumns="false"
            GridLines="None"
            DataKeyNames="Id">
            <Columns>
                <asp:TemplateField HeaderText="ID">
                    <ItemTemplate>
                        <%# Eval("Id") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Especialidad">
                    <ItemTemplate>
                        <%# Eval("Nombre") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Estado">
                    <ItemTemplate>
                        <span class='<%# (bool)Eval("Activo") ? "badge bg-success" : "badge bg-secondary" %>'>
                            <%# (bool)Eval("Activo") ? "Activo" : "Inactivo" %>
                        </span>
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

</asp:Content>
