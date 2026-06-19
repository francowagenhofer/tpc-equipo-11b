using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentación.Configuracion
{
    public partial class Generos : PaginaProtegida
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
            GeneroNegocio negocio = new GeneroNegocio();
            try
            {
                dgvGeneros.DataSource = negocio.ListarGeneros();
                dgvGeneros.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error al cargar géneros: " + ex.Message + "');</script>");
            }
        }


        protected void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            // Aquí agregaremos el filtro de búsqueda 
        }


        protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Aquí agregaremos el filtro por estado (Activo/Inactivo)
        }
    }
}