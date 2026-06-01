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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["usuarioLogeado"] != null) {
                Response.Redirect("Default.aspx");
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {


            if (string.IsNullOrEmpty(TextBox1.Text) || string.IsNullOrEmpty(TextBox2.Text)) {

                lblMensajeError.Text = "Por favor, completa todos los campos.";
                lblMensajeError.Visible = true;
                return;

            }

            UsuarioNegocio negocio = new UsuarioNegocio();

            try
            {
                Usuario usuario = negocio.ValidarLogin(TextBox1.Text, TextBox2.Text);

                if (usuario != null) {

                    Session["usuarioLogueado"] = usuario;
                    Response.Redirect("Default.aspx", false);

                }
                else{

                    lblMensajeError.Text = "Usuario o contraseña Incorrectos.";
                    lblMensajeError.Visible = true;

                }

            }
            catch (Exception ex)
            {

                lblMensajeError.Text = "Ocurrió un error al intentar iniciar sesión. Por favor, intenta nuevamente más tarde." + ex.Message; // error en la base de datos
                lblMensajeError.Visible = true;
            }
        }


    }
}