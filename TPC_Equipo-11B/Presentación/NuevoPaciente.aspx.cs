using Dominio;
using Negocio;
using System;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentación
{
    public partial class NuevoPaciente : PaginaProtegida
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarObrasSociales();
                CargarGeneros();

                if (Request.QueryString["id"] != null)
                {
                    lblTitulo.Text = "Editar Paciente";
                    lblSubtitulo.Text = "Modifique la información del paciente.";
                    btnGuardar.Text = "Guardar Cambios";

                    pnlAltaCredenciales.Visible = false;
                    pnlCredencialesAlta.Visible = false;
                    pnlEdicionCredenciales.Visible = true;
                    pnlCambioPassword.Visible = false;

                    CargarPaciente(int.Parse(Request.QueryString["id"]));
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

        private void CargarObrasSociales()
        {
            try
            {
                ObraSocialNegocio negocio = new ObraSocialNegocio();

                ddlObrasSociales.DataSource = negocio.ListarObrasSociales();
                ddlObrasSociales.DataTextField = "Nombre";
                ddlObrasSociales.DataValueField = "Id";
                ddlObrasSociales.DataBind();

                ddlObrasSociales.Items.Insert(0, new ListItem("Seleccione Obra Social", "")
                );
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar obras sociales: " + ex.Message;
                lblMensaje.CssClass = "alert alert-danger d-block text-center";
                lblMensaje.Visible = true;
            }
        }

        private void CargarGeneros()
        {
            try
            {
                GeneroNegocio negocio = new GeneroNegocio();

                ddlGenero.DataSource = negocio.ListarGeneros();
                ddlGenero.DataTextField = "Descripcion";
                ddlGenero.DataValueField = "Id";
                ddlGenero.DataBind();

                ddlGenero.Items.Insert(0, new ListItem("Seleccione Género", "")
                );
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar géneros: " + ex.Message;
                lblMensaje.CssClass = "alert alert-danger d-block text-center";
                lblMensaje.Visible = true;
            }
        }

        private void CargarPaciente(int idPaciente)
        {
            PacienteNegocio negocio = new PacienteNegocio();
            Paciente paciente = negocio.ObtenerPacientePorId(idPaciente);

            if (paciente == null)
            {
                Response.Redirect("Pacientes.aspx");
                return;
            }

            ViewState["IdPaciente"] = paciente.Id;
            ViewState["IdUsuario"] = paciente.Usuario.Id;

            txtNombre.Text = paciente.Usuario.Nombre;
            txtApellido.Text = paciente.Usuario.Apellido;
            txtDNI.Text = paciente.DNI;

            txtTelefono.Text = paciente.Usuario.Telefono;
            txtEmail.Text = paciente.Usuario.Email;
            txtDireccion.Text = paciente.Direccion;
            txtUsernameEdicion.Text = paciente.Usuario.Username;
            txtFechaNacimiento.Text = paciente.FechaNacimiento.ToString("yyyy-MM-dd");
            txtUsernameEdicion.Text = paciente.Usuario.Username;

            chkCambiarPassword.Checked = false;
            pnlCambioPassword.Visible = false;

            if (paciente.ObraSocial != null)
                ddlObrasSociales.SelectedValue = paciente.ObraSocial.Id.ToString();

            if (paciente.Genero != null)
                ddlGenero.SelectedValue = paciente.Genero.Id.ToString();

            if (!string.IsNullOrEmpty(paciente.Usuario.ImagenUrl))
            {
                imgPreview.ImageUrl = paciente.Usuario.ImagenUrl;
                imgPreview.Visible = true;

                ViewState["ImagenActual"] = paciente.Usuario.ImagenUrl;
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

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                bool esEdicion = ViewState["IdPaciente"] != null;

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
                    string extension = Path.GetExtension(fuImagen.FileName).ToLower();

                    if (extension != ".jpg" && extension != ".jpeg" && extension != ".png")
                    {
                        throw new Exception("Solo se permiten imágenes JPG o PNG.");
                    }

                    string carpeta = Server.MapPath("~/Uploads/");

                    if (!Directory.Exists(carpeta))
                        Directory.CreateDirectory(carpeta);

                    string nombreArchivo = Guid.NewGuid() + extension;

                    fuImagen.SaveAs(Path.Combine(carpeta, nombreArchivo));
                    imagenUrl = "~/Uploads/" + nombreArchivo;
                }

                Paciente paciente = new Paciente();

                paciente.DNI = txtDNI.Text.Trim();
                paciente.FechaNacimiento = Convert.ToDateTime(txtFechaNacimiento.Text);
                paciente.Direccion = txtDireccion.Text.Trim();
                paciente.Activo = true;


                paciente.ObraSocial = new ObraSocial
                {
                    Id = Convert.ToInt32(ddlObrasSociales.SelectedValue)
                };

                paciente.Genero = new Genero
                {
                    Id = Convert.ToInt32(ddlGenero.SelectedValue)
                };

                paciente.Usuario = new Usuario
                {
                    Nombre = txtNombre.Text.Trim(),
                    Apellido = txtApellido.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Telefono = txtTelefono.Text.Trim(),
                    ImagenUrl = imagenUrl,
                    Activo = true
                };

                PacienteNegocio negocio = new PacienteNegocio();

                if (esEdicion)
                {
                    paciente.Id = (int)ViewState["IdPaciente"];
                    paciente.UsuarioId = (int)ViewState["IdUsuario"];

                    paciente.Usuario.Id = paciente.UsuarioId;
                    paciente.Usuario.Username = txtUsernameEdicion.Text.Trim();

                    negocio.ModificarPaciente(paciente);

                    if (chkCambiarPassword.Checked)
                    {
                        UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
                        usuarioNegocio.CambiarPassword(paciente.Usuario.Id, txtPasswordEdicion.Text.Trim());
                    }
                }
                else
                {
                    if (chkCredencialesAutomaticas.Checked)
                    {
                        paciente.Usuario.Username = txtEmail.Text.Trim().ToLower();
                        paciente.Usuario.Password = txtDNI.Text.Trim();
                        //paciente.Usuario.Password = DateTime.Now.ToString("yyyyMMdd");
                    }
                    else
                    {
                        paciente.Usuario.Username = txtUsername.Text.Trim();
                        paciente.Usuario.Password = txtPassword.Text.Trim();
                    }

                    negocio.RegistrarPaciente(paciente);
                }

                Response.Redirect("Pacientes.aspx", false);
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