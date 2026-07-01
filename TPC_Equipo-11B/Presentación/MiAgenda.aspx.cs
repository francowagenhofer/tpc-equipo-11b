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
            ValidarRoles("Administrador", "Medico", "Paciente");

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
            List<Turno> lista;

            if (UsuarioLogueado.Rol.Nombre == "Medico")
                lista = negocio.ListarTurnosPorMedico(UsuarioLogueado.Medico.Id);
            else if (UsuarioLogueado.Rol.Nombre == "Paciente")
                lista = negocio.ListarTurnosPorPaciente(UsuarioLogueado.Paciente.Id);
            else
                lista = negocio.ListarTurnos();

            if (!string.IsNullOrWhiteSpace(txtFecha.Text))
            {
                DateTime fechaSeleccionada = DateTime.Parse(txtFecha.Text);
                lista = lista.FindAll(x => x.FechaHora.Date == fechaSeleccionada.Date);
            }

            string texto = txtBuscar.Text.Trim().ToLower();

            if (!string.IsNullOrWhiteSpace(texto))
            {
                if (UsuarioLogueado.Rol.Nombre == "Paciente")
                {
                    lista = lista.FindAll(x =>
                        x.Medico.Usuario.Nombre.ToLower().Contains(texto) ||
                        x.Medico.Usuario.Apellido.ToLower().Contains(texto));
                }
                else
                {
                    lista = lista.FindAll(x =>
                        x.Paciente.Usuario.Nombre.ToLower().Contains(texto) ||
                        x.Paciente.Usuario.Apellido.ToLower().Contains(texto));
                }
            }

            if (ddlEstado.SelectedIndex > 0)
                lista = lista.FindAll(x => x.EstadoTurno.Id == int.Parse(ddlEstado.SelectedValue));

            dgvAgenda.DataSource = lista;
            dgvAgenda.DataBind();

            ConfigurarColumnasAgenda();
        }


        private void ConfigurarColumnasAgenda()
        {
            bool esPaciente = UsuarioLogueado.Rol.Nombre == "Paciente";

            dgvAgenda.Columns[2].Visible = !esPaciente; // Paciente
            dgvAgenda.Columns[3].Visible = !esPaciente; // Obra Social

            dgvAgenda.Columns[4].Visible = esPaciente;  // Médico
            dgvAgenda.Columns[5].Visible = esPaciente;  // Especialidad
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
            List<Turno> lista;

            if (UsuarioLogueado.Rol.Nombre == "Medico")
                lista = negocio.ListarTurnosPorMedico(UsuarioLogueado.Medico.Id);
            else if (UsuarioLogueado.Rol.Nombre == "Paciente")
                lista = negocio.ListarTurnosPorPaciente(UsuarioLogueado.Paciente.Id);
            else
                lista = negocio.ListarTurnos();

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
            lblPaciente.Text = turno.Paciente != null ? $"{turno.Paciente.Usuario.Apellido}, {turno.Paciente.Usuario.Nombre}" : "-";
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

            else if (e.CommandName == "Atender")
            {
                Response.Redirect($"AtenderTurno.aspx?id={idTurno}");
            }

            else if (e.CommandName == "Historia")
            {
                HistoriaClinicaNegocio historiaNegocio = new HistoriaClinicaNegocio();
                HistoriaClinica historia = historiaNegocio.ObtenerHCPorTurno(idTurno);

                if (historia != null)
                    Response.Redirect($"HistoriaClinicaDetalle.aspx?id={historia.Id}");
            }

            else if (e.CommandName == "Confirmar")
            {
                TurnoNegocio turnoNegocio = new TurnoNegocio();

                turnoNegocio.ConfirmarTurno(idTurno);

                CargarAgenda();
                CargarResumen();
            }

            else if (e.CommandName == "Cancelar")
            {
                TurnoNegocio turnoNegocio = new TurnoNegocio();

                turnoNegocio.CancelarTurno(idTurno);

                CargarAgenda();
                CargarResumen();
            }
        }

        protected bool PuedeAtender(string estado)
        {
            return estado == "Pendiente" || estado == "Confirmado";
        }

    }
}
