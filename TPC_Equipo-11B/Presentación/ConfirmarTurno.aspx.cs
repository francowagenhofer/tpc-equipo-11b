using Dominio;
using Negocio;
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static Negocio.TurnoNegocio;

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
                    lblMensajeError.Text = "El enlace de confirmación no es válido.";
                }
            }
        }

        private void ProcesarConfirmacion(string codigo)
        {
            TurnoNegocio negocio = new TurnoNegocio();
            try
            {
                ResultadoConfirmacion resultado = negocio.ConfirmarTurnoPorCodigo(codigo);

                if (resultado.Exito)
                {
                    pnlExito.Visible = true;
                    Turno turno = resultado.Turno;

                    lblCodigo.Text = turno.Codigo;
                    lblPaciente.Text = $"{turno.Paciente.Usuario.Apellido}, {turno.Paciente.Usuario.Nombre}";
                    lblMedico.Text = $"Dr. {turno.Medico.Usuario.Apellido}, {turno.Medico.Usuario.Nombre}";
                    lblFechaHora.Text = turno.FechaHora.ToString("dd/MM/yyyy HH:mm") + " hs";
                }
                else
                {
                    pnlError.Visible = true;
                    lblMensajeError.Text = resultado.Mensaje;
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