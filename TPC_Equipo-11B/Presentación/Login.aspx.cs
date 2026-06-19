using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentación
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuarioLogueado"] != null)
            {
                Response.Redirect("Default.aspx");
                return; 
            }

            if (!IsPostBack)
            {
                if (Request.QueryString["acceso"] == "denegado")
                {
                    pnlAvisoAcceso.Visible = true;
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            lblMensajeError.Visible = false;

            try
            {
                string usuario = txtUsername.Text.Trim();
                string password = txtPassword.Text;

                if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(password))
                {
                    lblMensajeError.Text = "Debe completar todos los campos.";
                    lblMensajeError.Visible = true;
                    return;
                }

                if (usuario.Length > 50)
                {
                    lblMensajeError.Text = "El usuario es demasiado largo.";
                    lblMensajeError.Visible = true;
                    return;
                }

                if (password.Length > 50)
                {
                    lblMensajeError.Text = "La contraseña es demasiado larga.";
                    lblMensajeError.Visible = true;
                    return;
                }
                else if (password.Length < 4)
                {
                    lblMensajeError.Text = "La contraseña es demasiado corta.";
                    lblMensajeError.Visible = true;
                    return;
                }

                UsuarioNegocio negocio = new UsuarioNegocio();
                Usuario usuarioLogueado = negocio.ValidarLogin(usuario, password);

                if (usuarioLogueado != null)
                {
                    Session["usuarioLogueado"] = usuarioLogueado;
                    Response.Redirect("Default.aspx");
                    return;
                }

                lblMensajeError.Text = "Usuario o contraseña incorrectos.";
                lblMensajeError.Visible = true;
            }
            catch
            {
                lblMensajeError.Text = "Ocurrió un error al iniciar sesión. Intente nuevamente.";
                lblMensajeError.Visible = true;
            }
        }

    }
}