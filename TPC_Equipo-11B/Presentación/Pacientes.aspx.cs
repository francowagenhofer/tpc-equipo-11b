using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace Presentación
{
    public partial class Pacientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                cargarGrilla();
            }

        }


        private void cargarGrilla() {

            PacienteNegocio negocio = new PacienteNegocio();
            try
            {
                dgvPacientes.DataSource = negocio.ListarPacientes();
                dgvPacientes.DataBind();
            }
            catch (Exception ex)
            {

                Response.Write("<script>alert('Error al cargar paciente: " + ex.Message + "');</script>");
            }
        
        
        }


        protected void dgvPacientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                int idPaciente = Convert.ToInt32(e.CommandArgument);
                PacienteNegocio negocio = new PacienteNegocio();
                try
                {
                    negocio.EliminarPaciente(idPaciente);
                    cargarGrilla();
                }
                catch (Exception ex)
                {

                    Response.Write("<script>alert('Error al eliminar paciente: " + ex.Message + "');</script>");
                }

            }
        }

    }
}