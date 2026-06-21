using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Dominio;

namespace Presentación {
    public partial class ConfirmarTurno : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string codigo = Request.QueryString["codigo"];
                if (!string.IsNullOrEmpty(codigo))
                {
                    ProcesarConfirmacion(codigo);
                }
                else
                {
                    pnlError.Visible = true;
                    lblMensajeError.Text = "El enlace de confirmación no es válido o ha expirado.";
                }
            }
        }

        private void ProcesarConfirmacion(string codigo)
        {
            TurnoNegocio negocio = new TurnoNegocio();
            try
            {
                Turno turno = negocio.ObtenerTurnoPorCodigo(codigo);
                if (turno != null)
                {
                    if (turno.EstadoTurno != null && turno.EstadoTurno.Nombre.ToLower() == "confirmado")
                    {
                        pnlExito.Visible = true;
                        lblCodigo.Text = turno.Codigo;
                        lblPaciente.Text = $"{turno.Paciente.Usuario.Apellido}, {turno.Paciente.Usuario.Nombre}";
                        lblMedico.Text = $"Dr. {turno.Medico.Usuario.Apellido}, {turno.Medico.Usuario.Nombre}";
                        lblFechaHora.Text = turno.FechaHora.ToString("dd/MM/yyyy HH:mm") + " hs";
                        return; // ya estaba confirmado previamente
                    }

                    if (negocio.ConfirmarTurnoPorCodigo(codigo))
                    {
                        pnlExito.Visible = true;
                        lblCodigo.Text = turno.Codigo;
                        lblPaciente.Text = $"{turno.Paciente.Usuario.Apellido}, {turno.Paciente.Usuario.Nombre}";
                        lblMedico.Text = $"Dr. {turno.Medico.Usuario.Apellido}, {turno.Medico.Usuario.Nombre}";
                        lblFechaHora.Text = turno.FechaHora.ToString("dd/MM/yyyy HH:mm") + " hs";
                    }
                    else
                    {
                        pnlError.Visible = true;
                        lblMensajeError.Text = "No se pudo confirmar el turno. Intente nuevamente.";
                    }
                }
                else
                {
                    pnlError.Visible = true;
                    lblMensajeError.Text = "No se encontró ningún turno registrado con el código ingresado.";
                }
            }
            catch (Exception ex)
            {
                pnlError.Visible = true;
                lblMensajeError.Text = "Error al procesar la confirmación: " + ex.Message;
            }
        }
    }
}
