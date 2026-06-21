using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Dominio;

namespace Presentación {
    public partial class NuevoTurno : System.Web.UI.Page {
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

            // Programar turnos cada 60 minutos (1 hora) de 08:00 a 19:00
            for (int hora = 8; hora <= 19; hora++)
            {
                string valor = hora.ToString("D2") + ":00";
                ddlHora.Items.Add(new ListItem(valor, valor));
            }
        }

        private void CargarTurno(int idTurno)
        {
            TurnoNegocio negocio = new TurnoNegocio();
            try
            {
                Turno turno = negocio.ObtnerTurnoPorId(idTurno);
                if (turno != null)
                {
                    litTitulo.Text = "Modificar Turno";
                    btnGuardar.Text = "Modificar Turno";
                    ddlPaciente.SelectedValue = turno.PacienteId.ToString();
                    ddlMedico.SelectedValue = turno.MedicoId.ToString();

                    // Asignar fecha y hora por separado
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

                // Combinar la fecha y hora seleccionada
                DateTime fecha = Convert.ToDateTime(txtFecha.Text);

                // 1. Validación del lado del servidor para Fines de Semana
                if (fecha.DayOfWeek == DayOfWeek.Saturday || fecha.DayOfWeek == DayOfWeek.Sunday)
                {
                    lblMensaje.Text = "No se permiten agendar turnos los fines de semana (sábados y domingos).";
                    lblMensaje.CssClass = "alert alert-warning d-block text-center";
                    lblMensaje.Visible = true;
                    return;
                }

                // 2. Validación del lado del servidor para Feriados Nacionales
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
                bool resultado = false;
                bool esNuevo = Request.QueryString["id"] == null;

                // Manejar si se está editando o creando un turno nuevo
                if (!esNuevo)
                {
                    nuevo.Id = Convert.ToInt32(Request.QueryString["id"]);
                    resultado = negocio.ModificarTurno(nuevo);
                }
                else
                {
                    nuevo.Codigo = "TRN-" + DateTime.Now.ToString("yyyyMMdd-HHmmss");
                    resultado = negocio.AgregarTurno(nuevo);
                }

                if (resultado)
                {
                    // Enviar correo de confirmación solo si es un turno NUEVO
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

                                // Generar URL de confirmación dinámica (funciona en localhost y en hosting de producción)
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

                                            <p style='font-size: 0.9em; color: #666;'>Si tú no solicitaste este turno, por favor ignora este correo.</p>
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
                            // Se registra el error en consola para depuración, pero no bloquea el flujo principal
                            System.Diagnostics.Debug.WriteLine("Error al enviar el correo: " + ex.Message);
                        }
                    }

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


    }
}
