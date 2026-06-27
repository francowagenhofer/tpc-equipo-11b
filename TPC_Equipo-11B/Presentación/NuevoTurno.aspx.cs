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
            if (string.IsNullOrEmpty(ddlPaciente.SelectedValue) || string.IsNullOrEmpty(ddlMedico.SelectedValue) || string.IsNullOrEmpty(txtFecha.Text) || string.IsNullOrEmpty(ddlHora.SelectedValue))
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

                
                if (fecha.DayOfWeek == DayOfWeek.Saturday || fecha.DayOfWeek == DayOfWeek.Sunday)
                {
                    lblMensaje.Text = "No se permiten agendar turnos los fines de semana (sábados y domingos).";
                    lblMensaje.CssClass = "alert alert-warning d-block text-center";
                    lblMensaje.Visible = true;
                    return;
                }

                
                List<string> feriados = new List<string> {
                    "01-01", "03-24", "04-02", "05-01", "05-25", "06-17", "06-20", "07-09", "08-17", "10-12", "11-20", "12-08", "12-25"
                };
                string mesDiaStr = fecha.ToString("MM-dd");
                if (feriados.Contains(mesDiaStr))
                {
                    lblMensaje.Text = "El día seleccionado es un feriado nacional y la clínica permanece cerrada.";
                    lblMensaje.CssClass = "alert alert-warning d-block text-center";
                    lblMensaje.Visible = true;
                    return;
                }

                TimeSpan hora = TimeSpan.Parse(ddlHora.SelectedValue);
                nuevo.FechaHora = fecha.Date.Add(hora);

                TurnoNegocio negocio = new TurnoNegocio();
                bool esNuevo = Request.QueryString["id"] == null;
                int idTurnoEnEdicion = esNuevo ? 0 : Convert.ToInt32(Request.QueryString["id"]);

                
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

                bool resultado = false;

                if (!esNuevo)
                {
                    nuevo.Id = idTurnoEnEdicion;
                    resultado = negocio.ModificarTurno(nuevo);
                }
                else
                {
                    nuevo.Codigo = "T" + DateTime.Now.ToString("mmssfff");
                    resultado = negocio.AgregarTurno(nuevo);
                }

                if (resultado)
                {
                    bool errorEnvioMail = false;

                    if (esNuevo)
                    {
                        try
                        {
                            PacienteNegocio pacienteNeg = new PacienteNegocio();
                            Paciente pac = pacienteNeg.ObtenerPacientePorId(nuevo.PacienteId);

                            if (pac != null && pac.Usuario != null && !string.IsNullOrEmpty(pac.Usuario.Email))
                            {
                                string nombrePaciente = $"{pac.Usuario.Nombre} {pac.Usuario.Apellido}";
                                string nombreMedico = ddlMedico.SelectedItem.Text;
                                string fechaHoraStr = nuevo.FechaHora.ToString("dd/MM/yyyy HH:mm") + " hs";

                                string urlBase = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath.TrimEnd('/');
                                string urlConfirmacion = $"{urlBase}/ConfirmarTurno.aspx?codigo={nuevo.Codigo}";

                                string asunto = "Confirmación de Turno Médico - " + nuevo.Codigo;
                                string cuerpo = $@"
                    <html>
                    <body style='font-family: Arial, sans-serif; color: #333; line-height: 1.6;'>
                        <div style='max-width: 600px; margin: 0 auto; padding: 20px; border: 1px solid #e0e0e0; border-radius: 8px;'>
                            <h2 style='color: #0d6efd; text-align: center;'>¡Hola, {nombrePaciente}!</h2>
                            <p>Hemos registrado una solicitud de turno médico en nuestro sistema. A continuación se detallan los datos del turno:</p>
                            
                            <div style='background-color: #f8f9fa; padding: 15px; border-radius: 6px; margin: 20px 0; border-left: 4px solid #0d6efd;'>
                                <p style='margin: 5px 0;'><strong>Código de Turno:</strong> {nuevo.Codigo}</p>
                                <p style='margin: 5px 0;'><strong>Médico:</strong> {nombreMedico}</p>
                                <p style='margin: 5px 0;'><strong>Fecha y Hora:</strong> {fechaHoraStr}</p>
                            </div>

                            <p style='text-align: center; margin: 30px 0;'>
                                <a href='{urlConfirmacion}' style='background-color: #198754; color: white; padding: 12px 25px; text-decoration: none; border-radius: 50px; font-weight: bold; display: inline-block; box-shadow: 0 4px 6px rgba(0,0,0,0.1);'>Confirmar mi Turno</a>
                            </p>

                            <p style='font-size: 0.9em; color: #666;'>Este enlace vence en 48 horas o al llegar la fecha del turno. Si tú no solicitaste este turno, por favor ignora este correo.</p>
                            <hr style='border: none; border-top: 1px solid #eee; margin: 20px 0;' />
                            <p style='font-size: 0.8em; color: #999; text-align: center;'>© 2026 Sistema Clínica. Todos los derechos reservados.</p>
                        </div>
                    </body>
                    </html>";

                                EmailService emailService = new EmailService();
                                emailService.EnviarCorreo(pac.Usuario.Email, asunto, cuerpo);
                            }
                        }
                        catch (Exception ex)
                        {
                            //System.Diagnostics.Debug.WriteLine("Error al enviar el correo: " + ex.Message);
                            //errorEnvioMail = true;
                            lblMensaje.Text = ex.ToString();
                            lblMensaje.CssClass = "alert alert-danger d-block text-center";
                            lblMensaje.Visible = true;
                            errorEnvioMail = true;
                        }
                    }

                    if (!errorEnvioMail)
                    {
                        Response.Redirect("Turnos.aspx", false);
                    }
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