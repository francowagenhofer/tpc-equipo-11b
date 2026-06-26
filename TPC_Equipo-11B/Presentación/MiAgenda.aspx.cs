using Dominio;
using Negocio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentación
{
    public partial class MiAgenda : PaginaProtegida
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ValidarRoles("Medico", "Administrador");

            if (!IsPostBack)
            {
                CargarEstados();
                CargarAgenda();
                CargarResumen();
            }
        }

        private void CargarAgenda()
        {
            TurnoNegocio negocio = new TurnoNegocio();

            List<Turno> lista = negocio.ListarTurnosPorMedico(UsuarioLogueado.Medico.Id);

            if (!string.IsNullOrWhiteSpace(txtFecha.Text))
            {
                DateTime fechaSeleccionada = DateTime.Parse(txtFecha.Text);
                lista = lista.FindAll(x => x.FechaHora.Date == fechaSeleccionada.Date);
            }

            string texto = txtBuscar.Text.Trim().ToLower();
            if (!string.IsNullOrWhiteSpace(texto))
                lista = lista.FindAll(x => x.Paciente.Usuario.Nombre.ToLower().Contains(texto) || x.Paciente.Usuario.Apellido.ToLower().Contains(texto));

            if (ddlEstado.SelectedIndex > 0)
                lista = lista.FindAll(x => x.EstadoTurno.Nombre == ddlEstado.SelectedItem.Text);

            dgvAgenda.DataSource = lista;
            dgvAgenda.DataBind();
        }

        private void CargarEstados()
        {
            EstadoTurnoNegocio negocio = new EstadoTurnoNegocio();

            ddlEstado.DataSource = negocio.ListarEstadosTurno();
            ddlEstado.DataTextField = "Nombre";
            ddlEstado.DataValueField = "Id";
            ddlEstado.DataBind();

            ddlEstado.Items.Insert(0, new ListItem("Todos los estados", "0"));
        }

        private void CargarResumen()
        {
            TurnoNegocio negocio = new TurnoNegocio();
            List<Turno> lista = negocio.ListarTurnosPorMedico(UsuarioLogueado.Medico.Id);

            lblTurnosHoy.Text = lista.Count(x => x.FechaHora.Date == DateTime.Today).ToString();
            lblPendientes.Text = lista.Count(x => x.EstadoTurno.Nombre == "Pendiente").ToString();
            lblConfirmados.Text = lista.Count(x => x.EstadoTurno.Nombre == "Confirmado").ToString();
            lblFinalizados.Text = lista.Count(x => x.EstadoTurno.Nombre == "Finalizado").ToString();
        }

        private void CargarModalTurno(int idTurno)
        {
            TurnoNegocio negocio = new TurnoNegocio();

            Turno turno = negocio.ObtenerTurnoPorId(idTurno);

            if (turno == null)
                return;

            lblCodigo.Text = turno.Codigo;
            lblFecha.Text = turno.FechaHora.ToString("dd/MM/yyyy");
            lblHora.Text = turno.FechaHora.ToString("HH:mm");
            lblDni.Text = turno.Paciente.DNI;
            lblPaciente.Text = turno.Paciente != null ? $"{turno.Paciente.Usuario.Apellido}, {turno.Paciente.Usuario.Nombre}": "-";
            lblObraSocial.Text = turno.Paciente?.ObraSocial?.Nombre ?? "-";
            lblEspecialidad.Text = turno.Especialidad?.Nombre ?? "-";

            lblMedico.Text = $"Dr. {turno.Medico.Usuario.Apellido}, {turno.Medico.Usuario.Nombre}";
            lblMatricula.Text = turno.Medico.Matricula;
            litEstado.Text = $"<span class='badge {ObtenerClaseBadge(turno.EstadoTurno.Nombre)}'>{turno.EstadoTurno.Nombre}</span>";
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

        protected void txtFecha_TextChanged(object sender, EventArgs e)
        {
            CargarAgenda();
        }

        protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarAgenda();
        }

        protected void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            CargarAgenda();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtFecha.Text = "";
            txtBuscar.Text = "";
            ddlEstado.SelectedIndex = 0;

            CargarAgenda();
        }

        protected void dgvAgenda_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvAgenda.PageIndex = e.NewPageIndex;
            CargarAgenda();
        }

        protected void dgvAgenda_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idTurno = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Ver")
            {
                CargarModalTurno(idTurno);

                string script = @"
                    window.addEventListener('load', function () {
                    
                        var elementoModal = document.getElementById('modalResumenTurno');
                    
                        if (elementoModal && window.bootstrap) {
                            bootstrap.Modal.getOrCreateInstance(elementoModal).show();
                        }
                    
                    });
                    ";

                ClientScript.RegisterStartupScript(
                    GetType(),
                    "MostrarResumenTurno",
                    script,
                    true);
            }

            if (e.CommandName == "Atender")
                Response.Redirect($"AtenderTurno.aspx?id={idTurno}");

            if (e.CommandName == "Historia")
                Response.Redirect($"HistoriaClinica.aspx?idTurno={idTurno}");
        }

        protected bool PuedeAtender(string estado)
        {
            return estado == "Pendiente" || estado == "Confirmado";
        }

    }
}
