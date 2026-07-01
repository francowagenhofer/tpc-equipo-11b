using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentación
{
    public partial class AtenderTurno : PaginaProtegida
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ValidarRoles("Medico", "Administrador");

            if (!IsPostBack)
            {
                CargarDatosTurno();
            }
        }

        private void CargarDatosTurno()
        {
            if (!int.TryParse(Request.QueryString["id"], out int idTurno))
            {
                Response.Redirect("MiAgenda.aspx");
                return;
            }

            TurnoNegocio negocio = new TurnoNegocio();

            Turno turno = negocio.ObtenerTurnoPorId(idTurno);

            // Validar que el turno exista
            if (turno == null)
            {
                Response.Redirect("MiAgenda.aspx");
                return;
            }

            // Seguridad
            if (UsuarioLogueado.Rol.Nombre == "Medico" && turno.Medico.Id != UsuarioLogueado.Medico.Id)
            {
                Response.Redirect("MiAgenda.aspx");
                return;
            }

            // Solo pueden atenderse turnos pendientes o confirmados
            if (turno.EstadoTurno.Nombre != "Pendiente" && turno.EstadoTurno.Nombre != "Confirmado")
            {
                Response.Redirect("MiAgenda.aspx");
                return;
            }

            // El turno ya fue atendido
            if (turno.EstadoTurno.Nombre == "Finalizado")
            {
                Response.Redirect($"HistoriaClinica.aspx?idTurno={turno.Id}");
                return;
            }

            CargarInformacionTurno(turno);
        }

        private void CargarInformacionTurno(Turno turno)
        {
            // Datos del paciente
            lblPacienteResumen.Text = $"{turno.Paciente.Usuario.Apellido}, {turno.Paciente.Usuario.Nombre}";
            lblEspecialidadResumen.Text = turno.Especialidad.Nombre;
            lblEdad.Text = CalcularEdad(turno.Paciente.FechaNacimiento) + " años";
            lblGenero.Text = turno.Paciente.Genero.Descripcion;
            lblDni.Text = turno.Paciente.DNI;
            lblTelefono.Text = turno.Paciente.Usuario.Telefono;
            lblObraSocial.Text = turno.Paciente.ObraSocial?.Nombre ?? "-";

            // Datos del turno            
            lblCodigo.Text = turno.Codigo;
            lblTurnoResumen.Text = turno.FechaHora.ToString("dd/MM/yyyy HH:mm");
            lblEstado.Text = turno.EstadoTurno.Nombre;
        }

        private int CalcularEdad(DateTime fechaNacimiento)
        {
            int edad = DateTime.Today.Year - fechaNacimiento.Year;

            if (fechaNacimiento.Date > DateTime.Today.AddYears(-edad))
                edad--;
            return edad;
        }

        //private void CargarUltimasConsultas(Turno turno)
        //{
        //    HistoriaClinicaNegocio negocio = new HistoriaClinicaNegocio();

        //    repUltimasConsultas.DataSource = negocio.ListarUltimasHistoriasPaciente(turno.Paciente.Id, 3);

        //    repUltimasConsultas.DataBind();
        //}


        protected void btnFinalizar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validaciones
                if (string.IsNullOrWhiteSpace(txtDiagnostico.Text))
                {
                    return;
                }

                int idTurno = int.Parse(Request.QueryString["id"]);
                TurnoNegocio turnoNegocio = new TurnoNegocio();
                Turno turno = turnoNegocio.ObtenerTurnoPorId(idTurno);

                // Crear historia clínica
                HistoriaClinica historia = new HistoriaClinica();
                historia.Paciente = turno.Paciente;
                historia.Medico = turno.Medico;
                historia.Turno = turno;
                historia.Diagnostico = txtDiagnostico.Text.Trim();
                historia.Tratamiento = txtTratamiento.Text.Trim();
                historia.Observaciones = txtObservaciones.Text.Trim();

                // Guardar historia clínica
                HistoriaClinicaNegocio historiaNegocio = new HistoriaClinicaNegocio();
                historiaNegocio.AgregarHC(historia);

                // Cambiar estado del turno
                turnoNegocio.FinalizarTurno(turno.Id);

                // Volver a MiAgenda
                Response.Redirect("MiAgenda.aspx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("MiAgenda.aspx");
        }
    }
}