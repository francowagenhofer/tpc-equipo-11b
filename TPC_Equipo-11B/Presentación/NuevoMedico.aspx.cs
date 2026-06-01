using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace Presentación {
    public partial class NuevoMedico : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                
                Medico nuevoMedico = new Medico();
                nuevoMedico.Matricula = txtMatricula.Text.Trim();
                nuevoMedico.Activo = true;
               
                nuevoMedico.Usuario = new Usuario();
                nuevoMedico.Usuario.Nombre = txtNombre.Text.Trim();
                nuevoMedico.Usuario.Apellido = txtApellido.Text.Trim();
                nuevoMedico.Usuario.Email = txtEmail.Text.Trim();
                nuevoMedico.Usuario.Telefono = txtTelefono.Text.Trim();
                nuevoMedico.Usuario.Username = txtUsername.Text.Trim();
                nuevoMedico.Usuario.Password = txtPassword.Text.Trim();
                nuevoMedico.Usuario.RolId = 3; 
                nuevoMedico.Usuario.Activo = true;
                MedicoNegocio negocio = new MedicoNegocio();
                
                if (negocio.ArgregarMedico(nuevoMedico))
                {

                    Response.Redirect("Medicos.aspx", false);
                }
                else
                {
                    lblMensaje.Text = "No se pudo registrar al médico. Intente con otro nombre de usuario.";
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