using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Dominio;

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
                List<Turno> lista = negocio.ListarTurnos();
                Session["listaTurnos"] = lista;
                dgvTurnos.DataSource = lista;
                dgvTurnos.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error al cargar turnos: " + ex.Message + "');</script>");
            }
        }

        protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFiltros();
        }

        protected void txtFiltroBusqueda_TextChanged(object sender, EventArgs e)
        {
            AplicarFiltros();
        }

        protected void txtFechaFiltro_TextChanged(object sender, EventArgs e)
        {
            AplicarFiltros();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtFiltroBusqueda.Text = "";
            ddlEstado.SelectedIndex = 0;
            txtFechaFiltro.Text = "";

            CargarGrilla();
        }

        private void AplicarFiltros()
        {
            List<Turno> lista = (List<Turno>)Session["listaTurnos"];
            if (lista == null)
            {
                TurnoNegocio negocio = new TurnoNegocio();
                lista = negocio.ListarTurnos();
                Session["listaTurnos"] = lista;
            }

            // 1. Filtro por búsqueda rápida (Paciente, Médico o Código de Turno)
            string busqueda = txtFiltroBusqueda.Text.Trim().ToLower();
            if (!string.IsNullOrEmpty(busqueda))
            {
                lista = lista.FindAll(x =>
                    (x.Paciente != null && x.Paciente.Usuario != null &&
                     (x.Paciente.Usuario.Nombre.ToLower().Contains(busqueda) ||
                      x.Paciente.Usuario.Apellido.ToLower().Contains(busqueda)))
                    || (x.Paciente != null && x.Paciente.DNI.ToLower().Contains(busqueda))
                    || (x.Medico != null && x.Medico.Usuario != null &&
                     (x.Medico.Usuario.Nombre.ToLower().Contains(busqueda) ||
                      x.Medico.Usuario.Apellido.ToLower().Contains(busqueda)))
                    || (x.Codigo != null && x.Codigo.ToLower().Contains(busqueda))
                );
            }

            // 2. Filtro por Estado
            if (ddlEstado.SelectedValue != "0")
            {
                string estadoSeleccionado = ddlEstado.SelectedItem.Text.ToLower();
                lista = lista.FindAll(x =>
                    x.EstadoTurno != null &&
                    x.EstadoTurno.Nombre.ToLower() == estadoSeleccionado
                );
            }

            // 3. Filtro por Fecha
            if (!string.IsNullOrEmpty(txtFechaFiltro.Text) && DateTime.TryParse(txtFechaFiltro.Text, out DateTime fecha))
            {
                lista = lista.FindAll(x => x.FechaHora.Date == fecha.Date);
            }


            dgvTurnos.DataSource = lista;
            dgvTurnos.DataBind();
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

                case "no asistió":
                    return "estado-noasistio";

                default:
                    return "badge bg-secondary";
            }
        }
    }
}
