using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace Presentación {
    public partial class Pacientes : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarGrilla();
            }
        }

        private void cargarGrilla()
        {
            PacienteNegocio negocio = new PacienteNegocio();
            try
            {
                // Pasamos la preferencia del Checkbox al método del Negocio
                dgvPacientes.DataSource = negocio.ListarPacientes(chkSoloActivos.Checked);
                dgvPacientes.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error al cargar pacientes: " + ex.Message + "');</script>");
            }
        }

        // Evento que se dispara al cambiar el estado del switch "Solo Activos"
        protected void chkSoloActivos_CheckedChanged(object sender, EventArgs e)
        {
            cargarGrilla();
        }

        protected void dgvPacientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idPaciente = Convert.ToInt32(e.CommandArgument);
            PacienteNegocio negocio = new PacienteNegocio();

            if (e.CommandName == "Eliminar")
            {
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
            else if (e.CommandName == "Reactivar")
            {
                try
                {
                    negocio.ReactivarPaciente(idPaciente);
                    cargarGrilla();
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('Error al reactivar paciente: " + ex.Message + "');</script>");
                }
            }
        }
    }
}
