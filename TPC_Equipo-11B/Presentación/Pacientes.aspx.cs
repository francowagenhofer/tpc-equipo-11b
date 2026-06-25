using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentación {
    public partial class Pacientes : PaginaProtegida
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ValidarRoles("Administrador", "Recepcionista");

            if (!IsPostBack)
            {
                cargarGrilla();
                CargarObrasSociales();
            }
        }

        private void cargarGrilla()
        {
            try
            {
                PacienteNegocio negocio = new PacienteNegocio();

                List<Paciente> lista = negocio.ListarPacientes(false);

                Session["listaPacientes"] = lista;

                dgvPacientes.DataSource = lista;
                dgvPacientes.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error al cargar pacientes: " + ex.Message + "');</script>");
            }
        }

        private void CargarObrasSociales()
        {
            ObraSocialNegocio negocio = new ObraSocialNegocio();

            ddlObrasSociales.DataSource = negocio.ListarObrasSociales();
            ddlObrasSociales.DataTextField = "Nombre";
            ddlObrasSociales.DataValueField = "Id";
            ddlObrasSociales.DataBind();

            ddlObrasSociales.Items.Insert(0, new ListItem("Todas las obras sociales", "0"));
        }

        protected void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            AplicarFiltros();
        }

        protected void ddlObrasSociales_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFiltros();
        }

        protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFiltros();
        }
     
        private void AplicarFiltros()
        {
            List<Paciente> lista =
                (List<Paciente>)Session["listaPacientes"];

            string busqueda =
                txtBuscar.Text.Trim().ToLower();

            if (!string.IsNullOrEmpty(busqueda))
            {
                lista = lista.FindAll(x =>
                    x.Usuario.Nombre.ToLower().Contains(busqueda)
                    || x.Usuario.Apellido.ToLower().Contains(busqueda)
                    || x.DNI.ToLower().Contains(busqueda)
                    || x.Usuario.Email.ToLower().Contains(busqueda));
            }

            if (ddlObrasSociales.SelectedValue != "0")
            {
                int idObra = int.Parse(
                    ddlObrasSociales.SelectedValue);

                lista = lista.FindAll(x =>
                    x.ObraSocial != null &&
                    x.ObraSocial.Id == idObra);
            }

            switch (ddlEstado.SelectedValue)
            {
                case "1":
                    lista = lista.FindAll(x => x.Activo);
                    break;

                case "2":
                    lista = lista.FindAll(x => !x.Activo);
                    break;
            }

            dgvPacientes.DataSource = lista;
            dgvPacientes.DataBind();
        }
        
        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "";

            ddlObrasSociales.SelectedIndex = 0;
            ddlEstado.SelectedIndex = 0;

            cargarGrilla();
        }

        protected void dgvPacientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idPaciente = int.Parse(e.CommandArgument.ToString());

            switch (e.CommandName)
            {
                case "Editar":
                    Response.Redirect($"NuevoPaciente.aspx?id={idPaciente}");
                    break;

                case "Perfil":
                    Response.Redirect($"PerfilPaciente.aspx?id={idPaciente}");
                    break;

                case "Eliminar":
                    hfIdPaciente.Value = idPaciente.ToString();
                    hfAccion.Value = "Eliminar";
                    lblMensajeModal.Text = "¿Está seguro que desea desactivar este paciente?";
                    MostrarModalConfirmacion();
                    break;

                case "Reactivar":
                    hfIdPaciente.Value = idPaciente.ToString();
                    hfAccion.Value = "Reactivar";
                    lblMensajeModal.Text = "¿Está seguro que desea reactivar este paciente?";
                    MostrarModalConfirmacion();
                    break;

            }
        }

        protected void dgvPacientes_PageIndexChanging(object sender,GridViewPageEventArgs e)
        {
            dgvPacientes.PageIndex = e.NewPageIndex;
            AplicarFiltros();
        }

        private void MostrarModalConfirmacion()
        {
            if (hfAccion.Value == "Eliminar")
            {
                headerModal.Attributes["class"] = "modal-header bg-danger text-white";
                tituloModal.InnerText = "Desactivar Paciente";

                btnConfirmarAccion.Text = "Desactivar";
                btnConfirmarAccion.CssClass = "btn btn-danger";
            }
            else
            {
                headerModal.Attributes["class"] = "modal-header bg-success text-white";
                tituloModal.InnerText = "Reactivar Paciente";

                btnConfirmarAccion.Text = "Reactivar";
                btnConfirmarAccion.CssClass = "btn btn-success";
            }

            string script = @"window.addEventListener('load', function () {
                var elementoModal = document.getElementById('modalConfirmacion');
                if (elementoModal && window.bootstrap) {
                    bootstrap.Modal.getOrCreateInstance(elementoModal).show();
                }
            });";

            ClientScript.RegisterStartupScript(GetType(), "MostrarModal", script, true);
        }

        protected void btnConfirmarAccion_Click(object sender, EventArgs e)
        {
            try
            {
                int idPaciente = int.Parse(hfIdPaciente.Value);

                PacienteNegocio negocio = new PacienteNegocio();

                if (hfAccion.Value == "Eliminar")
                    negocio.EliminarPaciente(idPaciente);
                else
                    negocio.ReactivarPaciente(idPaciente);

                cargarGrilla();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
        }
    }
}
