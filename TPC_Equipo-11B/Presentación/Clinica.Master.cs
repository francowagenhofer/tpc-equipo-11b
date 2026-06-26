using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentación
{
    public partial class Clinica : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblTituloPagina.Text = ObtenerTituloPagina();
            }

            if (Session["usuarioLogueado"] != null)
            {
                Usuario usuario = (Usuario)Session["usuarioLogueado"];

                CargarUsuario(usuario);
                ConfigurarMenu(usuario);
            }
        }

        private void CargarUsuario(Usuario usuario)
        {
            lblNombreUsuario.Text = usuario.Nombre + " " + usuario.Apellido;
            lblRolUsuario.Text = usuario.Rol.Nombre;

            if (!string.IsNullOrWhiteSpace(usuario.ImagenUrl))
            {
                imgUsuario.ImageUrl = usuario.ImagenUrl;
            }
            else
            {
                imgUsuario.ImageUrl = "~/Assets/IMG/Perfil.jpg";
            }
        }

        private void ConfigurarMenu(Usuario usuario)
        {
            OcultarMenu();
            OcultarSecciones();

            liSeccionPrincipal.Visible = true;
            liInicio.Visible = true;
            liSeccionCuenta.Visible = true;
            liMiPerfil.Visible = true;

            switch (usuario.Rol.Nombre)
            {
                case "Administrador":

                    liSeccionGestion.Visible = true;
                    liSeccionAtencion.Visible = true;
                    liSeccionAdministracion.Visible = true;

                    liPacientes.Visible = true;
                    liMedicos.Visible = true;
                    liTurnos.Visible = true;

                    liHistoriaClinica.Visible = true;

                    liUsuarios.Visible = true;
                    liConfiguracion.Visible = true;

                    break;

                case "Recepcionista":

                    liSeccionGestion.Visible = true;

                    liPacientes.Visible = true;
                    liMedicos.Visible = true;
                    liTurnos.Visible = true;

                    break;

                case "Medico":

                    liSeccionAtencion.Visible = true;

                    liMiAgenda.Visible = true;
                    liSeccionGestion.Visible = true;
                    liMiDisponibilidad.Visible = true;
                    liHistoriaClinica.Visible = true;

                    break;

                case "Paciente":

                    liSeccionAtencion.Visible = true;

                    liMiAgenda.Visible = true;
                    liHistoriaClinica.Visible = true;

                    break;
            }
        }

        private void OcultarMenu()
        {
            liPacientes.Visible = false;
            liMedicos.Visible = false;
            liTurnos.Visible = false;

            liMiAgenda.Visible = false;
            liMiDisponibilidad.Visible = false;
            liHistoriaClinica.Visible = false;

            liUsuarios.Visible = false;
            liConfiguracion.Visible = false;

            liMiPerfil.Visible = false;
        }
        private void OcultarSecciones()
        {
            liSeccionPrincipal.Visible = false;
            liSeccionGestion.Visible = false;
            liSeccionAtencion.Visible = false;
            liSeccionAdministracion.Visible = false;
            liSeccionCuenta.Visible = false;
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();

            Response.Redirect("~/Login.aspx");
        }

        private string ObtenerTituloPagina()
        {
            string pagina = System.IO.Path.GetFileNameWithoutExtension(Request.Url.AbsolutePath);

            switch (pagina)
            {
                case "Default":
                    return "Inicio";

                case "Pacientes":
                    return "Pacientes";

                case "Medicos":
                    return "Médicos";

                case "Turnos":
                    return "Turnos";

                case "Usuarios":
                    return "Usuarios";

                case "Configuracion":
                    return "Configuración";

                case "Especialidades":
                    return "Especialidades";

                case "Roles":
                    return "Roles";

                case "ObrasSociales":
                    return "Obras Sociales";

                case "EstadosTurno":
                    return "Estados de Turno";

                case "Generos":
                    return "Generos";

                default:
                    return "Sistema Clínica";
            }
        }


    }
}