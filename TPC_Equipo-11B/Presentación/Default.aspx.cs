using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace Presentación
{
    public partial class Default : PaginaProtegida
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MostrarDashboard();
            }
        }

        private void MostrarDashboard()
        {
            OcultarTodos();

            switch (UsuarioLogueado.Rol.Nombre)
            {
                case "Administrador":
                    pnlAdministrador.Visible = true;
                    CargarDBAdministrador();
                    break;

                case "Recepcionista":
                    pnlRecepcionista.Visible = true;
                    CargarDBRecepcionista();
                    break;

                case "Medico":
                    pnlMedico.Visible = true;
                    CargarDBMedico();
                    break;

                case "Paciente":
                    pnlPaciente.Visible = true;
                    CargarDBPaciente();
                    break;
            }
        }

        private void OcultarTodos()
        {
            pnlAdministrador.Visible = false;
            pnlRecepcionista.Visible = false;
            pnlMedico.Visible = false;
            pnlPaciente.Visible = false;
        }
        private void CargarDBAdministrador()
        {
            TurnoNegocio turnoNegocio = new TurnoNegocio();
            HistoriaClinicaNegocio historiaNegocio = new HistoriaClinicaNegocio();
            PacienteNegocio pacienteNegocio = new PacienteNegocio();
            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
            MedicoNegocio medicoNegocio = new MedicoNegocio();
            EspecialidadNegocio especialidadNegocio = new EspecialidadNegocio();
            ObraSocialNegocio obraSocialNegocio = new ObraSocialNegocio();

            lblAdminNombre.Text = UsuarioLogueado.Apellido + ", " + UsuarioLogueado.Nombre;
            lblAdminRol.Text = UsuarioLogueado.Rol.Nombre;
            lblAdminEmail.Text = UsuarioLogueado.Email;
            lblAdminTelefono.Text = UsuarioLogueado.Telefono;
            imgAdmin.ImageUrl = string.IsNullOrWhiteSpace(UsuarioLogueado.ImagenUrl) ? "~/Assets/IMG/Perfil.jpg" : UsuarioLogueado.ImagenUrl;

            lblFechaActual.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lblUltimoAcceso.Text = DateTime.Now.ToString("HH:mm");

            lblTotalUsuarios.Text = usuarioNegocio.CantidadUsuarios().ToString();
            lblTotalPacientes.Text = pacienteNegocio.CantidadPacientes().ToString();
            lblTotalMedicos.Text = medicoNegocio.CantidadMedicos().ToString();
            lblTurnosHoy.Text = turnoNegocio.CantidadTurnosHoy().ToString();

            lblTurnosPendientes.Text = turnoNegocio.CantidadPendientes().ToString();
            lblTurnosConfirmados.Text = turnoNegocio.CantidadConfirmados().ToString();
            lblTurnosCancelados.Text = turnoNegocio.CantidadCanceladosHoy().ToString();

            lblHistoriasClinicas.Text = historiaNegocio.CantidadHistorias().ToString();
            lblEspecialidadMasSolicitada.Text = especialidadNegocio.ObtenerEspecialidadMasSolicitada();
            lblObraSocialMasUtilizada.Text = obraSocialNegocio.ObtenerObraSocialMasUtilizada();
            lblUsuariosActivos.Text = usuarioNegocio.CantidadUsuariosActivos().ToString();
        }

        private void CargarDBRecepcionista()
        {
            TurnoNegocio turnoNegocio = new TurnoNegocio();
            PacienteNegocio pacienteNegocio = new PacienteNegocio();

            lblRecepNombre.Text = UsuarioLogueado.Apellido + ", " + UsuarioLogueado.Nombre;
            lblRecepRol.Text = UsuarioLogueado.Rol.Nombre;
            lblRecepEmail.Text = UsuarioLogueado.Email;
            lblRecepTelefono.Text = UsuarioLogueado.Telefono;
            imgRecepcionista.ImageUrl = string.IsNullOrWhiteSpace(UsuarioLogueado.ImagenUrl) ? "~/Assets/IMG/Perfil.jpg" : UsuarioLogueado.ImagenUrl;

            lblRecepTurnosHoy.Text = turnoNegocio.CantidadTurnosHoy().ToString();
            lblRecepActualizacion.Text = DateTime.Now.ToString("HH:mm");

            lblRecepKpiTurnosHoy.Text = turnoNegocio.CantidadTurnosHoy().ToString();
            lblRecepKpiConfirmados.Text = turnoNegocio.CantidadTurnosConfirmadosHoy().ToString();
            lblRecepKpiPendientes.Text = turnoNegocio.CantidadTurnosPendientesHoy().ToString();
            lblRecepKpiCancelados.Text = turnoNegocio.CantidadCanceladosHoy().ToString();

            lblRecepPacientesRegistrados.Text = pacienteNegocio.CantidadPacientes().ToString();
            lblRecepTurnosCreados.Text = turnoNegocio.CantidadTurnosCreadosHoy().ToString();
            lblRecepTurnosReprogramados.Text = turnoNegocio.CantidadTurnosReprogramadosHoy().ToString();
            lblRecepCancelaciones.Text = turnoNegocio.CantidadCanceladosHoy().ToString();

            gvTurnosRecepcion.DataSource = turnoNegocio.ListarTurnosHoy();
            gvTurnosRecepcion.DataBind();
        }

        private void CargarDBMedico()
        {
            TurnoNegocio turnoNegocio = new TurnoNegocio();

            lblMedicoNombre.Text = UsuarioLogueado.Apellido + ", " + UsuarioLogueado.Nombre;
            lblMedicoMatricula.Text = UsuarioLogueado.Medico.Matricula;
            lblMedicoEspecialidad.Text = UsuarioLogueado.Medico.Especialidad.Nombre;
            lblMedicoEmail.Text = UsuarioLogueado.Email;
            lblMedicoTelefono.Text = UsuarioLogueado.Telefono;
            imgMedico.ImageUrl = string.IsNullOrWhiteSpace(UsuarioLogueado.ImagenUrl) ? "~/Assets/IMG/Perfil.jpg" : UsuarioLogueado.ImagenUrl;

            Turno proximo = turnoNegocio.ObtenerProximoTurnoMedico(UsuarioLogueado.Medico.Id);

            if (proximo != null)
            {
                lblMedicoProximoPaciente.Text = proximo.Paciente.Usuario.Apellido + ", " + proximo.Paciente.Usuario.Nombre;
                lblMedicoProximaHora.Text = proximo.FechaHora.ToString("HH:mm");
                lblMedicoMotivo.Text = proximo.Especialidad.Nombre;
                lblMedicoEstadoTurno.Text = proximo.EstadoTurno.Nombre;
            }
            else
            {
                lblMedicoProximoPaciente.Text = "-";
                lblMedicoProximaHora.Text = "-";
                lblMedicoMotivo.Text = "-";
                lblMedicoEstadoTurno.Text = "Sin turnos";
            }

            lblMedicoKpiTurnosHoy.Text = turnoNegocio.CantidadTurnosHoyMedico(UsuarioLogueado.Medico.Id).ToString();
            lblMedicoKpiPendientes.Text = turnoNegocio.CantidadPendientesMedico(UsuarioLogueado.Medico.Id).ToString();
            lblMedicoKpiFinalizados.Text = turnoNegocio.CantidadFinalizadosMedico(UsuarioLogueado.Medico.Id).ToString();
            lblMedicoKpiCancelados.Text = turnoNegocio.CantidadCanceladosMedico(UsuarioLogueado.Medico.Id).ToString();

            lblMedicoConsultasHoy.Text = turnoNegocio.CantidadFinalizadosHoyMedico(UsuarioLogueado.Medico.Id).ToString();
            lblMedicoPacientesAtendidos.Text = turnoNegocio.CantidadPacientesAtendidos(UsuarioLogueado.Medico.Id).ToString();
            lblMedicoAusentes.Text = turnoNegocio.CantidadAusentesMedico(UsuarioLogueado.Medico.Id).ToString();
            lblMedicoPendientes.Text = turnoNegocio.CantidadPendientesMedico(UsuarioLogueado.Medico.Id).ToString();

            gvAgendaMedico.DataSource = turnoNegocio.ListarAgendaHoyMedico(UsuarioLogueado.Medico.Id);
            gvAgendaMedico.DataBind();
        }

        private void CargarDBPaciente()
        {
            TurnoNegocio turnoNegocio = new TurnoNegocio();
            HistoriaClinicaNegocio historiaClinicaNegocio = new HistoriaClinicaNegocio();

            lblPacienteNombre.Text = UsuarioLogueado.Apellido + ", " + UsuarioLogueado.Nombre;
            lblPacienteDni.Text = UsuarioLogueado.Paciente.DNI;
            lblPacienteEmail.Text = UsuarioLogueado.Email;
            lblPacienteTelefono.Text = UsuarioLogueado.Telefono;
            lblPacienteObraSocial.Text = UsuarioLogueado.Paciente.ObraSocial.Nombre;
            lblPacienteGenero.Text = UsuarioLogueado.Paciente.Genero.Descripcion;
            lblPacienteDesde.Text = UsuarioLogueado.FechaAlta.ToString("MMMM yyyy", new System.Globalization.CultureInfo("es-AR"));
            imgPaciente.ImageUrl = string.IsNullOrWhiteSpace(UsuarioLogueado.ImagenUrl) ? "~/Assets/IMG/Perfil.jpg" : UsuarioLogueado.ImagenUrl;

            Turno proximo = turnoNegocio.ObtenerProximoTurnoPaciente(UsuarioLogueado.Paciente.Id);

            if (proximo != null)
            {
                lblPacienteProximoTurno.Text = proximo.FechaHora.ToString("dd/MM/yyyy");
                lblPacienteHoraTurno.Text = proximo.FechaHora.ToString("HH:mm");
                lblPacienteMedico.Text = proximo.Medico.Usuario.Apellido + ", " + proximo.Medico.Usuario.Nombre;
                lblPacienteEspecialidad.Text = proximo.Especialidad.Nombre;
                lblPacienteEstadoTurno.Text = proximo.EstadoTurno.Nombre;
            }
            else
            {
                lblPacienteProximoTurno.Text = "-";
                lblPacienteHoraTurno.Text = "-";
                lblPacienteMedico.Text = "-";
                lblPacienteEspecialidad.Text = "-";
                lblPacienteEstadoTurno.Text = "Sin turnos";
            }

            lblPacienteKpiTurnos.Text = turnoNegocio.CantidadTurnosPaciente(UsuarioLogueado.Paciente.Id).ToString();
            lblPacienteKpiPendientes.Text = turnoNegocio.CantidadPendientesPaciente(UsuarioLogueado.Paciente.Id).ToString();
            lblPacienteKpiFinalizados.Text = turnoNegocio.CantidadFinalizadosPaciente(UsuarioLogueado.Paciente.Id).ToString();
            lblPacienteKpiHistorias.Text = historiaClinicaNegocio.CantidadHistoriasPaciente(UsuarioLogueado.Paciente.Id).ToString();

            lblPacienteConsultas.Text = historiaClinicaNegocio.CantidadHistoriasPaciente(UsuarioLogueado.Paciente.Id).ToString();
            lblPacienteUltimaConsulta.Text = historiaClinicaNegocio.ObtenerFechaUltimaConsulta(UsuarioLogueado.Paciente.Id);
            lblPacienteDiagnostico.Text = historiaClinicaNegocio.ObtenerUltimoDiagnostico(UsuarioLogueado.Paciente.Id);
            lblPacienteProximoControl.Text = turnoNegocio.ObtenerFechaProximoControl(UsuarioLogueado.Paciente.Id);

            gvUltimosTurnosPaciente.DataSource = turnoNegocio.ListarUltimosTurnosPaciente(UsuarioLogueado.Paciente.Id);
            gvUltimosTurnosPaciente.DataBind();
        }
    }
}