using System;
using System.Collections.Generic;
using System.Linq;
using Dominio;
using Negocio;

namespace Presentación {
    public partial class PerfilMedico : PaginaProtegida {
        protected void Page_Load(object sender, EventArgs e)
        {
            ValidarRoles("Administrador", "Recepcionista");

            if (!IsPostBack)
            {
                CargarPerfil();
            }
        }

        private void CargarPerfil()
        {
            if (Request.QueryString["id"] == null || !int.TryParse(Request.QueryString["id"], out int idMedico))
            {
                MostrarError("No se especificó un médico válido.");
                return;
            }

            try
            {
                MedicoNegocio medicoNegocio = new MedicoNegocio();
                Medico medico = medicoNegocio.ObtenerMedicoPorId(idMedico);

                if (medico == null)
                {
                    MostrarError("El médico solicitado no existe.");
                    return;
                }

                // Datos personales
                litNombreCompleto.Text = $"{medico.Usuario.Apellido}, {medico.Usuario.Nombre}";
                litEspecialidad.Text = medico.Especialidad != null ? medico.Especialidad.Nombre : "Sin especialidad";
                litMatricula.Text = medico.Matricula;
                litEmail.Text = medico.Usuario.Email;
                litTelefono.Text = string.IsNullOrEmpty(medico.Usuario.Telefono) ? "-" : medico.Usuario.Telefono;

                badgeEstado.InnerText = medico.Activo ? "Activo" : "Inactivo";
                badgeEstado.Attributes["class"] = medico.Activo ? "badge bg-success" : "badge bg-danger";

                // Disponibilidad semanal
                DisponibilidadMedicoNegocio dispNegocio = new DisponibilidadMedicoNegocio();
                List<DisponibilidadMedico> disponibilidades = dispNegocio.ListarDisponibilidadesPorMedico(idMedico)
                    .Where(d => d.Activo)
                    .OrderBy(d => d.DiaSemana)
                    .ToList();

                if (disponibilidades.Count == 0)
                {
                    rptDisponibilidad.Visible = false;
                    lblSinDisponibilidad.Visible = true;
                    pnlAvisoSinDisponibilidad.Visible = true;
                }
                else
                {
                    rptDisponibilidad.DataSource = disponibilidades;
                    rptDisponibilidad.DataBind();
                    pnlAvisoSinDisponibilidad.Visible = false;
                }

                // Ausencias
                AusenciaMedicoNegocio ausenciaNegocio = new AusenciaMedicoNegocio();
                List<AusenciaMedico> ausencias = ausenciaNegocio.ListarAusenciasPorMedico(idMedico)
                    .Where(a => a.Fecha.Date >= DateTime.Today)
                    .OrderBy(a => a.Fecha)
                    .ToList();

                if (ausencias.Count == 0)
                {
                    rptAusencias.Visible = false;
                    lblSinAusencias.Visible = true;
                }
                else
                {
                    rptAusencias.DataSource = ausencias;
                    rptAusencias.DataBind();
                }

                // Próximos turnos (futuros, no cancelados)
                TurnoNegocio turnoNegocio = new TurnoNegocio();
                List<Turno> turnos = turnoNegocio.ListarTurnosPorMedico(idMedico)
                    .Where(t => t.FechaHora >= DateTime.Now
                             && t.EstadoTurno != null
                             && t.EstadoTurno.Nombre.ToLower() != "cancelado")
                    .OrderBy(t => t.FechaHora)
                    .Take(20)
                    .ToList();

                if (turnos.Count == 0)
                {
                    rptTurnos.Visible = false;
                    lblSinTurnos.Visible = true;
                }
                else
                {
                    rptTurnos.DataSource = turnos;
                    rptTurnos.DataBind();
                }
            }
            catch (Exception ex)
            {
                MostrarError("Error al cargar el perfil del médico: " + ex.Message);
            }
        }

        protected string ObtenerNombreDia(int diaSemana)
        {
            switch (diaSemana)
            {
                case 1: return "Lunes";
                case 2: return "Martes";
                case 3: return "Miércoles";
                case 4: return "Jueves";
                case 5: return "Viernes";
                case 6: return "Sábado";
                case 7: return "Domingo";
                default: return "";
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