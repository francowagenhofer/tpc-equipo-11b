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
    public partial class NuevoUsuario : PaginaProtegida
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    lblTitulo.Text = "Editar Usuario";
                    lblSubtitulo.Text = "Modifique la información del usuario.";
                    btnGuardar.Text = "Guardar Cambios";

                    pnlAltaCredenciales.Visible = false;
                    pnlCredencialesAlta.Visible = false;
                    pnlEdicionCredenciales.Visible = true;
                    pnlCambioPassword.Visible = false;

                    CargarUsuario(int.Parse(Request.QueryString["id"]));
                }
                else
                {
                    pnlAltaCredenciales.Visible = true;
                    pnlEdicionCredenciales.Visible = false;

                    chkCredencialesAutomaticas.Checked = true;
                    pnlCredencialesAlta.Visible = false;
                    pnlInfoCredenciales.Visible = true;
                }
            }

        }

        protected void chkCredencialesAutomaticas_CheckedChanged(object sender, EventArgs e)
        {
            pnlCredencialesAlta.Visible = !chkCredencialesAutomaticas.Checked;
            pnlInfoCredenciales.Visible = chkCredencialesAutomaticas.Checked;
        }

        protected void chkCambiarPassword_CheckedChanged(object sender, EventArgs e)
        {
            pnlCambioPassword.Visible = chkCambiarPassword.Checked;
        }


        private void CargarUsuario(int idUsuario)
        {
            UsuarioNegocio negocio = new UsuarioNegocio();
            Usuario usuario = negocio.ObtenerUsuarioPorId(idUsuario);

            if (usuario == null)
            {
                Response.Redirect("Usuarios.aspx");
                return;
            }

            ViewState["IdUsuario"] = usuario.Id;

            txtNombre.Text = usuario.Nombre;
            txtApellido.Text = usuario.Apellido;
            txtTelefono.Text = usuario.Telefono;
            txtEmail.Text = usuario.Email;

            txtUsernameEdicion.Text = usuario.Username;

            ddlRol.SelectedValue = usuario.RolId.ToString();

            if (!string.IsNullOrEmpty(usuario.ImagenUrl))
            {
                imgPreview.ImageUrl = usuario.ImagenUrl;
                ViewState["ImagenActual"] = usuario.ImagenUrl;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                bool esEdicion = ViewState["IdUsuario"] != null;

                if (!esEdicion)
                {
                    if (!chkCredencialesAutomaticas.Checked)
                    {
                        if (txtPassword.Text != txtConfirmarPassword.Text)
                            throw new Exception("Las contraseñas no coinciden.");
                    }
                }
                else
                {
                    if (chkCambiarPassword.Checked)
                    {
                        if (txtPasswordEdicion.Text != txtConfirmarPasswordEdicion.Text)
                            throw new Exception("Las contraseñas no coinciden.");
                    }
                }



                string imagenUrl = ViewState["ImagenActual"]?.ToString() ?? "";

                if (fuImagen.HasFile)
                {
                    string extension = System.IO.Path.GetExtension(fuImagen.FileName).ToLower();

                    if (extension != ".jpg" && extension != ".jpeg" && extension != ".png")
                    {
                        throw new Exception("Solo se permiten imágenes JPG o PNG.");
                    }

                    string nombreArchivo = Guid.NewGuid() + extension;

                    imagenUrl = "~/Assets/IMG/Usuarios/" + nombreArchivo;

                    fuImagen.SaveAs(Server.MapPath(imagenUrl));
                }

                Usuario usuario = new Usuario();

                if (esEdicion)
                    usuario.Id = (int)ViewState["IdUsuario"];

                usuario.Nombre = txtNombre.Text.Trim();
                usuario.Apellido = txtApellido.Text.Trim();
                usuario.Email = txtEmail.Text.Trim();
                usuario.Telefono = txtTelefono.Text.Trim();
                usuario.ImagenUrl = imagenUrl;
                usuario.RolId = int.Parse(ddlRol.SelectedValue);
                usuario.Activo = true;
                
                if (!string.IsNullOrEmpty(usuario.ImagenUrl))
                {
                    imgPreview.ImageUrl = usuario.ImagenUrl;
                    ViewState["ImagenActual"] = usuario.ImagenUrl;
                }

                UsuarioNegocio negocio = new UsuarioNegocio();

                if (esEdicion)
                {
                    usuario.Username = txtUsernameEdicion.Text.Trim();

                    negocio.ModificarUsuario(usuario);

                    if (chkCambiarPassword.Checked)
                    {
                        negocio.CambiarPassword(usuario.Id, txtPasswordEdicion.Text.Trim());
                    }
                }
                else
                {
                    if (chkCredencialesAutomaticas.Checked)
                    {
                        usuario.Username = txtEmail.Text.Trim().ToLower();
                        usuario.Password = DateTime.Now.ToString("yyyyMMdd");
                    }
                    else
                    {
                        usuario.Username = txtUsername.Text.Trim();
                        usuario.Password = txtPassword.Text.Trim();
                    }

                    negocio.RegistrarUsuario(usuario);
                }

                Response.Redirect("Usuarios.aspx", false);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "alert alert-danger d-block text-center";
                lblMensaje.Visible = true;
            }
        }
    }
}