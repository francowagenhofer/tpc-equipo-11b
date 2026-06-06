using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace Presentación {
    public partial class NuevoPaciente : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {

                CargarObrasSociales();
                CargarGeneros();
            
            }


        }

        private void CargarObrasSociales() { 
        
            ObraSocialNegocio negocio = new ObraSocialNegocio();
            try
            {
                ddlObraSocial.DataSource = negocio.ListarObrasSociales();
                ddlObraSocial.DataTextField = "Nombre";
                ddlObraSocial.DataValueField = "Id";
                ddlObraSocial.DataBind();

                ddlObraSocial.Items.Insert(0, new ListItem("-- Seleccione Obra Social --", ""));
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar Obras Sociales: " + ex.Message;
                lblMensaje.CssClass = "alert alert-danger d-block text-center";
                lblMensaje.Visible = true;
            }

        }


        private void CargarGeneros() {

            GeneroNegocio negocio = new GeneroNegocio();
            try
            {
                ddlGenero.DataSource = negocio.ListarGeneros();
                ddlGenero.DataTextField = "Descripcion";
                ddlGenero.DataValueField = "Id";
                ddlGenero.DataBind();

                ddlGenero.Items.Insert(0, new ListItem("-- Seleccione Género --", ""));
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar géneros: " + ex.Message;
                lblMensaje.CssClass = "alert alert-danger d-block text-center";
                lblMensaje.Visible = true;
            }
        
        }


        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrEmpty(ddlObraSocial.SelectedValue) || string.IsNullOrEmpty(ddlGenero.SelectedValue))
            {
                lblMensaje.Text = "Debe seleccionar una Obra Social y un Género.";
                lblMensaje.CssClass = "alert alert-warning d-block text-center";
                lblMensaje.Visible = true;
                return;
            }
            try
            {
                Paciente nuevo = new Paciente();
                nuevo.DNI = txtDNI.Text.Trim();
                nuevo.FechaNacimiento = Convert.ToDateTime(txtFechaNacimiento.Text);
                nuevo.Direccion = txtDireccion.Text.Trim();
                nuevo.Activo = true;
               
                nuevo.ObraSocial = new ObraSocial();
                nuevo.ObraSocial.Id = Convert.ToInt32(ddlObraSocial.SelectedValue);
                nuevo.Genero = new Genero();
                nuevo.Genero.Id = Convert.ToInt32(ddlGenero.SelectedValue);
                
                nuevo.Usuario = new Usuario();
                nuevo.Usuario.Nombre = txtNombre.Text.Trim();
                nuevo.Usuario.Apellido = txtApellido.Text.Trim();
                nuevo.Usuario.Email = txtEmail.Text.Trim();
                nuevo.Usuario.Telefono = txtTelefono.Text.Trim();
                nuevo.Usuario.Username = txtDNI.Text.Trim();
                nuevo.Usuario.Password = txtDNI.Text.Trim(); // Contraseña temporal

                // Lo dejamos en 0 por ahora, la capa de Negocio se encargará de buscar el ID real
                nuevo.Usuario.RolId = 0;
                nuevo.Usuario.Activo = true;
                PacienteNegocio negocio = new PacienteNegocio();
                negocio.RegistrarPaciente(nuevo); 
                Response.Redirect("Pacientes.aspx", false);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "No se pudo registrar al paciente: " + ex.Message;
                lblMensaje.CssClass = "alert alert-danger d-block text-center";
                lblMensaje.Visible = true;
            }
        }

    }
}