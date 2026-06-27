<%@ Page Title="" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="MiDisponibilidad.aspx.cs" Inherits="Presentación.MiDisponibilidad" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="server">

    <asp:Panel ID="pnlAvisoSinConfigurar" runat="server" CssClass="alert alert-warning d-flex align-items-start gap-3 mb-4" Visible="false">
    <i class="bi bi-exclamation-triangle-fill fs-4 mt-1"></i>
    <div>
        <strong>Todavía no configuraste tu disponibilidad.</strong>
        <p class="mb-0 small">
            Los pacientes no podrán reservar turnos con vos hasta que definas tus días y horarios de atención más abajo.
        </p>
    </div>
    </asp:Panel>

    <asp:Panel ID="pnlEncabezadoAdmin" runat="server" CssClass="alert alert-info d-flex align-items-center gap-2 mb-4" Visible="false">
    <i class="bi bi-shield-lock-fill fs-5"></i>
    <div>
        Estás configurando la disponibilidad de <strong><asp:Literal ID="litNombreMedicoAdmin" runat="server" /></strong> como Administrador.
        <a href="Medicos.aspx" class="ms-2">Volver a Médicos</a>
    </div>
    </asp:Panel>

    <asp:Panel ID="pnlFormulario" runat="server">

    <!-- FORMULARIO -->
    <div class="card shadow-sm border-0 mb-4">
        <div class="card-header bg-white">
            <h4 class="fw-bold mb-1">Mi disponibilidad
            </h4>
            <p class="text-muted mb-0">
                Configurá los días y horarios semanales en los que atendés pacientes.
           
            </p>
        </div>
        <div class="card-body">
            <asp:HiddenField
                ID="hfIdDisponibilidad"
                runat="server" />
            <asp:Label
                ID="lblMensajeError"
                runat="server"
                CssClass="alert alert-danger d-block"
                Visible="false" />
            <div class="mb-4">
                <label class="form-label fw-semibold">
                    Días de atención
               
                </label>
                <div class="row g-2">
                    <div class="col-md-3">
                        <asp:CheckBox ID="chkLunes" runat="server" Text=" Lunes" CssClass="form-check" />
                    </div>
                    <div class="col-md-3">
                        <asp:CheckBox ID="chkMartes" runat="server" Text=" Martes" CssClass="form-check" />
                    </div>
                    <div class="col-md-3">
                        <asp:CheckBox ID="chkMiercoles" runat="server" Text=" Miércoles" CssClass="form-check" />
                    </div>
                    <div class="col-md-3">
                        <asp:CheckBox ID="chkJueves" runat="server" Text=" Jueves" CssClass="form-check" />
                    </div>
                    <div class="col-md-3">
                        <asp:CheckBox ID="chkViernes" runat="server" Text=" Viernes" CssClass="form-check" />
                    </div>
                    <div class="col-md-3">
                        <asp:CheckBox ID="chkSabado" runat="server" Text=" Sábado" CssClass="form-check" />
                    </div>
                    <div class="col-md-3">
                        <asp:CheckBox ID="chkDomingo" runat="server" Text=" Domingo" CssClass="form-check" />
                    </div>
                </div>
            </div>
            <div class="row">
    <div class="col-md-4">
        <label class="form-label fw-semibold">
            Hora de inicio
       
        </label>
        <asp:DropDownList
            ID="ddlHoraInicio"
            runat="server"
            CssClass="form-select"
            AutoPostBack="true"
            OnSelectedIndexChanged="ddlHoraInicio_SelectedIndexChanged" />
    </div>
    <div class="col-md-4">
        <label class="form-label fw-semibold">
            Hora de finalización
       
        </label>
        <asp:DropDownList
            ID="ddlHoraFin"
            runat="server"
            CssClass="form-select" />
    </div>
    <div class="col-md-4 d-flex align-items-end">
        <asp:Button
            ID="btnGuardarDisponibilidad"
            runat="server"
            CssClass="btn btn-primary w-100"
            Text="Guardar disponibilidad"
            OnClick="btnGuardarDisponibilidad_Click" />
    </div>
</div>
            <hr />
            <small class="text-muted">Seleccioná uno o varios días. El mismo horario será aplicado a todos los días elegidos.
            </small>
        </div>
    </div>

    <!-- TABLA -->
    <div class="card shadow-sm border-0">
        <div class="card-header bg-white d-flex justify-content-between align-items-center">
            <div>
                <h5 class="mb-1">Horarios registrados
                </h5>
                <small class="text-muted">Disponibilidad semanal configurada.
                </small>
            </div>
        </div>
        <div class="card-body">
            <div class="table-responsive">
                <asp:GridView
                    ID="dgvDisponibilidad"
                    runat="server"
                    CssClass="table table-hover align-middle"
                    AutoGenerateColumns="false"
                    GridLines="None"
                    DataKeyNames="Id"
                    EmptyDataText="No hay horarios registrados."
                    OnRowCommand="dgvDisponibilidad_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="Día">
                            <ItemTemplate>
                                <strong>
                                    <%# ObtenerNombreDia((int)Eval("DiaSemana")) %>
                                </strong>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Desde">
                            <ItemTemplate>
                                <%# ((TimeSpan)Eval("HoraInicio")).ToString(@"hh\:mm") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Hasta">
                            <ItemTemplate>
                                <%# ((TimeSpan)Eval("HoraFin")).ToString(@"hh\:mm") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Estado">
                            <ItemTemplate>
                                <span class='<%# (bool)Eval("Activo") ? "badge bg-success" : "badge bg-secondary" %>'>
                                    <%# (bool)Eval("Activo") ? "Activo" : "Inactivo" %>
                                </span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="text-center"
                            ItemStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:LinkButton
                                    ID="btnModificar"
                                    runat="server"
                                    CssClass="btn btn-outline-primary btn-sm me-2"
                                    CommandName="Modificar"
                                    CommandArgument='<%# Eval("Id") %>'>
                                    <i class="bi bi-pencil"></i>
                                </asp:LinkButton>
                                <asp:LinkButton
                                    ID="btnEliminar"
                                    runat="server"
                                    CssClass="btn btn-outline-danger btn-sm"
                                    CommandName="Eliminar"
                                    CommandArgument='<%# Eval("Id") %>'
                                    OnClientClick="return confirm('¿Desea eliminar este horario?');">
                                    <i class="bi bi-trash"></i>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>

    </asp:Panel>

</asp:Content>