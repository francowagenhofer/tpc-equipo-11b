<%@ Page Title="Perfil del Médico" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="PerfilMedico.aspx.cs" Inherits="Presentación.PerfilMedico" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="server">

    <asp:Label ID="lblError" runat="server" CssClass="alert alert-danger d-block" Visible="false" />

    <asp:Panel ID="pnlContenido" runat="server">

        <!-- ENCABEZADO -->
        <div class="card card-custom p-4 shadow-sm border-0 mb-4">
            <div class="d-flex justify-content-between align-items-start">
                <div class="d-flex align-items-center gap-3">

                    <asp:Image
                        ID="imgPerfil"
                        runat="server"
                        CssClass="rounded-circle border"
                        Width="64"
                        Height="64"
                        ImageUrl="~/Assets/IMG/Perfil.jpg" />

                    <div>
                        <h2 class="fw-bold mb-1">Dr.
                            <asp:Literal ID="litNombreCompleto" runat="server" />
                        </h2>
                        <p class="text-muted mb-0">
                            <asp:Literal ID="litEspecialidad" runat="server" />
                            &middot; Matrícula
                            <asp:Literal ID="litMatricula" runat="server" />
                        </p>
                    </div>
                </div>
                <span id="badgeEstado" runat="server" class="badge">Estado</span>
            </div>

            <hr />

            <div class="row g-3">
                <div class="col-md-6">
                    <small class="text-muted d-block">Email</small>
                    <span class="fw-semibold">
                        <asp:Literal ID="litEmail" runat="server" /></span>
                </div>
                <div class="col-md-6">
                    <small class="text-muted d-block">Teléfono</small>
                    <span class="fw-semibold">
                        <asp:Literal ID="litTelefono" runat="server" /></span>
                </div>
            </div>
        </div>

        <!-- AVISO SIN DISPONIBILIDAD -->
        <asp:Panel ID="pnlAvisoSinDisponibilidad" runat="server" CssClass="alert alert-warning d-flex align-items-start gap-3 mb-4" Visible="false">
            <i class="bi bi-exclamation-triangle-fill fs-4 mt-1"></i>
            <div>
                <strong>Este médico no tiene disponibilidad configurada.</strong>
                <p class="mb-0 small">No podrá recibir turnos hasta que configure sus horarios de atención semanal.</p>
            </div>
        </asp:Panel>

        <div class="row g-4">

            <!-- DISPONIBILIDAD SEMANAL -->
            <div class="col-md-6">
                <div class="card shadow-sm border-0 h-100">
                    <div class="card-header bg-white">
                        <h5 class="mb-0"><i class="bi bi-calendar-week me-2"></i>Disponibilidad Semanal</h5>
                    </div>
                    <div class="card-body">
                        <asp:Repeater ID="rptDisponibilidad" runat="server">
                            <HeaderTemplate>
                                <table class="table table-sm">
                                    <thead>
                                        <tr>
                                            <th>Día</th>
                                            <th>Desde</th>
                                            <th>Hasta</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><%# ObtenerNombreDia((int)Eval("DiaSemana")) %></td>
                                    <td><%# ((TimeSpan)Eval("HoraInicio")).ToString(@"hh\:mm") %></td>
                                    <td><%# ((TimeSpan)Eval("HoraFin")).ToString(@"hh\:mm") %></td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                        <asp:Label ID="lblSinDisponibilidad" runat="server" CssClass="text-muted small" Text="No hay horarios configurados." Visible="false" />
                    </div>
                </div>
            </div>

            <!-- AUSENCIAS -->
            <div class="col-md-6">
                <div class="card shadow-sm border-0 h-100">
                    <div class="card-header bg-white">
                        <h5 class="mb-0"><i class="bi bi-calendar-x me-2"></i>Ausencias Registradas</h5>
                    </div>
                    <div class="card-body">
                        <asp:Repeater ID="rptAusencias" runat="server">
                            <HeaderTemplate>
                                <table class="table table-sm">
                                    <thead>
                                        <tr>
                                            <th>Fecha</th>
                                            <th>Motivo</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><%# ((DateTime)Eval("Fecha")).ToString("dd/MM/yyyy") %></td>
                                    <td><%# Eval("Motivo") %></td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                        <asp:Label ID="lblSinAusencias" runat="server" CssClass="text-muted small" Text="No hay ausencias registradas." Visible="false" />
                    </div>
                </div>
            </div>

            <!-- PRÓXIMOS TURNOS -->
            <div class="col-12">
                <div class="card shadow-sm border-0">
                    <div class="card-header bg-white">
                        <h5 class="mb-0"><i class="bi bi-calendar-check me-2"></i>Próximos Turnos</h5>
                    </div>
                    <div class="card-body">
                        <asp:Repeater ID="rptTurnos" runat="server">
                            <HeaderTemplate>
                                <table class="table table-sm table-hover">
                                    <thead>
                                        <tr>
                                            <th>Código</th>
                                            <th>Paciente</th>
                                            <th>Fecha y Hora</th>
                                            <th>Estado</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td class="fw-semibold"><%# Eval("Codigo") %></td>
                                    <td><%# Eval("Paciente.Usuario.Apellido") %>, <%# Eval("Paciente.Usuario.Nombre") %></td>
                                    <td><%# ((DateTime)Eval("FechaHora")).ToString("dd/MM/yyyy HH:mm") %> hs</td>
                                    <td><%# Eval("EstadoTurno.Nombre") %></td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                        <asp:Label ID="lblSinTurnos" runat="server" CssClass="text-muted small" Text="No hay turnos próximos agendados." Visible="false" />
                    </div>
                </div>
            </div>

        </div>

        <div class="mt-4 d-flex gap-2">
            <a href="Medicos.aspx" class="btn btn-outline-secondary">
                <i class="bi bi-arrow-left me-1"></i>Volver a Médicos
            </a>
            <a href='MiDisponibilidad.aspx?idMedico=<%= Request.QueryString["id"] %>' class="btn btn-primary">
                <i class="bi bi-calendar-plus me-1"></i>Configurar Disponibilidad
            </a>
        </div>



    </asp:Panel>

</asp:Content>
