using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Dominio;

namespace Presentación {
    public partial class NuevoTurno : PaginaProtegida 
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarPacientes();
                CargarMedicos();

                if (Request.QueryString["id"] != null) { 
                    int idTurno = Convert.ToInt32(Request.QueryString["id"]);
                    CargarTurno(idTurno);
                }
            }
        }

        private void CargarTurno(int idTurno) {

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

                    
                    txtFechaHora.Text = turno.FechaHora.ToString("yyyy-MM-ddTHH:mm");
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
            
            if (string.IsNullOrEmpty(ddlPaciente.SelectedValue) || string.IsNullOrEmpty(ddlMedico.SelectedValue))
            {
                lblMensaje.Text = "Debe seleccionar un Paciente y un Médico.";
                lblMensaje.CssClass = "alert alert-warning d-block text-center";
                lblMensaje.Visible = true;
                return;
            }
            try
            {
                Turno nuevo = new Turno();
                nuevo.PacienteId = Convert.ToInt32(ddlPaciente.SelectedValue);
                nuevo.MedicoId = Convert.ToInt32(ddlMedico.SelectedValue);
                nuevo.FechaHora = Convert.ToDateTime(txtFechaHora.Text);

                
                nuevo.Codigo = "TRN-" + DateTime.Now.ToString("yyyyMMdd-HHmmss");
                TurnoNegocio negocio = new TurnoNegocio();
                if (negocio.AgregarTurno(nuevo))
                {
                    
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