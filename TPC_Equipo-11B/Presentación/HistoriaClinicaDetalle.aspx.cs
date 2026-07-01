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
    public partial class HistoriaClinicaDetalle : PaginaProtegida
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ValidarRoles("Administrador", "Medico", "Paciente");

            if (!IsPostBack)
            {
                CargarHistoriaClinica();
            }
        }

        private void CargarHistoriaClinica()
        {
            if (!int.TryParse(Request.QueryString["id"], out int idHistoria))
            {
                Response.Redirect("HistorialClinico.aspx");
                return;
            }

            HistoriaClinicaNegocio negocio = new HistoriaClinicaNegocio();

            HistoriaClinica historia = negocio.ObtenerHCPorId(idHistoria);

            if (historia == null)
            {
                Response.Redirect("HistorialClinico.aspx");
                return;
            }

            // Seguridad
            ValidarPermisos(historia);

            // Datos del turno
            lblCodigo.Text = historia.Turno.Codigo;
            lblFecha.Text = historia.Fecha.ToString("dd/MM/yyyy");
            lblHora.Text = historia.Fecha.ToString("HH:mm");
            lblEspecialidad.Text = historia.Medico.Especialidad.Nombre;

            // Paciente
            lblPaciente.Text = $"{historia.Paciente.Usuario.Apellido}, {historia.Paciente.Usuario.Nombre}";
            lblDni.Text = historia.Paciente.DNI;
            lblObraSocial.Text = historia.Paciente.ObraSocial.Nombre;
            lblGenero.Text = historia.Paciente.Genero.Descripcion;

            // Médico
            lblMedico.Text = $"Dr. {historia.Medico.Usuario.Apellido}, {historia.Medico.Usuario.Nombre}";
            lblMatricula.Text = historia.Medico.Matricula;
            lblMatriculaFirma.Text = historia.Medico.Matricula;

            // Historia Clínica
            lblIdHistoria.Text = $"HC-{DateTime.Now.Year}-{historia.Id:D5}";
            lblDiagnostico.Text = historia.Diagnostico;
            lblTratamiento.Text = historia.Tratamiento;
            lblObservaciones.Text = historia.Observaciones;
        }

        private void ValidarPermisos(HistoriaClinica historia)
        {
            if (UsuarioLogueado.Rol.Nombre == "Administrador")
                return;

            if (UsuarioLogueado.Rol.Nombre == "Medico")
            {
                if (historia.Medico.Id != UsuarioLogueado.Medico.Id)
                    Response.Redirect("HistorialClinico.aspx");

                return;
            }

            if (UsuarioLogueado.Rol.Nombre == "Paciente")
            {
                if (historia.Paciente.Id != UsuarioLogueado.Paciente.Id)
                    Response.Redirect("HistorialClinico.aspx");
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("HistorialClinico.aspx");
        }
    }
}