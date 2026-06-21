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
    public partial class Medicos : PaginaProtegida
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarEspecialidades();
                CargarGrilla();
            }

        }

        private void CargarGrilla()
        {
            MedicoNegocio negocio = new MedicoNegocio();
            try
            {
                List<Medico> lista = negocio.ListarMedicos();

                Session["listaMedicos"] = lista;
                dgvMedicos.DataSource = lista;
                dgvMedicos.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error al cargar médicos: " + ex.Message + "');</script>");
            }
        }

        private void CargarEspecialidades()
        {
            EspecialidadNegocio negocio = new EspecialidadNegocio();

            ddlEspecialidad.DataSource = negocio.ListarEspecialidades();
            ddlEspecialidad.DataTextField = "Nombre";
            ddlEspecialidad.DataValueField = "Id";
            ddlEspecialidad.DataBind();

            ddlEspecialidad.Items.Insert(0, new ListItem("Todas las especialidades", "0"));
        }

        protected void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            AplicarFiltros();
        }

        protected void ddlEspecialidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFiltros();
        }

        protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFiltros();
        }

        private void AplicarFiltros()
        {
            List<Medico> lista = (List<Medico>)Session["listaMedicos"];

            string busqueda = txtBuscar.Text.Trim().ToLower();

            if (!string.IsNullOrEmpty(busqueda))
            {
                lista = lista.FindAll(x =>
                    x.Usuario.Nombre.ToLower().Contains(busqueda)
                    || x.Usuario.Apellido.ToLower().Contains(busqueda)
                    || x.Matricula.ToLower().Contains(busqueda)
                    || x.Usuario.Email.ToLower().Contains(busqueda)
                    || x.Especialidad.Nombre.ToLower().Contains(busqueda));
            }

            if (ddlEspecialidad.SelectedValue != "0")
            {
                int idEspecialidad = int.Parse(ddlEspecialidad.SelectedValue);
                lista = lista.FindAll(x => x.Especialidad.Id == idEspecialidad);
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

            dgvMedicos.DataSource = lista;
            dgvMedicos.DataBind();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "";

            ddlEspecialidad.SelectedIndex = 0;
            ddlEstado.SelectedIndex = 0;

            CargarGrilla();
        }

        protected void dgvMedicos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idMedico = int.Parse(e.CommandArgument.ToString());

            switch (e.CommandName)
            {
                case "Editar":
                    Response.Redirect($"NuevoMedico.aspx?id={idMedico}");
                    break;

                case "Perfil":
                    Response.Redirect($"PerfilMedico.aspx?id={idMedico}");
                    break;

                case "Eliminar":
                    hfIdMedico.Value = idMedico.ToString();
                    hfAccion.Value = "Eliminar";
                    lblMensajeModal.Text = "¿Está seguro que desea desactivar este médico?";
                    MostrarModalConfirmacion();
                    break;

                case "Reactivar":
                    hfIdMedico.Value = idMedico.ToString();
                    hfAccion.Value = "Reactivar";
                    lblMensajeModal.Text = "¿Está seguro que desea reactivar este médico?";
                    MostrarModalConfirmacion();
                    break;

            }
        }

        protected void dgvMedicos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvMedicos.PageIndex = e.NewPageIndex;
            AplicarFiltros();
        }

        private void MostrarModalConfirmacion()
        {
            if (hfAccion.Value == "Eliminar")
            {
                headerModal.Attributes["class"] = "modal-header bg-danger text-white";
                tituloModal.InnerText = "Desactivar Médico";

                btnConfirmarAccion.Text = "Desactivar";
                btnConfirmarAccion.CssClass = "btn btn-danger";
            }
            else
            {
                headerModal.Attributes["class"] = "modal-header bg-success text-white";
                tituloModal.InnerText = "Reactivar Médico";

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
                int idMedico = int.Parse(hfIdMedico.Value);

                MedicoNegocio negocio = new MedicoNegocio();

                if (hfAccion.Value == "Eliminar")
                    negocio.EliminarMedico(idMedico);
                else
                    negocio.ReactivarMedico(idMedico);

                CargarGrilla();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
        }

    }
}