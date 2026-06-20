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
    public partial class NuevoMedico : PaginaProtegida
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarEspecialidades();

                if (Request.QueryString["id"] != null)
                {
                    lblTitulo.Text = "Editar Médico";
                    lblSubtitulo.Text = "Modifique la información del profesional.";
                    btnGuardar.Text = "Guardar Cambios";

                    CargarMedico(int.Parse(Request.QueryString["id"]));
                }
            }
        }

        private void CargarEspecialidades()
        {
            EspecialidadNegocio negocio = new EspecialidadNegocio();

            ddlEspecialidad.DataSource = negocio.ListarEspecialidades();
            ddlEspecialidad.DataTextField = "Nombre";
            ddlEspecialidad.DataValueField = "Id";
            ddlEspecialidad.DataBind();

            ddlEspecialidad.Items.Insert(0, new ListItem("Seleccione una especialidad", "0"));
        }

        private void CargarMedico(int idMedico)
        {
            MedicoNegocio negocio = new MedicoNegocio();

            Medico medico = negocio.ObtenerMedicoPorId(idMedico);

            if (medico == null)
            {
                Response.Redirect("Medicos.aspx");
                return;
            }

            ViewState["IdMedico"] = medico.Id;
            ViewState["IdUsuario"] = medico.Usuario.Id;

            txtNombre.Text = medico.Usuario.Nombre;
            txtApellido.Text = medico.Usuario.Apellido;
            txtMatricula.Text = medico.Matricula;
            txtTelefono.Text = medico.Usuario.Telefono;
            txtEmail.Text = medico.Usuario.Email;
            txtUsername.Text = medico.Usuario.Username;

            ddlEspecialidad.SelectedValue = medico.Especialidad.Id.ToString();

            if (!string.IsNullOrEmpty(medico.Usuario.ImagenUrl))
            {
                imgPreview.ImageUrl = medico.Usuario.ImagenUrl;
                imgPreview.Visible = true;
                ViewState["ImagenActual"] = medico.Usuario.ImagenUrl;
            }
        }


        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                bool esEdicion = ViewState["IdMedico"] != null;

                if (!esEdicion)
                {
                    if (string.IsNullOrWhiteSpace(txtPassword.Text))
                        throw new Exception("Debe ingresar una contraseña.");

                    if (txtPassword.Text != txtConfirmarPassword.Text)
                        throw new Exception("Las contraseñas no coinciden.");
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(txtPassword.Text) && txtPassword.Text != txtConfirmarPassword.Text)
                    {
                        throw new Exception("Las contraseñas no coinciden.");
                    }
                }

                string rutaImagen = ViewState["ImagenActual"]?.ToString() ?? "";

                if (fuImagen.HasFile)
                {
                    string extension = System.IO.Path.GetExtension(fuImagen.FileName).ToLower();

                    if (extension != ".jpg" && extension != ".jpeg"  && extension != ".png")
                    {
                        throw new Exception("Solo se permiten imágenes JPG o PNG.");
                    }

                    string nombreArchivo = Guid.NewGuid().ToString() + extension;
                    rutaImagen = "~/Assets/IMG/Usuarios/" + nombreArchivo;
                    fuImagen.SaveAs(Server.MapPath(rutaImagen));
                }

                Medico medico = new Medico();

                if (esEdicion)
                {
                    medico.Id = (int)ViewState["IdMedico"];
                }

                medico.Matricula = txtMatricula.Text.Trim();
                medico.Activo = true;

                medico.Especialidad = new Especialidad
                {
                    Id = int.Parse(ddlEspecialidad.SelectedValue)
                };

                medico.Usuario = new Usuario();

                if (esEdicion)
                {
                    medico.Usuario.Id = (int)ViewState["IdUsuario"];
                }

                medico.Usuario.Nombre = txtNombre.Text.Trim();
                medico.Usuario.Apellido = txtApellido.Text.Trim();
                medico.Usuario.Email = txtEmail.Text.Trim();
                medico.Usuario.Telefono = txtTelefono.Text.Trim();
                medico.Usuario.Username = txtUsername.Text.Trim();
                medico.Usuario.ImagenUrl = rutaImagen;
                medico.Usuario.RolId = 3;
                medico.Usuario.Activo = true;

                if (!string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    medico.Usuario.Password = txtPassword.Text.Trim();
                }

                MedicoNegocio negocio = new MedicoNegocio();

                if (esEdicion)
                {
                    negocio.ModificarMedico(medico);
                }
                else
                {
                    negocio.RegistrarMedico(medico);
                }

                Response.Redirect("Medicos.aspx", false);
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