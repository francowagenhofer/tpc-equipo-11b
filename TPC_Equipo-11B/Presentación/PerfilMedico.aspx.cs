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
    public partial class PerfilMedico : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            MedicoNegocio negocio = new MedicoNegocio();

            if (!IsPostBack)
            {
                int id = int.Parse(Request.QueryString["id"]);

                Medico medico = negocio.ObtenerMedicoPorId(id);

                lblNombreCompleto.Text = medico.Usuario.Nombre + " " + medico.Usuario.Apellido;

                lblNombre.Text = medico.Usuario.Nombre;
                lblApellido.Text = medico.Usuario.Apellido;
                lblEmail.Text = medico.Usuario.Email;
                lblTelefono.Text = medico.Usuario.Telefono;

                lblEspecialidad.Text = medico.Especialidad.Nombre;
                lblEspecialidadDetalle.Text = medico.Especialidad.Nombre;

                lblMatricula.Text = medico.Matricula;

                string estado = medico.Activo ? "Activo" : "Inactivo";

                lblEstado.Text = estado;
                lblEstadoDetalle.Text = estado;

                btnEditar.PostBackUrl = "~/NuevoMedico.aspx?id=" + medico.Id;
            }
        }
    }
}