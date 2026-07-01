using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentación
{
    public partial class PerfilPaciente : PaginaProtegida
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ValidarRoles("Administrador", "Recepcionista");

            if (!IsPostBack)
                CargarPerfil();
        }

        private void CargarPerfil()
        {
            if (Request.QueryString["id"] == null || !int.TryParse(Request.QueryString["id"], out int idPaciente))
            {
                MostrarError("No se especificó un paciente válido.");
                return;
            }

            try
            {
                PacienteNegocio pacienteNegocio = new PacienteNegocio();
                Paciente paciente = pacienteNegocio.ObtenerPacientePorId(idPaciente);

                if (paciente == null)
                {
                    MostrarError("El paciente solicitado no existe.");
                    return;
                }

                litNombreCompleto.Text = $"{paciente.Usuario.Apellido}, {paciente.Usuario.Nombre}";
                litDni.Text = paciente.DNI;
                litEmail.Text = paciente.Usuario.Email;
                litTelefono.Text = string.IsNullOrWhiteSpace(paciente.Usuario.Telefono) ? "-" : paciente.Usuario.Telefono;
                
                imgPerfil.ImageUrl = string.IsNullOrWhiteSpace(paciente.Usuario.ImagenUrl) ? "~/Assets/IMG/Perfil.jpg" : paciente.Usuario.ImagenUrl;

                litObraSocial.Text = paciente.ObraSocial?.Nombre ?? "-";
                litGenero.Text = paciente.Genero?.Descripcion ?? "-";
                litDireccion.Text = paciente.Direccion ?? "-";
                litFechaNacimiento.Text = paciente.FechaNacimiento.ToString("dd/MM/yyyy");
                litFechaAlta.Text = paciente.Usuario.FechaAlta.ToString("MMMM yyyy",
                    new System.Globalization.CultureInfo("es-AR"));

                badgeEstado.InnerText = paciente.Activo ? "Activo" : "Inactivo";
                badgeEstado.Attributes["class"] = paciente.Activo ? "badge bg-success" : "badge bg-danger";



                TurnoNegocio turnoNegocio = new TurnoNegocio();
                HistoriaClinicaNegocio historiaNegocio = new HistoriaClinicaNegocio();

                Turno proximo = turnoNegocio.ObtenerProximoTurnoPaciente(idPaciente);

                if (proximo == null)
                {
                    pnlProximoTurno.Visible = false;
                    lblSinTurno.Visible = true;
                }
                else
                {
                    litFechaTurno.Text = proximo.FechaHora.ToString("dd/MM/yyyy");
                    litHoraTurno.Text = proximo.FechaHora.ToString("HH:mm");

                    litMedico.Text = $"Dr. {proximo.Medico.Usuario.Apellido}, {proximo.Medico.Usuario.Nombre}";

                    litEspecialidad.Text = proximo.Especialidad?.Nombre ?? "-";
                    litEstadoTurno.Text = proximo.EstadoTurno.Nombre;
                }

                litHistorias.Text = historiaNegocio.CantidadHistoriasPaciente(idPaciente).ToString();
                litUltimaConsulta.Text = historiaNegocio.ObtenerFechaUltimaConsulta(idPaciente);
                litDiagnostico.Text = historiaNegocio.ObtenerUltimoDiagnostico(idPaciente);

                List<Turno> turnos = turnoNegocio.ListarTurnosPorPaciente(idPaciente)
                    .OrderByDescending(x => x.FechaHora)
                    .Take(20)
                    .ToList();

                if (turnos.Count == 0)
                {
                    rptTurnos.Visible = false;
                    lblSinHistorial.Visible = true;
                }
                else
                {
                    rptTurnos.DataSource = turnos;
                    rptTurnos.DataBind();
                }
            }
            catch (Exception ex)
            {
                MostrarError("Error al cargar el perfil del paciente: " + ex.Message);
            }
        }
        private void MostrarError(string mensaje)
        {
            lblError.Text = mensaje;
            lblError.Visible = true;
            pnlContenido.Visible = false;
        }
    }
}