<%@ Page Title="Médicos" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="Medicos.aspx.cs" Inherits="Presentación.Medicos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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

        <asp:Panel ID="pnlAlertaSinDisponibilidad" runat="server" CssClass="alert alert-warning d-flex align-items-start gap-3 mb-4" Visible="false">
            <i class="bi bi-exclamation-triangle-fill fs-4 mt-1"></i>
            <div class="flex-grow-1">
                <strong>Médicos sin disponibilidad configurada</strong>
                <p class="mb-2 small">
                    Los siguientes médicos no podrán recibir turnos hasta que configuren sus horarios de atención semanal.
                </p>
                <asp:Repeater ID="rptMedicosSinDisponibilidad" runat="server">
                    <ItemTemplate>
                        <span class="badge bg-white text-dark border me-2 mb-1 d-inline-flex align-items-center gap-1">Dr. <%# Eval("Usuario.Apellido") %>, <%# Eval("Usuario.Nombre") %> (Mat: <%# Eval("Matricula") %>)
        <a href='PerfilMedico.aspx?id=<%# Eval("Id") %>' class="text-decoration-none ms-1" title="Ver perfil">
            <i class="bi bi-box-arrow-up-right"></i>
        </a>
                            <a href='MiDisponibilidad.aspx?idMedico=<%# Eval("Id") %>' class="text-decoration-none ms-1 text-primary" title="Configurar disponibilidad">
                                <i class="bi bi-calendar-plus"></i>
                            </a>
                        </span>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </asp:Panel>

        <div class="row g-3 mb-4">
            <div class="col-md-3">
                <label class="form-label fw-semibold text-muted small">Buscar Médico</label>
                <div class="input-group">
                    <span class="input-group-text bg-white border-end-0"><i class="bi bi-search text-muted"></i></span>
                    <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control border-start-0" placeholder="Buscar por nombre o matrícula..." AutoPostBack="true" OnTextChanged="txtBuscar_TextChanged" />
                </div>
            </div>

            <div class="col-md-3">
                <label class="form-label fw-semibold text-muted small">Filtrar por Especialidad</label>
                <asp:DropDownList ID="ddlEspecialidad" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlEspecialidad_SelectedIndexChanged">
                </asp:DropDownList>
            </div>

            <div class="col-md-3">
                <label class="form-label fw-semibold text-muted small">Filtrar por Estado</label>
                <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlEstado_SelectedIndexChanged">
                    <asp:ListItem Text="Todos los estados" Value="0" />
                    <asp:ListItem Text="Activo" Value="1" />
                    <asp:ListItem Text="No activo" Value="2" />
                </asp:DropDownList>
            </div>

            <div class="col-md-2 d-flex align-items-end">
                <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn btn-outline-secondary w-100" OnClick="btnLimpiar_Click" />
            </div>
        </div>

        <div class="table-responsive">
            <asp:GridView ID="dgvMedicos" runat="server"
                CssClass="table table-hover align-middle tabla-personalizada"
                AutoGenerateColumns="false"
                GridLines="None"
                DataKeyNames="Id"
                OnRowCommand="dgvMedicos_RowCommand"
                AllowPaging="true"
                PageSize="10"
                OnPageIndexChanging="dgvMedicos_PageIndexChanging"
                PagerStyle-CssClass="table-pager">

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

                    <asp:TemplateField HeaderText="Estado">
                        <ItemTemplate>
                            <span class='<%# (bool)Eval("Activo") ? "badge bg-success" : "badge bg-danger" %>'>
                                <%# (bool)Eval("Activo") ? "Activo" : "Inactivo" %>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Acciones"
                        ItemStyle-CssClass="text-center"
                        HeaderStyle-CssClass="text-center">
                        <ItemTemplate>
                                <div class="d-flex justify-content-center gap-2">
                                <asp:LinkButton
                                    ID="btnPerfil"
                                    runat="server"
                                    CssClass="btn btn-sm btn-outline-info"
                                    CommandName="Perfil"
                                    CommandArgument='<%# Eval("Id") %>'>
                                    <i class="bi bi-person-vcard me-1"></i> Perfil
                                </asp:LinkButton>

                                <asp:LinkButton
                                    ID="btnEditar"
                                    runat="server"
                                    CssClass="btn btn-sm btn-outline-secondary"
                                    CommandName="Editar"
                                    Visible='<%# (bool)Eval("Activo") %>'
                                    CommandArgument='<%# Eval("Id") %>'>
                                     <i class="bi bi-pencil me-1"></i> Editar
                                </asp:LinkButton>

                                <asp:LinkButton
                                    ID="btnBaja"
                                    runat="server"
                                    CssClass="btn btn-sm btn-outline-danger"
                                    CommandName="Eliminar"
                                    Visible='<%# (bool)Eval("Activo") %>'
                                    CommandArgument='<%# Eval("Id") %>'>
                                     <i class="bi bi-trash me-1"></i> Eliminar
                                </asp:LinkButton>

                                <asp:LinkButton
                                    ID="btnReactivar"
                                    runat="server"
                                    CssClass="btn btn-sm btn-outline-success"
                                    CommandName="Reactivar"
                                    Visible='<%# !(bool)Eval("Activo") %>'
                                    CommandArgument='<%# Eval("Id") %>'>
                                    <i class="bi bi-arrow-clockwise me-1"></i> Reactivar
                                </asp:LinkButton>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>


        </div>
        <asp:HiddenField ID="hfIdMedico" runat="server" />
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
