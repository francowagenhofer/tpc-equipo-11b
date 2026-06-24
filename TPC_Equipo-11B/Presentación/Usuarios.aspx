<%@ Page Title="" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="Presentación.Usuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
        .table-pager {
            text-align: center;
        }

            .table-pager table {
                margin: 10px auto;
            }

            .table-pager a,
            .table-pager span {
                padding: 6px 10px;
                margin: 0 3px;
                border: 1px solid #dee2e6;
                border-radius: 6px;
                text-decoration: none;
            }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="server">


    <div class="card card-custom p-4 shadow-sm border-0">

        <div class="d-flex justify-content-between align-items-center mb-4">

            <div>
                <h2 class="fw-bold mb-1">Usuarios</h2>
                <p class="text-muted mb-0">
                    Gestión de usuarios administrativos y recepcionistas.
               
                </p>
            </div>

            <a href="NuevoUsuario.aspx"
                class="btn btn-primary d-flex align-items-center gap-2">

                <i class="bi bi-person-plus-fill"></i>
                Registrar Usuario

            </a>

        </div>

        <div class="row g-3 mb-4">

            <div class="col-md-3">

                <label class="form-label fw-semibold text-muted small">
                    Buscar Usuario
               
                </label>

                <div class="input-group">

                    <span class="input-group-text bg-white border-end-0">
                        <i class="bi bi-search text-muted"></i>
                    </span>

                    <asp:TextBox
                        ID="txtBuscar"
                        runat="server"
                        CssClass="form-control border-start-0"
                        placeholder="Buscar por nombre o usuario..."
                        AutoPostBack="true"
                        OnTextChanged="txtBuscar_TextChanged" />

                </div>

            </div>

            <div class="col-md-3">

                <label class="form-label fw-semibold text-muted small">
                    Filtrar por Rol
               
                </label>

                <asp:DropDownList
                    ID="ddlRol"
                    runat="server"
                    CssClass="form-select"
                    AutoPostBack="true"
                    OnSelectedIndexChanged="ddlRol_SelectedIndexChanged">

                    <asp:ListItem Text="Todos los roles" Value="0" />
                    <asp:ListItem Text="Administrador" Value="1" />
                    <asp:ListItem Text="Recepcionista" Value="2" />

                </asp:DropDownList>

            </div>

            <div class="col-md-3">

                <label class="form-label fw-semibold text-muted small">
                    Filtrar por Estado
               
                </label>

                <asp:DropDownList
                    ID="ddlEstado"
                    runat="server"
                    CssClass="form-select"
                    AutoPostBack="true"
                    OnSelectedIndexChanged="ddlEstado_SelectedIndexChanged">

                    <asp:ListItem Text="Todos los estados" Value="0" />
                    <asp:ListItem Text="Activo" Value="1" />
                    <asp:ListItem Text="No activo" Value="2" />

                </asp:DropDownList>

            </div>

            <div class="col-md-2 d-flex align-items-end">

                <asp:Button
                    ID="btnLimpiar"
                    runat="server"
                    Text="Limpiar"
                    CssClass="btn btn-outline-secondary btn-sm w-100"
                    OnClick="btnLimpiar_Click" />

            </div>

        </div>

        <div class="table-responsive">

            <asp:GridView
                ID="dgvUsuarios"
                runat="server"
                CssClass="table table-hover align-middle tabla-personalizada"
                AutoGenerateColumns="false"
                GridLines="None"
                DataKeyNames="Id"
                OnRowCommand="dgvUsuarios_RowCommand"
                AllowPaging="true"
                PageSize="10"
                OnPageIndexChanging="dgvUsuarios_PageIndexChanging"
                PagerStyle-CssClass="table-pager">

                <Columns>

                    <asp:TemplateField HeaderText="Nombre Completo">
                        <ItemTemplate>
                            <span class="fw-semibold">
                                <%# Eval("Apellido") %>, <%# Eval("Nombre") %>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField
                        HeaderText="Usuario"
                        DataField="Username" />

                    <asp:BoundField
                        HeaderText="Email"
                        DataField="Email" />

                    <asp:TemplateField HeaderText="Rol">
                        <ItemTemplate>
                            <%# Eval("Rol.Nombre") %>
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

                                <asp:LinkButton
                                    ID="btnEditar"
                                    runat="server"
                                    CssClass="btn btn-sm btn-outline-secondary"
                                    CommandName="Editar"
                                    Visible='<%# (bool)Eval("Activo") %>'
                                    CommandArgument='<%# Eval("Id") %>'>

                                    <i class="bi bi-pencil"></i>

                                </asp:LinkButton>

                                <asp:LinkButton
                                    ID="btnBaja"
                                    runat="server"
                                    CssClass="btn btn-sm btn-outline-danger"
                                    CommandName="Eliminar"
                                    Visible='<%# (bool)Eval("Activo") %>'
                                    CommandArgument='<%# Eval("Id") %>'>

                                    <i class="bi bi-trash"></i>

                                </asp:LinkButton>

                                <asp:LinkButton
                                    ID="btnReactivar"
                                    runat="server"
                                    CssClass="btn btn-sm btn-outline-success"
                                    CommandName="Reactivar"
                                    Visible='<%# !(bool)Eval("Activo") %>'
                                    CommandArgument='<%# Eval("Id") %>'>

                                    <i class="bi bi-arrow-clockwise"></i>

                                </asp:LinkButton>

                            </div>

                        </ItemTemplate>

                    </asp:TemplateField>

                </Columns>

            </asp:GridView>

        </div>

        <asp:HiddenField ID="hfIdUsuario" runat="server" />
        <asp:HiddenField ID="hfAccion" runat="server" />

        <div class="modal fade"
            id="modalConfirmacion"
            runat="server"
            clientidmode="Static"
            tabindex="-1"
            aria-hidden="true">

            <div class="modal-dialog">
                <div class="modal-content">

                    <div id="headerModal" runat="server" class="modal-header bg-danger text-white">

                        <h5 id="tituloModal" runat="server" class="modal-title">Confirmación</h5>

                        <button type="button"
                            class="btn-close"
                            data-bs-dismiss="modal">
                        </button>
                    </div>

                    <div class="modal-body">

                        <asp:Label
                            ID="lblMensajeModal"
                            runat="server"
                            Text="">
                        </asp:Label>

                    </div>

                    <div class="modal-footer">

                        <button type="button"
                            class="btn btn-secondary"
                            data-bs-dismiss="modal">
                            Cancelar
                        </button>

                        <asp:Button
                            ID="btnConfirmarAccion"
                            runat="server"
                            Text="Confirmar"
                            CssClass="btn btn-danger"
                            OnClick="btnConfirmarAccion_Click" />
                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>
