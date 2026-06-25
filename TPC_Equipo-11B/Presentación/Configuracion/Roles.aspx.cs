using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentación.Configuracion
{
    public partial class Roles : PaginaProtegida
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ValidarRoles("Administrador");

            if (!IsPostBack)
            {
                CargarGrilla();
            }
        }

        private void CargarGrilla()
        {
            RolNegocio negocio = new RolNegocio();
            try
            {
                dgvRoles.DataSource = negocio.ListarRoles();
                dgvRoles.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error al cargar roles: " + ex.Message + "');</script>");
            }
        }


        protected void txtBuscar_TextChanged(object sender, EventArgs e) {}


        protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e) {}
    }
}