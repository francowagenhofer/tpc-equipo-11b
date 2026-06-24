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
    public partial class Usuarios : PaginaProtegida
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarGrilla();
            }

        }
        private void CargarGrilla()
        {
            try
            {
                UsuarioNegocio negocio = new UsuarioNegocio();

                List<Usuario> lista = negocio.ListarUsuarios();

                Session["listaUsuarios"] = lista;

                dgvUsuarios.DataSource = lista;
                dgvUsuarios.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error al cargar usuarios: " + ex.Message + "');</script>");
            }
        }

        private void AplicarFiltros()
        {
            List<Usuario> lista =
                (List<Usuario>)Session["listaUsuarios"];

            string busqueda =
                txtBuscar.Text.Trim().ToLower();

            if (!string.IsNullOrEmpty(busqueda))
            {
                lista = lista.FindAll(x =>
                    x.Nombre.ToLower().Contains(busqueda)
                    || x.Apellido.ToLower().Contains(busqueda)
                    || x.Username.ToLower().Contains(busqueda)
                    || x.Email.ToLower().Contains(busqueda));
            }

            if (ddlRol.SelectedValue != "0")
            {
                int idRol = int.Parse(ddlRol.SelectedValue);

                lista = lista.FindAll(x =>
                    x.RolId == idRol);
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

            dgvUsuarios.DataSource = lista;
            dgvUsuarios.DataBind();
        }

        protected void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            AplicarFiltros();
        }

        protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFiltros();
        }

        protected void ddlRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFiltros();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "";

            ddlRol.SelectedIndex = 0;
            ddlEstado.SelectedIndex = 0;

            CargarGrilla();
        }

        protected void dgvUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idUsuario = int.Parse(e.CommandArgument.ToString());

            switch (e.CommandName)
            {
                case "Editar":
                    Response.Redirect($"NuevoUsuario.aspx?id={idUsuario}");
                    break;

                case "Eliminar":
                    hfIdUsuario.Value = idUsuario.ToString();
                    hfAccion.Value = "Eliminar";

                    lblMensajeModal.Text = "¿Está seguro que desea desactivar este usuario?";

                    MostrarModalConfirmacion();
                    break;

                case "Reactivar":
                    hfIdUsuario.Value = idUsuario.ToString();
                    hfAccion.Value = "Reactivar";

                    lblMensajeModal.Text = "¿Está seguro que desea reactivar este usuario?";

                    MostrarModalConfirmacion();
                    break;
            }
        }

        protected void dgvUsuarios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvUsuarios.PageIndex = e.NewPageIndex;
            AplicarFiltros();
        }

        private void MostrarModalConfirmacion()
        {
            if (hfAccion.Value == "Eliminar")
            {
                headerModal.Attributes["class"] = "modal-header bg-danger text-white";
                tituloModal.InnerText = "Desactivar Usuario";

                btnConfirmarAccion.Text = "Desactivar";
                btnConfirmarAccion.CssClass = "btn btn-danger";
            }
            else
            {
                headerModal.Attributes["class"] = "modal-header bg-success text-white";
                tituloModal.InnerText = "Reactivar Usuario";

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
                int idUsuario = int.Parse(hfIdUsuario.Value);

                UsuarioNegocio negocio = new UsuarioNegocio();

                if (hfAccion.Value == "Eliminar")
                    negocio.EliminarUsuario(idUsuario);
                else
                    negocio.ReactivarUsuario(idUsuario);

                CargarGrilla();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
        }

    }
}