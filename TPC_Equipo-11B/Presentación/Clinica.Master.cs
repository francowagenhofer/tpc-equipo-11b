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

                lblNombreUsuario.Text =
                    usuario.Nombre + " " + usuario.Apellido;

                lblRolUsuario.Text =
                    usuario.Rol.Nombre;

                if (!string.IsNullOrWhiteSpace(usuario.ImagenUrl))
                {
                    imgUsuario.ImageUrl = usuario.ImagenUrl;
                }
                else
                {
                    imgUsuario.ImageUrl =
                        "~/Assets/IMG/usuario-default.png";
                }
            }
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