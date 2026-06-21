using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace Presentación {
    public partial class Login : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuarioLogueado"] != null)
            {
                Response.Redirect("Default.aspx");
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsuario.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                lblMensajeError.Text = "Por favor, completa todos los campos.";
                lblMensajeError.Visible = true;
                return;
            }

            UsuarioNegocio negocio = new UsuarioNegocio();

            try
            {
                Usuario usuario = negocio.ValidarLogin(txtUsuario.Text, txtPassword.Text);

                if (usuario != null)
                {
                    Session["usuarioLogueado"] = usuario;
                    Response.Redirect("Default.aspx", false);
                }
                else
                {
                    lblMensajeError.Text = "Usuario o contraseña Incorrectos.";
                    lblMensajeError.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblMensajeError.Text = "Ocurrió un error al intentar iniciar sesión. Por favor, intenta nuevamente más tarde. " + ex.Message;
                lblMensajeError.Visible = true;
            }
        }
    }
}
