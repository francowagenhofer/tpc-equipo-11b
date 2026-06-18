using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentación
{
    public partial class Usuarios : System.Web.UI.Page
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
            UsuarioNegocio negocio = new UsuarioNegocio();
            try
            {
                dgvUsuarios.DataSource = negocio.ListarUsuarios();
                dgvUsuarios.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error al cargar médicos: " + ex.Message + "');</script>");
            }
        }
        protected void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            // Aca agregaremos el filtro de búsqueda 
        }

        protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Aca agregaremos el filtro por estado (Activo/Inactivo)
        }
        protected void ddlRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Aca agregaremos el filtro por rol
        }

    }
}