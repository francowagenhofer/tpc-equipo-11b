using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using Negocio;
using Dominio;

namespace Presentación {
    public partial class NuevoTurno : PaginaProtegida {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarPacientes();
                CargarMedicos();
                CargarHoras();

                if (Request.QueryString["id"] != null)
                {
                    int idTurno = Convert.ToInt32(Request.QueryString["id"]);
                    CargarTurno(idTurno);
                }
            }
        }

        private void CargarHoras()
        {
            ddlHora.Items.Clear();
            ddlHora.Items.Add(new ListItem("-- Seleccione una Hora --", ""));

            for (int hora = 8; hora <= 19; hora++)
            {
                string valor = hora.ToString("D2") + ":00";
                ddlHora.Items.Add(new ListItem(valor, valor));
            }
        }

        [WebMethod]
        public static List<string> ObtenerHorasOcupadasAjax(int idMedico, string fecha, int idTurnoActual)
        {
            List<string> resultado = new List<string>();

            if (idMedico <= 0 || string.IsNullOrEmpty(fecha))
                return resultado;

            DateTime fechaParseada;
            if (!DateTime.TryParse(fecha, out fechaParseada))
                return resultado;

            TurnoNegocio negocio = new TurnoNegocio();
            List<DateTime> ocupadas = negocio.ObtenerHorasOcupadas(idMedico, fechaParseada);

            foreach (DateTime dt in ocupadas)
            {
                if (idTurnoActual > 0)
                {
                    Turno turnoActual = negocio.ObtenerTurnoPorId(idTurnoActual);
                    if (turnoActual != null && turnoActual.FechaHora == dt)
                        continue;
                }
                resultado.Add(dt.ToString("HH:mm"));
            }

            // Horas fuera de disponibilidad o en día de ausencia del médico
            for (int hora = 8; hora <= 19; hora++)
            {
                DateTime horaCandidata = fechaParseada.Date.AddHours(hora);
                string horaStr = hora.ToString("D2") + ":00";

                if (!resultado.Contains(horaStr) && !negocio.MedicoDisponibleEnFechaHora(idMedico, horaCandidata))
                {
                    resultado.Add(horaStr);
                }
            }

            return resultado;
        }

        private void CargarTurno(int idTurno)
        {
            TurnoNegocio negocio = new TurnoNegocio();
            try
            {
                Turno turno = negocio.ObtenerTurnoPorId(idTurno);
                if (turno != null)
                {
                    litTitulo.Text = "Modificar Turno";
                    btnGuardar.Text = "Modificar Turno";
                    ddlPaciente.SelectedValue = turno.PacienteId.ToString();
                    ddlMedico.SelectedValue = turno.MedicoId.ToString();

                    txtFecha.Text = turno.FechaHora.ToString("yyyy-MM-dd");
                    ddlHora.SelectedValue = turno.FechaHora.ToString("HH:mm");
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar datos del turno: " + ex.Message;
                lblMensaje.CssClass = "alert alert-danger d-block text-center";
                lblMensaje.Visible = true;
            }
        }

        private void CargarPacientes()
        {
            PacienteNegocio negocio = new PacienteNegocio();
            try
            {
                var lista = negocio.ListarPacientes();
                ddlPaciente.Items.Clear();
                ddlPaciente.Items.Add(new ListItem("-- Seleccione un Paciente --", ""));
                foreach (var pac in lista)
                {
                    string texto = $"{pac.Usuario.Apellido}, {pac.Usuario.Nombre} (DNI: {pac.DNI})";
                    ddlPaciente.Items.Add(new ListItem(texto, pac.Id.ToString()));
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar pacientes: " + ex.Message;
                lblMensaje.CssClass = "alert alert-danger d-block text-center";
                lblMensaje.Visible = true;
            }
        }

        private void CargarMedicos()
        {
            MedicoNegocio negocio = new MedicoNegocio();
            try
            {
                var lista = negocio.ListarMedicos();
                ddlMedico.Items.Clear();
                ddlMedico.Items.Add(new ListItem("-- Seleccione un Médico --", ""));
                foreach (var med in lista)
                {
                    string texto = $"Dr. {med.Usuario.Apellido}, {med.Usuario.Nombre} (Mat: {med.Matricula})";
                    ddlMedico.Items.Add(new ListItem(texto, med.Id.ToString()));
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar médicos: " + ex.Message;
                lblMensaje.CssClass = "alert alert-danger d-block text-center";
                lblMensaje.Visible = true;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlPaciente.SelectedValue) ||
                string.IsNullOrEmpty(ddlMedico.SelectedValue) ||
                string.IsNullOrEmpty(txtFecha.Text) ||
                string.IsNullOrEmpty(ddlHora.SelectedValue))
            {
                lblMensaje.Text = "Debe completar todos los campos (Paciente, Médico, Fecha y Hora).";
                lblMensaje.CssClass = "alert alert-warning d-block text-center";
                lblMensaje.Visible = true;
                return;
            }

            try
            {
                Turno nuevo = new Turno();
                nuevo.PacienteId = Convert.ToInt32(ddlPaciente.SelectedValue);
                nuevo.MedicoId = Convert.ToInt32(ddlMedico.SelectedValue);

                DateTime fecha = Convert.ToDateTime(txtFecha.Text);
                TimeSpan hora = TimeSpan.Parse(ddlHora.SelectedValue);
                nuevo.FechaHora = fecha.Date.Add(hora);

                // 1. VALIDACIÓN: Evitar turnos en el pasado
                if (nuevo.FechaHora < DateTime.Now)
                {
                    lblMensaje.Text = "No se permiten agendar turnos en una fecha y hora que ya pasaron.";
                    lblMensaje.CssClass = "alert alert-warning d-block text-center";
                    lblMensaje.Visible = true;
                    return;
                }

                TurnoNegocio negocio = new TurnoNegocio();
                bool esNuevo = Request.QueryString["id"] == null;
                int idTurnoEnEdicion = esNuevo ? 0 : Convert.ToInt32(Request.QueryString["id"]);

                // 2. VALIDACIÓN: Superposición de turnos para el médico
                List<DateTime> ocupadas = negocio.ObtenerHorasOcupadas(nuevo.MedicoId, fecha);
                bool horarioOcupado = ocupadas.Any(h =>
                    h == nuevo.FechaHora && (esNuevo || h != ObtenerFechaHoraOriginal(idTurnoEnEdicion))
                );

                if (horarioOcupado)
                {
                    lblMensaje.Text = "Ese horario ya fue reservado para este médico. Por favor, elegí otro horario.";
                    lblMensaje.CssClass = "alert alert-warning d-block text-center";
                    lblMensaje.Visible = true;
                    CargarHoras();
                    return;
                }

                // 3. VALIDACIÓN: Superposición de turnos para el paciente
                if (!negocio.PacienteDisponibleEnFechaHora(nuevo.PacienteId, nuevo.FechaHora, idTurnoEnEdicion))
                {
                    lblMensaje.Text = "El paciente ya tiene un turno reservado para esa misma fecha y hora.";
                    lblMensaje.CssClass = "alert alert-warning d-block text-center";
                    lblMensaje.Visible = true;
                    return;
                }

                bool resultado = false;

                if (!esNuevo)
                {
                    nuevo.Id = idTurnoEnEdicion;
                    resultado = negocio.ModificarTurno(nuevo);
                }
                else
                {
                    nuevo.Codigo = "TRN-" + DateTime.Now.ToString("yyyyMMdd-HHmmss");
                    resultado = negocio.AgregarTurno(nuevo);
                }

                if (resultado)
                {
                    // CORREO TEMPORALMENTE DESACTIVADO
                    // Reactivar cuando se configuren las credenciales SMTP reales en Web.config
                    // (EmailEmisor / EmailPassword con contraseña de aplicación de Gmail)

                    Response.Redirect("Turnos.aspx", false);
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "No se pudo agendar el turno: " + ex.Message;
                lblMensaje.CssClass = "alert alert-danger d-block text-center";
                lblMensaje.Visible = true;
            }
        }

        private DateTime ObtenerFechaHoraOriginal(int idTurno)
        {
            if (idTurno <= 0) return DateTime.MinValue;
            TurnoNegocio negocio = new TurnoNegocio();
            Turno t = negocio.ObtenerTurnoPorId(idTurno);
            return t != null ? t.FechaHora : DateTime.MinValue;
        }
    }
}
