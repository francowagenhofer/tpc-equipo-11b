<%@ Page Title="" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Presentación.Default" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Assets/CSS/Default.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="server">


    <!-- ADMINISTRADOR -->
    <asp:Panel ID="pnlAdministrador" runat="server" Visible="false">

        <div class="grid-doble mb-4">

            <div class="tarjeta-credencial credencial-layout">
                <div class="credencial-datos">
                    <h3>
                        <asp:Label ID="lblAdminNombre" runat="server" /></h3>
                    <hr />
                    <p>
                        <strong>Rol:</strong>
                        <asp:Label ID="lblAdminRol" runat="server" />
                    </p>
                    <p>
                        <strong>Email:</strong>
                        <asp:Label ID="lblAdminEmail" runat="server" />
                    </p>
                    <p>
                        <strong>Teléfono:</strong>
                        <asp:Label ID="lblAdminTelefono" runat="server" />
                    </p>
                    <p><strong>Estado:</strong> <span class="badge bg-success">Activo</span></p>
                </div>
                <div class="credencial-imagen">
                    <asp:Image ID="imgAdmin" runat="server" CssClass="img-fluid rounded-circle" AlternateText="Administrador" />
                </div>
            </div>

            <div class="tarjeta-destacada">
                <h5>Sistema Clínico</h5>
                <h3>Panel de Administración</h3>
                <hr />
                <p>
                    <strong>Fecha:</strong>
                    <asp:Label ID="lblFechaActual" runat="server" />
                </p>
                <p>
                    <strong>Último acceso:</strong>
                    <asp:Label ID="lblUltimoAcceso" runat="server" Text="Hoy" />
                </p>
                <p><strong>Estado:</strong> <span class="badge bg-success">Operativo</span></p>
            </div>

        </div>

        <div class="row g-3 mb-4">

            <div class="col-lg-3 col-md-6">
                <div class="dashboard-card text-center">
                    <h6>Pacientes</h6>
                    <h2>
                        <asp:Label ID="lblTotalPacientes" runat="server" Text="0" /></h2>
                </div>
            </div>

            <div class="col-lg-3 col-md-6">
                <div class="dashboard-card text-center">
                    <h6>Médicos</h6>
                    <h2>
                        <asp:Label ID="lblTotalMedicos" runat="server" Text="0" /></h2>
                </div>
            </div>

            <div class="col-lg-3 col-md-6">
                <div class="dashboard-card text-center">
                    <h6>Usuarios</h6>
                    <h2>
                        <asp:Label ID="lblTotalUsuarios" runat="server" Text="0" /></h2>
                </div>
            </div>

            <div class="col-lg-3 col-md-6">
                <div class="dashboard-card text-center">
                    <h6>Turnos de Hoy</h6>
                    <h2>
                        <asp:Label ID="lblTurnosHoy" runat="server" Text="0" /></h2>
                </div>
            </div>

        </div>

        <div class="tarjeta-seccion mb-4">
            <h5>Acciones rápidas</h5>
            <div class="grupo-botones">
                <asp:HyperLink ID="lnkPacientes" runat="server" NavigateUrl="~/Pacientes.aspx" CssClass="btn-accion btn-verde">Gestionar Pacientes</asp:HyperLink>
                <asp:HyperLink ID="lnkMedicos" runat="server" NavigateUrl="~/Medicos.aspx" CssClass="btn-accion btn-azul">Gestionar Médicos</asp:HyperLink>
                <asp:HyperLink ID="lnkTurnos" runat="server" NavigateUrl="~/Turnos.aspx" CssClass="btn-accion">Gestionar Turnos</asp:HyperLink>
                <asp:HyperLink ID="lnkUsuarios" runat="server" NavigateUrl="~/Usuarios.aspx" CssClass="btn-accion">Gestionar Usuarios</asp:HyperLink>
                <asp:HyperLink ID="lnkConfiguracion" runat="server" NavigateUrl="~/Configuracion/Default.aspx" CssClass="btn-accion">Configuración</asp:HyperLink>
            </div>
        </div>

        <div class="grid-doble">

            <div class="tarjeta-seccion">
                <h5>Resumen del sistema</h5>
                <table class="table table-borderless mb-0">
                    <tr>
                        <td>Turnos pendientes</td>
                        <td class="text-end"><strong>
                            <asp:Label ID="lblTurnosPendientes" runat="server" Text="0" /></strong></td>
                    </tr>
                    <tr>
                        <td>Turnos confirmados</td>
                        <td class="text-end"><strong>
                            <asp:Label ID="lblTurnosConfirmados" runat="server" Text="0" /></strong></td>
                    </tr>
                    <tr>
                        <td>Turnos cancelados hoy</td>
                        <td class="text-end"><strong>
                            <asp:Label ID="lblTurnosCancelados" runat="server" Text="0" /></strong></td>
                    </tr>
                    <tr>
                        <td>Historias clínicas</td>
                        <td class="text-end"><strong>
                            <asp:Label ID="lblHistoriasClinicas" runat="server" Text="0" /></strong></td>
                    </tr>
                </table>
            </div>

            <div class="tarjeta-seccion">
                <h5>Indicadores</h5>
                <table class="table table-borderless mb-0">
                    <tr>
                        <td>Especialidad más solicitada</td>
                        <td class="text-end"><strong>
                            <asp:Label ID="lblEspecialidadMasSolicitada" runat="server" Text="-" /></strong></td>
                    </tr>
                    <tr>
                        <td>Obra Social más utilizada</td>
                        <td class="text-end"><strong>
                            <asp:Label ID="lblObraSocialMasUtilizada" runat="server" Text="-" /></strong></td>
                    </tr>
                    <tr>
                        <td>Usuarios activos</td>
                        <td class="text-end"><strong>
                            <asp:Label ID="lblUsuariosActivos" runat="server" Text="0" /></strong></td>
                    </tr>
                    <tr>
                        <td>Estado del sistema</td>
                        <td class="text-end"><span class="badge bg-success">Operativo</span></td>
                    </tr>
                </table>
            </div>
        </div>
    </asp:Panel>

    <!-- RECEPCIONISTA -->
    <asp:Panel ID="pnlRecepcionista" runat="server" Visible="false">

        <div class="grid-doble mb-4">

            <div class="tarjeta-credencial credencial-layout">
                <div class="credencial-datos">
                    <h3>
                        <asp:Label ID="lblRecepNombre" runat="server" /></h3>
                    <hr />
                    <p>
                        <strong>Rol:</strong>
                        <asp:Label ID="lblRecepRol" runat="server" />
                    </p>
                    <p>
                        <strong>Email:</strong>
                        <asp:Label ID="lblRecepEmail" runat="server" />
                    </p>
                    <p>
                        <strong>Teléfono:</strong>
                        <asp:Label ID="lblRecepTelefono" runat="server" />
                    </p>
                    <p><strong>Estado:</strong> <span class="badge bg-success">Activo</span></p>
                </div>
                <div class="credencial-imagen">
                    <asp:Image ID="imgRecepcionista" runat="server" CssClass="img-fluid rounded-circle" AlternateText="Recepcionista" />
                </div>
            </div>

            <div class="tarjeta-destacada">
                <h5>Resumen del día</h5>
                <h3>Recepción</h3>
                <hr />
                <p>
                    <strong>Turnos de hoy:</strong>
                    <asp:Label ID="lblRecepTurnosHoy" runat="server" Text="0" />
                </p>
                <p>
                    <strong>Última actualización:</strong>
                    <asp:Label ID="lblRecepActualizacion" runat="server" />
                </p>
                <p><strong>Estado:</strong> <span class="badge bg-success">Operativo</span></p>
            </div>

        </div>

        <div class="row g-3 mb-4">

            <div class="col-lg-3 col-md-6">
                <div class="dashboard-card text-center">
                    <h6>Turnos Hoy</h6>
                    <h2>
                        <asp:Label ID="lblRecepKpiTurnosHoy" runat="server" Text="0" /></h2>
                </div>
            </div>

            <div class="col-lg-3 col-md-6">
                <div class="dashboard-card text-center">
                    <h6>Confirmados</h6>
                    <h2>
                        <asp:Label ID="lblRecepKpiConfirmados" runat="server" Text="0" /></h2>
                </div>
            </div>

            <div class="col-lg-3 col-md-6">
                <div class="dashboard-card text-center">
                    <h6>Pendientes</h6>
                    <h2>
                        <asp:Label ID="lblRecepKpiPendientes" runat="server" Text="0" /></h2>
                </div>
            </div>

            <div class="col-lg-3 col-md-6">
                <div class="dashboard-card text-center">
                    <h6>Cancelados</h6>
                    <h2>
                        <asp:Label ID="lblRecepKpiCancelados" runat="server" Text="0" /></h2>
                </div>
            </div>

        </div>

        <div class="tarjeta-seccion mb-4">
            <h5>Acciones rápidas</h5>
            <div class="grupo-botones">
                <asp:HyperLink ID="lnkNuevoPaciente" runat="server" NavigateUrl="~/NuevoPaciente.aspx" CssClass="btn-accion btn-verde">Nuevo Paciente</asp:HyperLink>
                <asp:HyperLink ID="lnkNuevoTurno" runat="server" NavigateUrl="~/NuevoTurno.aspx" CssClass="btn-accion btn-azul">Nuevo Turno</asp:HyperLink>
                <asp:HyperLink ID="lnkGestionTurnos" runat="server" NavigateUrl="~/Turnos.aspx" CssClass="btn-accion">Gestionar Turnos</asp:HyperLink>
                <asp:HyperLink ID="lnkBuscarPaciente" runat="server" NavigateUrl="~/Pacientes.aspx" CssClass="btn-accion">Buscar Paciente</asp:HyperLink>
            </div>
        </div>

        <div class="grid-doble">

            <div class="tarjeta-seccion">
                <h5>Resumen operativo</h5>
                <table class="table table-borderless mb-0">
                    <tr>
                        <td>Pacientes registrados</td>
                        <td class="text-end"><strong>
                            <asp:Label ID="lblRecepPacientesRegistrados" runat="server" Text="0" /></strong></td>
                    </tr>
                    <tr>
                        <td>Turnos creados hoy</td>
                        <td class="text-end"><strong>
                            <asp:Label ID="lblRecepTurnosCreados" runat="server" Text="0" /></strong></td>
                    </tr>
                    <tr>
                        <td>Turnos reprogramados</td>
                        <td class="text-end"><strong>
                            <asp:Label ID="lblRecepTurnosReprogramados" runat="server" Text="0" /></strong></td>
                    </tr>
                    <tr>
                        <td>Cancelaciones</td>
                        <td class="text-end"><strong>
                            <asp:Label ID="lblRecepCancelaciones" runat="server" Text="0" /></strong></td>
                    </tr>
                </table>
            </div>

            <div class="tarjeta-seccion">
                <h5>Agenda del día</h5>

                <asp:GridView ID="gvTurnosRecepcion" runat="server" AutoGenerateColumns="False" CssClass="table table-hover table-striped align-middle">
                    <Columns>
                        <asp:BoundField DataField="FechaHora"
                            HeaderText="Hora"
                            DataFormatString="{0:HH:mm}" />

                        <asp:TemplateField HeaderText="Paciente">
                            <ItemTemplate>
                                <%# Eval("Paciente.Usuario.Apellido") %>, <%# Eval("Paciente.Usuario.Nombre") %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Médico">
                            <ItemTemplate>
                                <%# Eval("Medico.Usuario.Apellido") %>, <%# Eval("Medico.Usuario.Nombre") %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Estado">
                            <ItemTemplate>
                                <%# Eval("EstadoTurno.Nombre") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

        </div>
    </asp:Panel>

    <!-- MÉDICO -->
    <asp:Panel ID="pnlMedico" runat="server" Visible="false">

        <div class="grid-doble mb-4">

            <div class="tarjeta-credencial credencial-layout">
                <div class="credencial-datos">
                    <h3>
                        <asp:Label ID="lblMedicoNombre" runat="server" /></h3>
                    <hr />
                    <p>
                        <strong>Matrícula:</strong>
                        <asp:Label ID="lblMedicoMatricula" runat="server" />
                    </p>
                    <p>
                        <strong>Especialidad:</strong>
                        <asp:Label ID="lblMedicoEspecialidad" runat="server" />
                    </p>
                    <p>
                        <strong>Email:</strong>
                        <asp:Label ID="lblMedicoEmail" runat="server" />
                    </p>
                    <p>
                        <strong>Teléfono:</strong>
                        <asp:Label ID="lblMedicoTelefono" runat="server" />
                    </p>
                    <p><strong>Estado:</strong> <span class="badge bg-success">Activo</span></p>
                </div>
                <div class="credencial-imagen">
                    <asp:Image ID="imgMedico" runat="server" CssClass="img-fluid rounded-circle" AlternateText="Médico" />
                </div>
            </div>

            <div class="tarjeta-destacada">
                <h5>Próximo turno</h5>
                <h3>
                    <asp:Label ID="lblMedicoProximoPaciente" runat="server" Text="Sin turnos" /></h3>
                <hr />
                <p>
                    <strong>Hora:</strong>
                    <asp:Label ID="lblMedicoProximaHora" runat="server" Text="-" />
                </p>
                <p>
                    <strong>Motivo:</strong>
                    <asp:Label ID="lblMedicoMotivo" runat="server" Text="-" />
                </p>
                <p>
                    <strong>Estado:</strong>
                    <asp:Label ID="lblMedicoEstadoTurno" runat="server" Text="-" />
                </p>
                <asp:HyperLink ID="lnkAtenderPaciente" runat="server" NavigateUrl="~/MiAgenda.aspx" CssClass="btn-accion btn-verde mt-3">Ir a mi agenda</asp:HyperLink>
            </div>

        </div>

        <div class="row g-3 mb-4">

            <div class="col-lg-3 col-md-6">
                <div class="dashboard-card text-center">
                    <h6>Turnos Hoy</h6>
                    <h2>
                        <asp:Label ID="lblMedicoKpiTurnosHoy" runat="server" Text="0" /></h2>
                </div>
            </div>

            <div class="col-lg-3 col-md-6">
                <div class="dashboard-card text-center">
                    <h6>Pendientes</h6>
                    <h2>
                        <asp:Label ID="lblMedicoKpiPendientes" runat="server" Text="0" /></h2>
                </div>
            </div>

            <div class="col-lg-3 col-md-6">
                <div class="dashboard-card text-center">
                    <h6>Finalizados</h6>
                    <h2>
                        <asp:Label ID="lblMedicoKpiFinalizados" runat="server" Text="0" /></h2>
                </div>
            </div>

            <div class="col-lg-3 col-md-6">
                <div class="dashboard-card text-center">
                    <h6>Cancelados</h6>
                    <h2>
                        <asp:Label ID="lblMedicoKpiCancelados" runat="server" Text="0" /></h2>
                </div>
            </div>

        </div>

        <div class="tarjeta-seccion mb-4">
            <h5>Acciones rápidas</h5>
            <div class="grupo-botones">
                <asp:HyperLink ID="lnkMiAgenda" runat="server" NavigateUrl="~/MiAgenda.aspx" CssClass="btn-accion btn-verde">Mi Agenda</asp:HyperLink>
                <asp:HyperLink ID="lnkHistoriasClinicas" runat="server" NavigateUrl="~/HistoriaClinica.aspx" CssClass="btn-accion btn-azul">Historias Clínicas</asp:HyperLink>
                <asp:HyperLink ID="lnkDisponibilidad" runat="server" NavigateUrl="~/MiDisponibilidad.aspx" CssClass="btn-accion">Mi Disponibilidad</asp:HyperLink>
            </div>
        </div>

        <div class="grid-doble">
            <div class="tarjeta-seccion">
                <h5>Agenda del día</h5>

                <asp:GridView ID="gvAgendaMedico" runat="server" AutoGenerateColumns="False" CssClass="table table-hover table-striped align-middle">
                    <Columns>
                        <asp:BoundField HeaderText="Hora"
                            DataField="FechaHora"
                            DataFormatString="{0:HH:mm}" />

                        <asp:TemplateField HeaderText="Paciente">
                            <ItemTemplate>
                                <%# Eval("Paciente.Usuario.Apellido") %>,
                                <%# Eval("Paciente.Usuario.Nombre") %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Estado">
                            <ItemTemplate>
                                <%# Eval("EstadoTurno.Nombre") %>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>
            </div>

            <div class="tarjeta-seccion">
                <h5>Actividad clínica</h5>
                <table class="table table-borderless mb-0">
                    <tr>
                        <td>Consultas del día</td>
                        <td class="text-end"><strong>
                            <asp:Label ID="lblMedicoConsultasHoy" runat="server" Text="0" /></strong></td>
                    </tr>
                    <tr>
                        <td>Pacientes atendidos</td>
                        <td class="text-end"><strong>
                            <asp:Label ID="lblMedicoPacientesAtendidos" runat="server" Text="0" /></strong></td>
                    </tr>
                    <tr>
                        <td>No asistieron</td>
                        <td class="text-end"><strong>
                            <asp:Label ID="lblMedicoAusentes" runat="server" Text="0" /></strong></td>
                    </tr>
                    <tr>
                        <td>Consultas pendientes</td>
                        <td class="text-end"><strong>
                            <asp:Label ID="lblMedicoPendientes" runat="server" Text="0" /></strong></td>
                    </tr>
                </table>
            </div>

        </div>

    </asp:Panel>

    <!-- PACIENTE -->
    <asp:Panel ID="pnlPaciente" runat="server" Visible="false">

        <div class="grid-doble mb-4">

            <div class="tarjeta-credencial credencial-layout">
                <div class="credencial-datos">
                    <h3>
                        <asp:Label ID="lblPacienteNombre" runat="server" /></h3>
                    <hr />
                    <p>
                        <strong>DNI:</strong>
                        <asp:Label ID="lblPacienteDni" runat="server" />
                    </p>
                    <p>
                        <strong>Email:</strong>
                        <asp:Label ID="lblPacienteEmail" runat="server" />
                    </p>
                    <p>
                        <strong>Teléfono:</strong>
                        <asp:Label ID="lblPacienteTelefono" runat="server" />
                    </p>
                    <p>
                        <strong>Obra Social:</strong>
                        <asp:Label ID="lblPacienteObraSocial" runat="server" />
                    </p>
                    <p>
                        <strong>Género:</strong>
                        <asp:Label ID="lblPacienteGenero" runat="server" />
                    </p>
                    <p>
                        <strong>Paciente desde:</strong>
                        <asp:Label ID="lblPacienteDesde" runat="server" />
                    </p>
                </div>
                <div class="credencial-imagen">
                    <asp:Image ID="imgPaciente" runat="server" CssClass="img-fluid rounded-circle" AlternateText="Paciente" />
                </div>
            </div>

            <div class="tarjeta-destacada">
                <h5>Próximo turno</h5>
                <h3>
                    <asp:Label ID="lblPacienteProximoTurno" runat="server" Text="Sin turnos programados" /></h3>
                <hr />
                <p>
                    <strong>Hora:</strong>
                    <asp:Label ID="lblPacienteHoraTurno" runat="server" Text="-" />
                </p>
                <p>
                    <strong>Médico:</strong>
                    <asp:Label ID="lblPacienteMedico" runat="server" Text="-" />
                </p>
                <p>
                    <strong>Especialidad:</strong>
                    <asp:Label ID="lblPacienteEspecialidad" runat="server" Text="-" />
                </p>
                <p>
                    <strong>Estado:</strong>
                    <asp:Label ID="lblPacienteEstadoTurno" runat="server" Text="-" />
                </p>
                <asp:HyperLink ID="lnkPacienteTurnos" runat="server" NavigateUrl="~/MiAgenda.aspx" CssClass="btn-accion btn-verde mt-3">Ver mis turnos</asp:HyperLink>
            </div>

        </div>

        <div class="row g-3 mb-4">

            <div class="col-lg-3 col-md-6">
                <div class="dashboard-card text-center">
                    <h6>Turnos Totales</h6>
                    <h2>
                        <asp:Label ID="lblPacienteKpiTurnos" runat="server" Text="0" /></h2>
                </div>
            </div>

            <div class="col-lg-3 col-md-6">
                <div class="dashboard-card text-center">
                    <h6>Próximos</h6>
                    <h2>
                        <asp:Label ID="lblPacienteKpiPendientes" runat="server" Text="0" /></h2>
                </div>
            </div>

            <div class="col-lg-3 col-md-6">
                <div class="dashboard-card text-center">
                    <h6>Finalizados</h6>
                    <h2>
                        <asp:Label ID="lblPacienteKpiFinalizados" runat="server" Text="0" /></h2>
                </div>
            </div>

            <div class="col-lg-3 col-md-6">
                <div class="dashboard-card text-center">
                    <h6>Historias Clínicas</h6>
                    <h2>
                        <asp:Label ID="lblPacienteKpiHistorias" runat="server" Text="0" /></h2>
                </div>
            </div>

        </div>

        <div class="tarjeta-seccion mb-4">
            <h5>Acciones rápidas</h5>
            <div class="grupo-botones">
                <asp:HyperLink ID="lnkSolicitarTurno" runat="server" NavigateUrl="~/NuevoTurno.aspx" CssClass="btn-accion btn-verde">Solicitar turno</asp:HyperLink>
                <asp:HyperLink ID="lnkMisTurnos" runat="server" NavigateUrl="~/MiAgenda.aspx" CssClass="btn-accion btn-azul">Mis turnos</asp:HyperLink>
                <asp:HyperLink ID="lnkHistoriaPaciente" runat="server" NavigateUrl="~/HistoriaClinica.aspx" CssClass="btn-accion">Historia clínica</asp:HyperLink>
                <asp:HyperLink ID="lnkPerfilPaciente" runat="server" NavigateUrl="~/MiPerfil.aspx" CssClass="btn-accion">Mi perfil</asp:HyperLink>
            </div>
        </div>

        <div class="grid-doble">

            <div class="tarjeta-seccion">
                <h5>Últimos turnos</h5>

                <asp:GridView ID="gvUltimosTurnosPaciente" runat="server" AutoGenerateColumns="False" CssClass="table table-hover table-striped align-middle">
                    <Columns>
                        <asp:BoundField DataField="FechaHora"
                            HeaderText="Hora"
                            DataFormatString="{0:HH:mm}" />

                        <asp:TemplateField HeaderText="Médico">
                            <ItemTemplate>
                                <%# Eval("Medico.Usuario.Apellido") %>, <%# Eval("Medico.Usuario.Nombre") %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Especialidad">
                            <ItemTemplate>
                                <%# Eval("Especialidad.Nombre") %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Estado">
                            <ItemTemplate>
                                <%# Eval("EstadoTurno.Nombre") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

            <div class="tarjeta-seccion">
                <h5>Resumen clínico</h5>
                <table class="table table-borderless mb-0">
                    <tr>
                        <td>Consultas realizadas</td>
                        <td class="text-end"><strong>
                            <asp:Label ID="lblPacienteConsultas" runat="server" Text="0" /></strong></td>
                    </tr>
                    <tr>
                        <td>Última consulta</td>
                        <td class="text-end"><strong>
                            <asp:Label ID="lblPacienteUltimaConsulta" runat="server" Text="-" /></strong></td>
                    </tr>
                    <tr>
                        <td>Último diagnóstico</td>
                        <td class="text-end"><strong>
                            <asp:Label ID="lblPacienteDiagnostico" runat="server" Text="-" /></strong></td>
                    </tr>
                    <tr>
                        <td>Próximo control</td>
                        <td class="text-end"><strong>
                            <asp:Label ID="lblPacienteProximoControl" runat="server" Text="-" /></strong></td>
                    </tr>
                </table>
            </div>
        </div>
    </asp:Panel>

</asp:Content>
