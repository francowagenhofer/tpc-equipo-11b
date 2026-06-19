using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace Presentación {
    public partial class Turnos : PaginaProtegida
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
            TurnoNegocio negocio = new TurnoNegocio();
            try
            {
                
                dgvTurnos.DataSource = negocio.ListarTurnos();
                dgvTurnos.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error al cargar turnos: " + ex.Message + "');</script>");
            }
        }
        
        protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            // lo dejo vacío por ahora mas adelante lo usaremos para filtrar por estado
        }
        protected void txtFiltroBusqueda_TextChanged(object sender, EventArgs e)
        {
            // lo dejo vacío por ahora mas adelante lo usaremos para filtrar por estado
        }

        protected void txtFechaFiltro_TextChanged(object sender, EventArgs e)
        {
            if (DateTime.TryParse(txtFechaFiltro.Text, out DateTime fecha))
            {
                // filtrar
            }
            else
            {
                // sin filtro
            }
        }


        protected void dgvTurnos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Cancelar")
            {
                int idTurno = Convert.ToInt32(e.CommandArgument);
                TurnoNegocio negocio = new TurnoNegocio();
                try
                {
                    if (negocio.CancelarTurno(idTurno))
                    {
                        CargarGrilla();
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('No se pudo cancelar el turno: " + ex.Message + "');</script>");
                }
            }
            else if (e.CommandName == "Modificar")
            {
                string idTurno = e.CommandArgument.ToString();
                Response.Redirect("NuevoTurno.aspx?id=" + idTurno);
            }
        }
        
        protected string ObtenerClaseBadge(string estado)
        {
            switch (estado.ToLower())
            {
                case "pendiente":
                    return "estado-pendiente";
                case "confirmado":
                    return "estado-confirmado";
                case "cancelado":
                    return "estado-cancelado";
                case "reprogramado":
                    return "estado-reprogramado";
                case "finalizado":
                    return "estado-finalizado";
                case "no asistio":
                    return "estado-noasistio";
                default:
                    return "badge bg-secondary";
            }
        }
    }
}