using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentación
{
    public partial class MiPerfil : PaginaProtegida
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ValidarRoles("Administrador", "Recepcionista", "Medico", "Paciente");

            if (!IsPostBack)
            {
                if (Session["Mensaje"] != null)
                {
                    lblMensaje.Visible = true;
                    lblMensaje.CssClass = "alert alert-success alert-dismissible fade show";
                    lblMensaje.Text = Session["Mensaje"].ToString();

                    Session.Remove("Mensaje");
                }

                CargarDatos();
            }
        }

        private void CargarDatos()
        {
            ConfigurarPantalla();

            EspecialidadNegocio negocioEspecialidad = new EspecialidadNegocio();
            GeneroNegocio negocioGenero = new GeneroNegocio();
            ObraSocialNegocio negocioOS = new ObraSocialNegocio();

            ddlEspecialidad.DataSource = negocioEspecialidad.ListarEspecialidades();
            ddlEspecialidad.DataValueField = "Id";
            ddlEspecialidad.DataTextField = "Nombre";
            ddlEspecialidad.DataBind();

            ddlGenero.DataSource = negocioGenero.ListarGeneros();
            ddlGenero.DataValueField = "Id";
            ddlGenero.DataTextField = "Descripcion";
            ddlGenero.DataBind();

            ddlObraSocial.DataSource = negocioOS.ListarObrasSociales();
            ddlObraSocial.DataValueField = "Id";
            ddlObraSocial.DataTextField = "Nombre";
            ddlObraSocial.DataBind();

            CargarDatosUsuario();

            if (UsuarioLogueado.Rol.Nombre == "Medico")
                CargarDatosMedico();

            if (UsuarioLogueado.Rol.Nombre == "Paciente")
                CargarDatosPaciente();
        }

        private void ConfigurarPantalla()
        {
            pnlMedico.Visible = UsuarioLogueado.Rol.Nombre == "Medico";
            pnlPaciente.Visible = UsuarioLogueado.Rol.Nombre == "Paciente";
        }

        private void CargarDatosUsuario()
        {
            Usuario usuario = UsuarioLogueado;

            lblNombreCompleto.Text = $"{usuario.Apellido}, {usuario.Nombre}";
            lblRol.Text = usuario.Rol.Nombre;
            lblUsername.Text = usuario.Username;
            txtNombre.Text = usuario.Nombre;
            txtApellido.Text = usuario.Apellido;
            txtEmail.Text = usuario.Email;
            txtTelefono.Text = usuario.Telefono;
            txtUsername.Text = usuario.Username;
            imgPerfil.ImageUrl = string.IsNullOrWhiteSpace(usuario.ImagenUrl) ? "~/Assets/IMG/Perfil.jpg" : usuario.ImagenUrl;
        }
        private void CargarDatosMedico()
        {
            if (UsuarioLogueado == null)
                throw new Exception("UsuarioLogueado = null");

            if (UsuarioLogueado.Medico == null)
                throw new Exception("Medico = null");

            if (UsuarioLogueado.Medico.Especialidad == null)
                throw new Exception("Especialidad = null");

            Medico medico = UsuarioLogueado.Medico;

            txtMatricula.Text = medico.Matricula;
            ddlEspecialidad.SelectedValue = medico.Especialidad.Id.ToString();
        }

        private void CargarDatosPaciente()
        {
            Paciente paciente = UsuarioLogueado.Paciente;

            txtDni.Text = paciente.DNI;
            txtFechaNacimiento.Text = paciente.FechaNacimiento.ToString("yyyy-MM-dd");
            ddlGenero.SelectedValue = paciente.Genero.Id.ToString();
            ddlObraSocial.SelectedValue = paciente.ObraSocial.Id.ToString();
            txtDireccion.Text = paciente.Direccion;
        }

        protected void chkCambiarPassword_CheckedChanged(object sender, EventArgs e)
        {
            pnlPassword.Visible = chkCambiarPassword.Checked;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                GuardarCambios();
                Session["Mensaje"] = "Perfil actualizado correctamente.";
                Response.Redirect("MiPerfil.aspx");
            }
            catch (Exception ex)
            {
                lblMensaje.Visible = true;
                lblMensaje.CssClass = "alert alert-danger d-block";
                lblMensaje.Text = ex.Message;
            }
        }

        private void GuardarCambios()
        {
            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();

            Usuario usuario = UsuarioLogueado;

            usuario.Nombre = txtNombre.Text.Trim();
            usuario.Apellido = txtApellido.Text.Trim();
            usuario.Email = txtEmail.Text.Trim();
            usuario.Username = txtUsername.Text.Trim();
            usuario.Telefono = txtTelefono.Text.Trim();

            if (fuImagen.HasFile)
            {
                string extension = Path.GetExtension(fuImagen.FileName).ToLower();

                if (extension != ".jpg" && extension != ".jpeg" && extension != ".png")
                    throw new Exception("Solo se permiten imágenes JPG o PNG.");

                if (fuImagen.PostedFile.ContentLength > 5 * 1024 * 1024)
                    throw new Exception("La imagen no puede superar los 5 MB.");

                string nombreArchivo = Guid.NewGuid().ToString() + extension;
                string rutaServidor = Server.MapPath("~/Uploads/Usuarios/");

                if (!Directory.Exists(rutaServidor))
                    Directory.CreateDirectory(rutaServidor);

                fuImagen.SaveAs(Path.Combine(rutaServidor, nombreArchivo));

                usuario.ImagenUrl = "~/Uploads/Usuarios/" + nombreArchivo;
            }

            if (!string.IsNullOrWhiteSpace(usuario.Telefono) && usuario.Telefono.Length > 20)
                throw new Exception("El teléfono no puede superar los 20 caracteres.");

            if (UsuarioLogueado.Rol.Nombre == "Medico")
            {
                MedicoNegocio medicoNegocio = new MedicoNegocio();
                Medico medico = UsuarioLogueado.Medico;

                medico.Usuario = usuario;
                medico.Matricula = txtMatricula.Text.Trim();
                medico.Especialidad.Id = int.Parse(ddlEspecialidad.SelectedValue);

                medicoNegocio.ModificarMedico(medico);
            }
            else if (UsuarioLogueado.Rol.Nombre == "Paciente")
            {
                if (!DateTime.TryParse(txtFechaNacimiento.Text, out DateTime fechaNacimiento))
                    throw new Exception("La fecha de nacimiento no es válida.");

                if (fechaNacimiento > DateTime.Today)
                    throw new Exception("La fecha de nacimiento no puede ser futura.");

                if (!txtDni.Text.All(char.IsDigit))
                    throw new Exception("El DNI solo puede contener números.");

                if (txtDni.Text.Length < 7 || txtDni.Text.Length > 8)
                    throw new Exception("El DNI ingresado no es válido.");

                PacienteNegocio pacienteNegocio = new PacienteNegocio();
                Paciente paciente = UsuarioLogueado.Paciente;

                paciente.Usuario = usuario;
                paciente.DNI = txtDni.Text.Trim();
                paciente.FechaNacimiento = fechaNacimiento;
                paciente.Genero.Id = int.Parse(ddlGenero.SelectedValue);
                paciente.ObraSocial.Id = int.Parse(ddlObraSocial.SelectedValue);
                paciente.Direccion = txtDireccion.Text.Trim();

                pacienteNegocio.ModificarPaciente(paciente);
            }
            else
            {
                usuarioNegocio.ModificarUsuario(usuario);
            }

            if (chkCambiarPassword.Checked)
            {
                if (string.IsNullOrWhiteSpace(txtPassword.Text))
                    throw new Exception("Ingrese la nueva contraseña.");

                if (txtPassword.Text != txtConfirmarPassword.Text)
                    throw new Exception("Las contraseñas no coinciden.");

                usuarioNegocio.CambiarPassword(usuario.Id, txtPassword.Text);
            }

            Session["usuarioLogueado"] = usuarioNegocio.ObtenerUsuarioPorId(usuario.Id);
        }
    }
}