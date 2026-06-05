<%@ Page Title="" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Presentación.Default" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Assets/CSS/Default.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="server">


    <!-- ADMINISTRADOR -->
    <asp:Panel ID="pnlAdministrador" runat="server" Visible="false">

        <!--Credencial y resumen general -->

        <div class="grid-doble mb-4">

            <!-- Credencial ADMIN -->
            <div class="tarjeta-credencial credencial-layout">

                <div class="credencial-datos">
                    <h3>Juan Pérez</h3>
                    <hr />

                    <p><strong>Rol:</strong> Administrador</p>
                    <p><strong>Email:</strong> admin@clinica.com</p>
                    <p><strong>Teléfono:</strong> 11-1111-2222</p>
                    <p><strong>Estado:</strong> Activo</p>
                </div>

                <div class="credencial-imagen">
                    <img src="#" alt="Foto administrador" />
                </div>

            </div>

            <!-- Resumen -->
            <div class="tarjeta-destacada">

                <h4>Sistema clínico</h4>
                <h3>Administrador</h3>

                <p><strong>Fecha:</strong> 04/06/2026</p>
                <p><strong>Estado global:</strong> OK</p>

            </div>

        </div>

        <!-- KPIs  -->
        <div class="row g-3 mb-4">

            <div class="col-md-3">
                <div class="dashboard-card">
                    <h6>Pacientes</h6>
                    <h2>245</h2>
                </div>
            </div>

            <div class="col-md-3">
                <div class="dashboard-card">
                    <h6>Médicos</h6>
                    <h2>18</h2>
                </div>
            </div>

            <div class="col-md-3">
                <div class="dashboard-card">
                    <h6>Usuarios</h6>
                    <h2>26</h2>
                </div>
            </div>

            <div class="col-md-3">
                <div class="dashboard-card">
                    <h6>Turnos hoy</h6>
                    <h2>42</h2>
                </div>
            </div>

        </div>


        <!-- CONTROL DEL SISTEMA -->
        <div class="tarjeta-seccion mb-4">

            <h5>Control del sistema</h5>

            <div class="grupo-botones">

                <button class="btn-accion btn-verde">
                    Gestionar Pacientes
                </button>

                <button class="btn-accion btn-azul">
                    Gestionar Médicos
                </button>

                <button class="btn-accion">
                    Gestionar Turnos
                </button>

                <button class="btn-accion">
                    Gestionar Usuarios
                </button>

                <button class="btn-accion">
                    Configuración
                </button>

            </div>

        </div>

        <!-- ESTADO DEL SISTEMA -->

        <div class="grid-doble">

            <!-- ACTIVIDAD RECIENTE -->
            <div class="tarjeta-seccion">

                <h5>Actividad reciente</h5>

                <ul>
                    <li>Nuevo paciente registrado</li>
                    <li>Turno reprogramado</li>
                    <li>Médico agregado al sistema</li>
                    <li>Usuario administrador modificado</li>
                </ul>

            </div>

            <!-- INDICADORES DEL SISTEMA -->
            <div class="tarjeta-seccion">

                <h5>Indicadores del sistema</h5>

                <p><strong>Turnos cancelados hoy:</strong> 3</p>
                <p><strong>Turnos pendientes:</strong> 12</p>
                <p><strong>Especialidad más usada:</strong> Cardiología</p>
                <p><strong>Estado general:</strong> Estable</p>

            </div>

        </div>

    </asp:Panel>


    <!-- RECEPCIONISTA -->
    <asp:Panel ID="pnlRecepcionista" runat="server" Visible="false">


        <!-- Credencial + RESUMEN -->
        <div class="grid-doble mb-4">

            <div class="tarjeta-credencial credencial-layout">

                <div class="credencial-datos">
                    <h3>Carla Gómez</h3>
                    <hr />

                    <p><strong>Rol:</strong> Recepcionista</p>
                    <p><strong>Email:</strong> carla@clinica.com</p>
                    <p><strong>Teléfono:</strong> 11-4567-8910</p>
                    <p><strong>Estado:</strong> Activa</p>
                </div>

                <div class="credencial-imagen">
                    <img src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRVjL5_Oyr2uRZaOTBHrwCkae00brRJyzeVFg&s" alt="Foto recepcionista" />
                </div>

            </div>

            <div class="tarjeta-destacada">

                <h4>Turnos de hoy</h4>
                <h2>42</h2>

                <p><strong>Última actualización:</strong> 09:30 hs</p>

            </div>

        </div>

        <!-- KPIs  -->

        <div class="row g-3 mb-4">

            <div class="col-md-3">
                <div class="tarjeta-kpi">
                    <h6>Atendidos</h6>
                    <h2>126</h2>
                    <p class="kpi-sub">Histórico acumulado</p>
                </div>
            </div>

            <div class="col-md-3">
                <div class="tarjeta-kpi">
                    <h6>Atendidos</h6>
                    <h2>126</h2>
                    <p class="kpi-sub">Histórico acumulado</p>
                </div>
            </div>

            <div class="col-md-3">
                <div class="tarjeta-kpi">
                    <h6>En espera</h6>
                    <h2>3</h2>
                    <p class="kpi-sub">Pacientes pendientes</p>
                </div>
            </div>

            <div class="col-md-3">
                <div class="tarjeta-kpi">
                    <h6>Turnos hoy</h6>
                    <h2>8</h2>
                    <p class="kpi-sub">Agenda total del día</p>
                </div>
            </div>

        </div>


        <!-- GESTIÓN OPERATIVA -->
        <div class="tarjeta-seccion mb-4">

            <h5>Gestión del día</h5>

            <div class="grupo-botones">

                <button class="btn-accion btn-verde">
                    + Nuevo Paciente
                </button>

                <button class="btn-accion btn-azul">
                    + Nuevo Turno
                </button>

                <button class="btn-accion">
                    Reprogramar Turno
                </button>

                <button class="btn-accion">
                    Buscar Paciente
                </button>

            </div>

        </div>

        <!-- TABLA DE TURNOS -->
        <div class="tarjeta-seccion">

            <h5>Turnos del día</h5>

            <table class="tabla-simple">

                <tr>
                    <th>Hora</th>
                    <th>Paciente</th>
                    <th>Médico</th>
                    <th>Estado</th>
                </tr>

                <tr>
                    <td>09:00</td>
                    <td>Juan García</td>
                    <td>Dr. Pérez</td>
                    <td>Confirmado</td>
                </tr>

                <tr>
                    <td>10:30</td>
                    <td>María López</td>
                    <td>Dra. González</td>
                    <td>Pendiente</td>
                </tr>

                <tr>
                    <td>11:15</td>
                    <td>Carlos Díaz</td>
                    <td>Dr. Ruiz</td>
                    <td>Cancelado</td>
                </tr>

            </table>

        </div>

    </asp:Panel>

    <!-- MÉDICO -->
    <asp:Panel ID="pnlMedico" runat="server" Visible="false">

        <div class="grid-doble mb-4">
            <!-- Credencial Médico-->
            <div class="tarjeta-credencial credencial-layout">

                <div class="credencial-datos">
                    <h3>Dr. Juan Pérez</h3>
                    <hr />

                    <p><strong>Matrícula:</strong> MED-1001</p>
                    <p><strong>Email:</strong> jperez@clinica.com</p>
                    <p><strong>Teléfono:</strong> 11-3344-5566</p>

                    <p><strong>Especialidad:</strong> Cardiología</p>
                    <p><strong>Estado:</strong> Activo</p>
                </div>

                <!-- IMAGEN -->
                <div class="credencial-imagen">
                    <img src="https://static.vecteezy.com/system/resources/previews/002/181/615/non_2x/medical-doctor-general-practitioner-physician-profile-avatar-cartoon-vector.jpg" alt="Foto médico" />
                </div>

            </div>

            <!-- Proximo Paciente-->
            <div class="tarjeta-destacada">
                <h4>Próximo paciente</h4>

                <h2>Juan García</h2>

                <p><strong>Hora:</strong> 10:30 hs</p>
                <p><strong>Motivo:</strong> Control cardiológico</p>

                <hr />

                <p><strong>Estado:</strong> Confirmado</p>

                <button class="btn-accion btn-verde mt-3">
                    Iniciar consulta
                </button>
            </div>

        </div>


        <!-- Resumen del día (KPIs) -->
        <div class="grid-triple mb-4">

            <div class="tarjeta-kpi">
                <h6>Turnos hoy</h6>
                <h2>8</h2>
                <p class="kpi-sub">Agenda total del día</p>
            </div>

            <div class="tarjeta-kpi">
                <h6>En espera</h6>
                <h2>3</h2>
                <p class="kpi-sub">Pacientes pendientes</p>
            </div>

            <div class="tarjeta-kpi">
                <h6>Atendidos</h6>
                <h2>126</h2>
                <p class="kpi-sub">Histórico acumulado</p>
            </div>

        </div>

        <!-- Agenda del día -->
        <div class="tarjeta-seccion mb-4">
            <h5>Agenda del día</h5>
            <table class="tabla-simple">
                <tr>
                    <th>Hora</th>
                    <th>Paciente</th>
                    <th>Estado</th>
                </tr>
                <tr>
                    <td>09:00</td>
                    <td>María López</td>
                    <td><span class="estado ok">Finalizado</span></td>
                </tr>
                <tr>
                    <td>10:30</td>
                    <td>Juan García</td>
                    <td><span class="estado info">En curso</span></td>
                </tr>
                <tr>
                    <td>11:15</td>
                    <td>Carlos Díaz</td>
                    <td><span class="estado warn">Pendiente</span></td>
                </tr>
                <tr>
                    <td>12:00</td>
                    <td>Lucía Fernández</td>
                    <td><span class="estado warn">Pendiente</span></td>
                </tr>

            </table>
        </div>


        <!-- Resumen clinico operativo-->
        <div class="grid-doble">

            <!-- Pacientes recientes-->
            <div class="tarjeta-seccion">

                <h5>Pacientes recientes</h5>

                <ul class="lista-simple">
                    <li><strong>Juan García</strong> - Control cardiológico</li>
                    <li><strong>María López</strong> - Revisión general</li>
                    <li><strong>Pedro Gómez</strong> - Dolor torácico</li>
                    <li><strong>Sofía Pérez</strong> - Seguimiento</li>
                </ul>
            </div>

            <!-- Ondicadores clinicos -->
            <div class="tarjeta-seccion">
                <h5>Indicadores clínicos</h5>
                <div class="mini-grid">
                    <p>Consultas hoy: <strong>8</strong></p>
                    <p>Cancelaciones: <strong>1</strong></p>
                    <p>No asistieron: <strong>2</strong></p>
                </div>
            </div>
        </div>

    </asp:Panel>


    <!-- PACIENTE -->
    <asp:Panel ID="pnlPaciente" runat="server" Visible="false">
        
        <!-- Credencial + info proximo turno -->
        <div class="grid-doble mb-4">
            <!-- Credenciales -->
            <div class="tarjeta-credencial">
                <h3>Juan García</h3>
                <hr />
                <p><strong>DNI:</strong> 30.111.222</p>
                <p><strong>Email:</strong> juan.garcia@gmail.com</p>
                <p><strong>Teléfono:</strong> 11-2334-5566</p>

                <p><strong>Obra Social:</strong> OSDE</p>
                <p><strong>Género:</strong> Masculino</p>
                <p><strong>Paciente desde:</strong> Junio 2024</p>
            </div>

            <!-- proximo turno-->
            <div class="tarjeta-destacada">
                <h4>Próximo turno</h4>
                <h2>18/06/2026</h2>
                <p>10:30 hs</p>
                <hr />
                <p><strong>Médico:</strong> Dra. María González</p>
                <p><strong>Especialidad:</strong> Cardiología</p>
                <p><strong>Estado:</strong> Confirmado</p>
            </div>
        </div>

        <!-- Acciones -->
        <div class="tarjeta-seccion mb-4">
            <h5>Acciones rápidas</h5>
            <div class="grupo-botones">
                <button class="btn-accion btn-verde">
                    Solicitar turno
                </button>
                <button class="btn-accion">
                    Ver mis turnos
                </button>
                <button class="btn-accion">
                    Historia clínica
                </button>
                <button class="btn-accion">
                    Mi perfil
                </button>
            </div>
        </div>

        <!-- Contenido inferior-->
        <div class="grid-doble">

            <!-- ÚLTIMOS TURNOS -->
            <div class="tarjeta-seccion">
                <h5>Últimos turnos</h5>

                <table class="tabla-simple">
                    <tr>
                        <th>Fecha</th>
                        <th>Médico</th>
                        <th>Estado</th>
                    </tr>

                    <tr>
                        <td>12/05/2026</td>
                        <td>Dr. Juan Pérez</td>
                        <td>Finalizado</td>
                    </tr>

                    <tr>
                        <td>03/05/2026</td>
                        <td>Dra. María González</td>
                        <td>Finalizado</td>
                    </tr>

                    <tr>
                        <td>15/04/2026</td>
                        <td>Dr. Carlos Díaz</td>
                        <td>Cancelado</td>
                    </tr>
                </table>
            </div>

            <!-- Resumen clínico -->
            <div class="tarjeta-seccion">

                <h5>Resumen clínico</h5>
                <p><strong>Consultas realizadas:</strong> 12</p>
                <p><strong>Último diagnóstico:</strong> Control general</p>
                <p><strong>Última atención:</strong> 12/05/2026</p>
                <hr />

                <p><strong>Indicaciones recientes:</strong></p>
                <ul>
                    <li>Control de presión arterial</li>
                    <li>Examen de laboratorio anual</li>
                    <li>Seguimiento cardiológico</li>
                </ul>

            </div>

        </div>

    </asp:Panel>

</asp:Content>
