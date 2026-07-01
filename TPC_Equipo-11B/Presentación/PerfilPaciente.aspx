<%@ Page Title="" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="PerfilPaciente.aspx.cs" Inherits="Presentación.PerfilPaciente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="server">

    <asp:Label ID="lblError" runat="server" CssClass="alert alert-danger d-block" Visible="false" />
    <asp:Panel ID="pnlContenido" runat="server">

        <!-- ENCABEZADO -->
        <div class="card shadow-sm border-0 mb-4">
            <div class="card-body">

                <div class="d-flex justify-content-between align-items-start">

                    <div class="d-flex align-items-center gap-3">

                        <div class="rounded-circle bg-light d-flex align-items-center justify-content-center" style="width: 64px; height: 64px;">
                            <i class="bi bi-person-fill fs-1 text-secondary"></i>
                        </div>

                        <div>
                            <h2 class="fw-bold mb-1">
                                <asp:Literal ID="litNombreCompleto" runat="server" />
                            </h2>

                            <p class="text-muted mb-0">
                                DNI
                                <asp:Literal ID="litDni" runat="server" /> &nbsp;•&nbsp; <asp:Literal ID="litGenero" runat="server" />
                            </p>
                        </div>

                    </div>

                    <span id="badgeEstado" runat="server" class="badge"></span>

                </div>

                <hr />

                <div class="row g-3">

                    <div class="col-md-4">
                        <small class="text-muted d-block">Email</small>
                        <span class="fw-semibold">
                            <asp:Literal ID="litEmail" runat="server" />
                        </span>
                    </div>

                    <div class="col-md-4">
                        <small class="text-muted d-block">Teléfono</small>
                        <span class="fw-semibold">
                            <asp:Literal ID="litTelefono" runat="server" />
                        </span>
                    </div>

                    <div class="col-md-4">
                        <small class="text-muted d-block">Obra Social</small>
                        <span class="fw-semibold">
                            <asp:Literal ID="litObraSocial" runat="server" />
                        </span>
                    </div>

                    <div class="col-md-4">
                        <small class="text-muted d-block">Dirección</small>
                        <span class="fw-semibold">
                            <asp:Literal ID="litDireccion" runat="server" />
                        </span>
                    </div>

                    <div class="col-md-4">
                        <small class="text-muted d-block">Fecha de nacimiento</small>
                        <span class="fw-semibold">
                            <asp:Literal ID="litFechaNacimiento" runat="server" />
                        </span>
                    </div>

                    <div class="col-md-4">
                        <small class="text-muted d-block">Paciente desde</small>
                        <span class="fw-semibold">
                            <asp:Literal ID="litFechaAlta" runat="server" />
                        </span>
                    </div>

                </div>
            </div>
        </div>

        <div class="row g-4">

            <!-- Próximo turno -->
            <div class="col-md-6">

                <div class="card shadow-sm border-0 h-100">

                    <div class="card-header bg-white">
                        <h5 class="mb-0">
                            <i class="bi bi-calendar-check me-2"></i>
                            Próximo Turno
                        </h5>
                    </div>

                    <div class="card-body">

                        <asp:Panel ID="pnlProximoTurno" runat="server">

                            <p>
                                <strong>Fecha:</strong>
                                <asp:Literal ID="litFechaTurno" runat="server" />
                            </p>
                            <p>
                                <strong>Hora:</strong>
                                <asp:Literal ID="litHoraTurno" runat="server" />
                            </p>
                            <p>
                                <strong>Médico:</strong>
                                <asp:Literal ID="litMedico" runat="server" />
                            </p>
                            <p>
                                <strong>Especialidad:</strong>
                                <asp:Literal ID="litEspecialidad" runat="server" />
                            </p>
                            <p>
                                <strong>Estado:</strong>
                                <asp:Literal ID="litEstadoTurno" runat="server" />
                            </p>

                        </asp:Panel>

                        <asp:Label ID="lblSinTurno" runat="server"
                            CssClass="text-muted"
                            Text="No posee turnos próximos."
                            Visible="false" />

                    </div>

                </div>

            </div>

            <!-- Resumen clínico -->
            <div class="col-md-6">

                <div class="card shadow-sm border-0 h-100">

                    <div class="card-header bg-white">
                        <h5 class="mb-0">
                            <i class="bi bi-file-earmark-medical me-2"></i>
                            Resumen Clínico
                        </h5>
                    </div>

                    <div class="card-body">

                        <p>
                            <strong>Historias Clínicas:</strong>
                            <asp:Literal ID="litHistorias" runat="server" />
                        </p>
                        <p>
                            <strong>Última consulta:</strong>
                            <asp:Literal ID="litUltimaConsulta" runat="server" />
                        </p>
                        <p>
                            <strong>Último diagnóstico:</strong>
                            <asp:Literal ID="litDiagnostico" runat="server" />
                        </p>

                    </div>

                </div>

            </div>

            <!-- Últimos turnos -->
            <div class="col-12">

                <div class="card shadow-sm border-0">

                    <div class="card-header bg-white">
                        <h5 class="mb-0">
                            <i class="bi bi-clock-history me-2"></i>
                            Últimos Turnos
                        </h5>
                    </div>

                    <div class="card-body">

                        <asp:Repeater ID="rptTurnos" runat="server">

                            <HeaderTemplate>

                                <table class="table table-hover">

                                    <thead>
                                        <tr>
                                            <th>Fecha</th>
                                            <th>Médico</th>
                                            <th>Especialidad</th>
                                            <th>Estado</th>
                                        </tr>
                                    </thead>

                                    <tbody>
                            </HeaderTemplate>

                            <ItemTemplate>

                                <tr>
                                    <td><%# ((DateTime)Eval("FechaHora")).ToString("dd/MM/yyyy HH:mm") %></td>
                                    <td>Dr. <%# Eval("Medico.Usuario.Apellido") %>, <%# Eval("Medico.Usuario.Nombre") %></td>
                                    <td><%# Eval("Especialidad.Nombre") %></td>
                                    <td><%# Eval("EstadoTurno.Nombre") %></td>
                                </tr>
                            </ItemTemplate>

                            <FooterTemplate>
                                </tbody>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>

                        <asp:Label ID="lblSinHistorial"
                            runat="server"
                            CssClass="text-muted"
                            Text="No hay turnos registrados."
                            Visible="false" />
                    </div>
                </div>
            </div>
        </div>

        <div class="mt-4">
            <a href="Pacientes.aspx" class="btn btn-outline-secondary">
                <i class="bi bi-arrow-left me-1"></i>
                Volver a Pacientes
            </a>
        </div>
    </asp:Panel>
</asp:Content>
