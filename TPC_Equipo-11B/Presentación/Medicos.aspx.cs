using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace Presentación
{
    public partial class Medicos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                CargarGrilla();
            }

        }

        private void CargarGrilla()
        {
            MedicoNegocio negocio = new MedicoNegocio();
            try
            {
                dgvMedicos.DataSource = negocio.ListarMedicos();
                dgvMedicos.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error al cargar médicos: " + ex.Message + "');</script>");
            }
        }
        protected void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            // Aquí agregaremos el filtro de búsqueda 
        }



        protected void ddlEspecialidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Aca agregaremos el filtro por especialidad
        }
        protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Aca agregaremos el filtro por rol
        }
    }
}