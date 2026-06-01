using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace Presentación
{
    public partial class NuevoMedico : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarEspecialidades();
            }
        }

        private void CargarEspecialidades()
        {
            EspecialidadNegocio negocio = new EspecialidadNegocio();

            ddlEspecialidad.DataSource = negocio.Listar();
            ddlEspecialidad.DataTextField = "Nombre";
            ddlEspecialidad.DataValueField = "Id";
            ddlEspecialidad.DataBind();

            ddlEspecialidad.Items.Insert(0,
                new ListItem("Seleccione una especialidad", "0"));
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Medico nuevoMedico = new Medico
                {
                    Matricula = txtMatricula.Text.Trim(),
                    Activo = true,

                    Usuario = new Usuario
                    {
                        Nombre = txtNombre.Text.Trim(),
                        Apellido = txtApellido.Text.Trim(),
                        Email = txtEmail.Text.Trim(),
                        Telefono = txtTelefono.Text.Trim(),
                        Username = txtUsername.Text.Trim(),
                        Password = txtPassword.Text.Trim(),
                        RolId = 3,
                        Activo = true
                    }
                };

                nuevoMedico.Especialidad = new Especialidad();
                nuevoMedico.Especialidad.Id = int.Parse(ddlEspecialidad.SelectedValue);

                MedicoNegocio negocio = new MedicoNegocio();

                if (negocio.AgregarMedico(nuevoMedico))
                {
                    Response.Redirect("Medicos.aspx", false);
                }
                else
                {
                    lblMensaje.Text = "No se pudo registrar al médico.";
                    lblMensaje.CssClass = "alert alert-warning d-block text-center";
                    lblMensaje.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error: " + ex.Message;
                lblMensaje.CssClass = "alert alert-danger d-block text-center";
                lblMensaje.Visible = true;
            }
        }
    }
}