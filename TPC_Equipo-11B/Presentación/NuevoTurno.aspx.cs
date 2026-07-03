using Dominio;
using Negocio;
using Presentación;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion
{
    public partial class NuevoTurno : PaginaProtegida
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ValidarRoles("Administrador", "Recepcionista");

            if (!IsPostBack)
            {
                CargarFiltroObrasSociales();
                CargarPacientes();
                CargarEspecialidades();
                CargarHoras();

                if (Request.QueryString["id"] != null)
                {
                    int idTurno;
                    if (int.TryParse(Request.QueryString["id"], out idTurno))
                    {
                        CargarTurno(idTurno);
                    }
                }
            }
        }

        private void MostrarMensaje(string texto, string tipo)
        {
            lblMensaje.Text = texto;
            lblMensaje.CssClass = "alert alert-" + tipo + " d-block";
            lblMensaje.Visible = true;
        }

        private void LimpiarMensaje()
        {
            lblMensaje.Text = string.Empty;
            lblMensaje.Visible = false;
        }

        private void CargarHoras()
        {
            rblHora.Items.Clear();

            for (int hora = 8; hora <= 19; hora++)
            {
                string valor = hora.ToString("D2") + ":00";
                ListItem item = new ListItem(valor, valor);
                item.Attributes["class"] = "horario-radio";
                rblHora.Items.Add(item);
            }
        }

        private void CargarFiltroObrasSociales()
        {
            ObraSocialNegocio negocio = new ObraSocialNegocio();

            ddlFiltroObraSocial.Items.Clear();
            ddlFiltroObraSocial.Items.Add(new ListItem("Todas las obras sociales", ""));

            foreach (ObraSocial obra in negocio.ListarObrasSociales())
            {
                ddlFiltroObraSocial.Items.Add(new ListItem($"{obra.Nombre} - {obra.TipoPlan}", obra.Id.ToString()));
            }

            foreach (ListItem item in ddlFiltroObraSocial.Items)
            {
                System.Diagnostics.Debug.WriteLine(
                    item.Text + " -> " + item.Value);
            }
        }

        private void CargarPacientes()
        {
            try
            {
                PacienteNegocio negocio = new PacienteNegocio();
                List<Paciente> lista = negocio.ListarPacientes();

                string busqueda = txtBuscarPaciente.Text.Trim().ToLower();
                int idObraSocial;
                bool filtraObraSocial = int.TryParse(ddlFiltroObraSocial.SelectedValue, out idObraSocial);

                if (!string.IsNullOrEmpty(busqueda))
                {
                    lista = lista.Where(p =>
                        (p.DNI != null && p.DNI.ToLower().Contains(busqueda)) ||
                        (p.Usuario != null && p.Usuario.Nombre != null && p.Usuario.Nombre.ToLower().Contains(busqueda)) ||
                        (p.Usuario != null && p.Usuario.Apellido != null && p.Usuario.Apellido.ToLower().Contains(busqueda))
                    ).ToList();
                }

                if (filtraObraSocial)
                {
                    lista = lista.Where(p => p.ObraSocial != null && p.ObraSocial.Id == idObraSocial).ToList();
                }

                ddlPaciente.Items.Clear();
                ddlPaciente.Items.Add(new ListItem("Seleccione un paciente", ""));

                foreach (Paciente paciente in lista.OrderBy(p => p.Usuario.Apellido).ThenBy(p => p.Usuario.Nombre))
                {
                    string texto = paciente.Usuario.Apellido + ", " + paciente.Usuario.Nombre + " - DNI " + paciente.DNI;
                    ddlPaciente.Items.Add(new ListItem(texto, paciente.Id.ToString()));
                }

                lblResultadoPacientes.Text = lista.Count == 1 ? "1 paciente encontrado" : lista.Count + " pacientes encontrados";

                rptPacientes.DataSource = lista;
                rptPacientes.DataBind();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar pacientes: " + ex.Message, "danger");
            }
        }

        private void CargarEspecialidades()
        {
            try
            {
                EspecialidadNegocio negocio = new EspecialidadNegocio();
                List<Especialidad> lista = negocio.ListarEspecialidades();

                ddlEspecialidad.Items.Clear();
                ddlEspecialidad.Items.Add(new ListItem("Seleccione una especialidad", ""));

                foreach (Especialidad especialidad in lista.OrderBy(e => e.Nombre))
                {
                    ddlEspecialidad.Items.Add(new ListItem(especialidad.Nombre, especialidad.Id.ToString()));
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar especialidades: " + ex.Message, "danger");
            }
        }

        private void CargarMedicosDisponibles(int idEspecialidad, int idObraSocial)
        {
            try
            {
                MedicoNegocio negocio = new MedicoNegocio();
                List<Medico> lista = negocio.ListarMedicosDisponibles(idEspecialidad, idObraSocial);

                rptMedicos.DataSource = lista;
                rptMedicos.DataBind();

                pnlSinMedicos.Visible = lista.Count == 0;
                lblCantidadMedicos.Text = lista.Count == 1 ? "1 profesional disponible" : lista.Count + " profesionales disponibles";

                if (lista.Count == 0)
                {
                    hfMedicoSeleccionado.Value = string.Empty;
                    lblMedicoSeleccionado.Text = "Sin seleccionar";
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar profesionales: " + ex.Message, "danger");
            }
        }
        
        protected void txtBuscarPaciente_TextChanged(object sender, EventArgs e)
        {
            CargarPacientes();
        }

        protected void ddlFiltroObraSocial_SelectedIndexChanged(object sender, EventArgs e)
        {
            LimpiarMensaje();
            CargarPacientes();
            LimpiarPacienteSeleccionado(false);
            LimpiarEspecialidadYMedico();
        }

        protected void ddlPaciente_SelectedIndexChanged(object sender, EventArgs e)
        {
            LimpiarMensaje();

            if (string.IsNullOrEmpty(ddlPaciente.SelectedValue))
            {
                LimpiarPacienteSeleccionado(false);
                LimpiarEspecialidadYMedico();
                return;
            }

            try
            {
                PacienteNegocio negocio = new PacienteNegocio();
                Paciente paciente = negocio.ObtenerPacientePorId(Convert.ToInt32(ddlPaciente.SelectedValue));

                if (paciente == null)
                {
                    MostrarMensaje("No se encontro el paciente seleccionado.", "warning");
                    return;
                }

                lblPacienteSeleccionado.Text = paciente.Usuario.Apellido + ", " + paciente.Usuario.Nombre;
                lblDNI.Text = paciente.DNI;
                lblEdad.Text = CalcularEdad(paciente.FechaNacimiento) + " años";

                if (paciente.ObraSocial != null)
                {
                    lblObraSocial.Text = $"{paciente.ObraSocial.Nombre} - {paciente.ObraSocial.TipoPlan}";
                    ddlFiltroObraSocial.SelectedValue = paciente.ObraSocial.Id.ToString();
                }
                else
                {
                    lblObraSocial.Text = "Sin obra social";
                    ddlFiltroObraSocial.SelectedIndex = 0;
                }

                LimpiarEspecialidadYMedico();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al seleccionar paciente: " + ex.Message, "danger");
            }
        }

        protected void ddlEspecialidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            LimpiarMensaje();
            hfMedicoSeleccionado.Value = string.Empty;
            lblMedicoSeleccionado.Text = "Sin seleccionar";
            hfHoraSeleccionada.Value = string.Empty;
            rblHora.ClearSelection();

            if (string.IsNullOrEmpty(ddlPaciente.SelectedValue))
            {
                ddlEspecialidad.SelectedIndex = 0;
                MostrarMensaje("Primero seleccione un paciente para conocer su obra social.", "warning");
                return;
            }

            if (string.IsNullOrEmpty(ddlEspecialidad.SelectedValue))
            {
                LimpiarEspecialidadYMedico();
                return;
            }

            if (string.IsNullOrEmpty(ddlFiltroObraSocial.SelectedValue))
            {
                MostrarMensaje("El paciente seleccionado no tiene una obra social asociada para filtrar profesionales.", "warning");
                return;
            }

            int idEspecialidad = Convert.ToInt32(ddlEspecialidad.SelectedValue);
            int idObraSocial = Convert.ToInt32(ddlFiltroObraSocial.SelectedValue);

            lblEspecialidadSeleccionada.Text = ddlEspecialidad.SelectedItem.Text;
            CargarMedicosDisponibles(idEspecialidad, idObraSocial);
        }
        
        protected void btnLimpiarPaciente_Click(object sender, EventArgs e)
        {
            LimpiarMensaje();
            txtBuscarPaciente.Text = string.Empty;
            ddlFiltroObraSocial.SelectedIndex = 0;
            CargarPacientes();
            LimpiarPacienteSeleccionado(true);
            LimpiarEspecialidadYMedico();
            ReiniciarAgenda();
        }

        protected void btnLimpiarEspecialidad_Click(object sender, EventArgs e)
        {
            ReiniciarAgenda();
            LimpiarMensaje();
            LimpiarEspecialidadYMedico();
        }

        protected void rptMedicos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
            {
                return;
            }

            Medico medico = (Medico)e.Item.DataItem;
            LinkButton boton = (LinkButton)e.Item.FindControl("btnSeleccionarMedico");

            if (boton != null && medico != null && hfMedicoSeleccionado.Value == medico.Id.ToString())
            {
                boton.CssClass += " is-selected";
            }
        }

        protected void rptMedicos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName != "SeleccionarMedico")
            {
                return;
            }

            int idMedico = Convert.ToInt32(e.CommandArgument);
            hfMedicoSeleccionado.Value = idMedico.ToString();
            hfHoraSeleccionada.Value = string.Empty;
            rblHora.ClearSelection();

            MedicoNegocio negocio = new MedicoNegocio();
            Medico medico = negocio.ObtenerMedicoPorId(idMedico);

            if (medico != null)
            {
                lblMedicoSeleccionado.Text = "Dr. " + medico.Usuario.Apellido + ", " + medico.Usuario.Nombre + " - " + medico.Matricula;
            }

            if (!string.IsNullOrEmpty(ddlFiltroObraSocial.SelectedValue))
            {
                MedicoObraSocialNegocio coberturaNegocio = new MedicoObraSocialNegocio();

                int idObraSocialPaciente = Convert.ToInt32(ddlFiltroObraSocial.SelectedValue);

                if (coberturaNegocio.AtiendeObraSocial(idMedico, idObraSocialPaciente))
                {
                    lblCoberturaTurno.Text = ddlFiltroObraSocial.SelectedItem.Text;
                }
                else
                {
                    lblCoberturaTurno.Text = "Consulta Particular";
                }
            }
            else
            {
                lblCoberturaTurno.Text = "-";
            }

            CargarProximoTurnoDisponible(idMedico);
            RecargarMedicosManteniendoSeleccion();
        }

        private void CargarProximoTurnoDisponible(int idMedico)
        {
            TurnoNegocio negocio = new TurnoNegocio();

            DateTime? proximo = negocio.ObtenerPrimerHorarioDisponible(idMedico);

            if (proximo == null)
            {
                lblProximoTurno.Text = "No hay disponibilidad.";
                txtFecha.Text = "";
                hfHoraSeleccionada.Value = "";
                rblHora.ClearSelection();
                rblHora.Items.Clear();
                return;
            }

            txtFecha.Text = proximo.Value.ToString("yyyy-MM-dd");

            lblProximoTurno.Text = proximo.Value.ToString("dddd dd 'de' MMMM - HH:mm");

            CargarHorasDisponibles(idMedico, proximo.Value.Date);

            rblHora.SelectedValue = proximo.Value.ToString("HH:mm");
        }

        private void CargarHorasDisponibles(int idMedico, DateTime fecha)
        {
            TurnoNegocio negocio = new TurnoNegocio();

            rblHora.Items.Clear();

            List<DateTime> ocupadas = negocio.ObtenerHorasOcupadas(idMedico, fecha);

            for (int hora = 8; hora <= 19; hora++)
            {
                DateTime candidato = fecha.Date.AddHours(hora);

                ListItem item = new ListItem(
                    candidato.ToString("HH:mm"),
                    candidato.ToString("HH:mm"));

                // Si el horario ya pasó (solo para el día de hoy)
                if (fecha.Date == DateTime.Today && candidato <= DateTime.Now)
                {
                    item.Enabled = false;
                }
                // Si el médico no trabaja o está ausente
                else if (!negocio.MedicoDisponibleEnFechaHora(idMedico, candidato))
                {
                    item.Enabled = false;
                }
                // Si el horario ya está ocupado
                else if (ocupadas.Any(x => x == candidato))
                {
                    item.Enabled = false;
                }

                rblHora.Items.Add(item);
            }
        }

        protected void txtFecha_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hfMedicoSeleccionado.Value))
                return;

            int idMedico = Convert.ToInt32(hfMedicoSeleccionado.Value);

            CargarHorasDisponibles(idMedico, DateTime.Parse(txtFecha.Text));
        }

        private void ReiniciarAgenda()
        {
            hfMedicoSeleccionado.Value = "";
            hfHoraSeleccionada.Value = "";

            txtFecha.Text = "";

            lblMedicoSeleccionado.Text = "Sin seleccionar";
            lblProximoTurno.Text = "Seleccione un profesional.";

            rblHora.Items.Clear();
            rblHora.ClearSelection();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            LimpiarMensaje();

            if (!ValidarFormulario())
            {
                return;
            }

            try
            {
                int idTurnoEnEdicion = ObtenerIdTurnoEnEdicion();
                bool esNuevo = idTurnoEnEdicion == 0;

                Turno nuevo = new Turno();
                nuevo.Id = idTurnoEnEdicion;
                nuevo.PacienteId = Convert.ToInt32(ddlPaciente.SelectedValue);
                nuevo.MedicoId = Convert.ToInt32(hfMedicoSeleccionado.Value);
                nuevo.FechaHora = ObtenerFechaHoraSeleccionada();
                nuevo.EspecialidadId = Convert.ToInt32(ddlEspecialidad.SelectedValue);

                TurnoNegocio negocio = new TurnoNegocio();

                if (nuevo.FechaHora <= DateTime.Now)
                {
                    MostrarMensaje("No se pueden agendar turnos en una fecha y hora pasada.", "warning");
                    return;
                }

                if (!negocio.MedicoDisponibleEnFechaHora(nuevo.MedicoId, nuevo.FechaHora))
                {
                    MostrarMensaje("El medico no atiende o figura ausente en el horario seleccionado.", "warning");
                    return;
                }

                if (HorarioMedicoOcupado(negocio, nuevo.MedicoId, nuevo.FechaHora, idTurnoEnEdicion))
                {
                    MostrarMensaje("Ese horario ya fue reservado para el medico seleccionado.", "warning");
                    return;
                }

                if (!negocio.PacienteDisponibleEnFechaHora(nuevo.PacienteId, nuevo.FechaHora, idTurnoEnEdicion))
                {
                    MostrarMensaje("El paciente ya tiene un turno reservado para esa misma fecha y hora.", "warning");
                    return;
                }

                bool resultado;

                if (esNuevo)
                {
                    nuevo.Codigo = "TRN-" + DateTime.Now.ToString("yyyyMMdd-HHmmss");
                    resultado = negocio.AgregarTurno(nuevo);
                }
                else
                {
                    resultado = negocio.ModificarTurno(nuevo);
                }

                if (resultado)
                {
                    Response.Redirect("Turnos.aspx", false);
                }
                else
                {
                    MostrarMensaje("No se pudo guardar el turno. Intente nuevamente.", "danger");
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("No se pudo agendar el turno: " + ex.Message, "danger");
            }
        }

        [WebMethod]
        public static List<string> ObtenerHorasNoDisponiblesAjax(int idMedico, string fecha, int idTurnoActual)
        {
            List<string> resultado = new List<string>();

            if (idMedico <= 0 || string.IsNullOrWhiteSpace(fecha))
            {
                return resultado;
            }

            DateTime fechaParseada;
            if (!DateTime.TryParse(fecha, out fechaParseada))
            {
                return resultado;
            }

            TurnoNegocio negocio = new TurnoNegocio();
            DateTime fechaHoraOriginal = ObtenerFechaHoraOriginalStatic(idTurnoActual, negocio);
            List<DateTime> ocupadas = negocio.ObtenerHorasOcupadas(idMedico, fechaParseada);

            for (int hora = 8; hora <= 19; hora++)
            {
                DateTime candidata = fechaParseada.Date.AddHours(hora);
                string horaTexto = hora.ToString("D2") + ":00";

                bool esHoraOriginalDelTurno = idTurnoActual > 0 && candidata == fechaHoraOriginal;
                bool noDisponible = candidata <= DateTime.Now || !negocio.MedicoDisponibleEnFechaHora(idMedico, candidata);
                bool ocupada = ocupadas.Any(h => h == candidata && !esHoraOriginalDelTurno);

                if (noDisponible || ocupada)
                {
                    resultado.Add(horaTexto);
                }
            }

            return resultado.Distinct().ToList();
        }

        private bool ValidarFormulario()
        {
            if (string.IsNullOrEmpty(ddlPaciente.SelectedValue))
            {
                MostrarMensaje("Seleccione un paciente.", "warning");
                return false;
            }

            if (string.IsNullOrEmpty(ddlEspecialidad.SelectedValue))
            {
                MostrarMensaje("Seleccione una especialidad.", "warning");
                return false;
            }

            if (string.IsNullOrEmpty(hfMedicoSeleccionado.Value))
            {
                MostrarMensaje("Seleccione un profesional.", "warning");
                return false;
            }

            if (string.IsNullOrEmpty(txtFecha.Text))
            {
                MostrarMensaje("Seleccione una fecha.", "warning");
                return false;
            }

            if (string.IsNullOrEmpty(hfHoraSeleccionada.Value) && string.IsNullOrEmpty(rblHora.SelectedValue))
            {
                MostrarMensaje("Seleccione un horario.", "warning");
                return false;
            }

            return true;
        }

        private DateTime ObtenerFechaHoraSeleccionada()
        {
            DateTime fecha = Convert.ToDateTime(txtFecha.Text);
            string horaTexto = !string.IsNullOrEmpty(hfHoraSeleccionada.Value) ? hfHoraSeleccionada.Value : rblHora.SelectedValue;
            TimeSpan hora = TimeSpan.Parse(horaTexto);
            return fecha.Date.Add(hora);
        }

        private bool HorarioMedicoOcupado(TurnoNegocio negocio, int idMedico, DateTime fechaHora, int idTurnoEnEdicion)
        {
            DateTime fechaHoraOriginal = ObtenerFechaHoraOriginal(idTurnoEnEdicion);

            return negocio.ObtenerHorasOcupadas(idMedico, fechaHora.Date)
                .Any(h => h == fechaHora && (idTurnoEnEdicion == 0 || h != fechaHoraOriginal));
        }

        private int ObtenerIdTurnoEnEdicion()
        {
            int idTurno;
            return int.TryParse(Request.QueryString["id"], out idTurno) ? idTurno : 0;
        }

        private static DateTime ObtenerFechaHoraOriginalStatic(int idTurno, TurnoNegocio negocio)
        {
            if (idTurno <= 0)
            {
                return DateTime.MinValue;
            }

            Turno turno = negocio.ObtenerTurnoPorId(idTurno);
            return turno != null ? turno.FechaHora : DateTime.MinValue;
        }

        private DateTime ObtenerFechaHoraOriginal(int idTurno)
        {
            TurnoNegocio negocio = new TurnoNegocio();
            return ObtenerFechaHoraOriginalStatic(idTurno, negocio);
        }

        private int CalcularEdad(DateTime fechaNacimiento)
        {
            int edad = DateTime.Today.Year - fechaNacimiento.Year;

            if (fechaNacimiento > DateTime.Today.AddYears(-edad))
            {
                edad--;
            }

            return edad;
        }

        private void LimpiarPacienteSeleccionado(bool limpiarCombo)
        {
            if (limpiarCombo)
            {
                ddlPaciente.SelectedIndex = 0;
            }

            lblPacienteSeleccionado.Text = "-";
            lblDNI.Text = "-";
            lblEdad.Text = "-";
            lblObraSocial.Text = "-";
        }

        private void LimpiarEspecialidadYMedico()
        {
            ddlEspecialidad.SelectedIndex = 0;
            lblEspecialidadSeleccionada.Text = "-";
            lblCantidadMedicos.Text = "Seleccione paciente y especialidad";
            lblMedicoSeleccionado.Text = "Sin seleccionar";
            hfMedicoSeleccionado.Value = string.Empty;
            hfHoraSeleccionada.Value = string.Empty;
            rblHora.ClearSelection();
            rptMedicos.DataSource = null;
            rptMedicos.DataBind();
            pnlSinMedicos.Visible = false;
            lblCoberturaTurno.Text = "-";
        }

        private void RecargarMedicosManteniendoSeleccion()
        {
            if (string.IsNullOrEmpty(ddlEspecialidad.SelectedValue) || string.IsNullOrEmpty(ddlFiltroObraSocial.SelectedValue))
            {
                return;
            }

            CargarMedicosDisponibles(
                Convert.ToInt32(ddlEspecialidad.SelectedValue),
                Convert.ToInt32(ddlFiltroObraSocial.SelectedValue)
            );
        }

        protected void rptPacientes_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName != "SeleccionarPaciente")
                return;

            ddlPaciente.SelectedValue = e.CommandArgument.ToString();
            ddlPaciente_SelectedIndexChanged(ddlPaciente, EventArgs.Empty);

            rptPacientes.DataBind();
        }

        protected void rptPacientes_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
                return;

            LinkButton btn = (LinkButton)e.Item.FindControl("btnSeleccionarPaciente");

            if (btn != null && btn.CommandArgument == ddlPaciente.SelectedValue)
                btn.CssClass += " is-selected";
        }

        private void CargarTurno(int idTurno)
        {
            try
            {
                TurnoNegocio negocio = new TurnoNegocio();
                Turno turno = negocio.ObtenerTurnoPorId(idTurno);

                if (turno == null)
                {
                    MostrarMensaje("No se encontro el turno solicitado.", "warning");
                    return;
                }

                litTitulo.Text = "Modificar turno";
                btnGuardar.Text = "Guardar cambios";

                ddlPaciente.SelectedValue = turno.PacienteId.ToString();
                ddlPaciente_SelectedIndexChanged(null, EventArgs.Empty);

                ddlEspecialidad.SelectedValue = turno.Medico.EspecialidadId.ToString();
                lblEspecialidadSeleccionada.Text = ddlEspecialidad.SelectedItem.Text;

                CargarMedicosDisponibles(turno.Medico.EspecialidadId, turno.Paciente.ObraSocial.Id);
                hfMedicoSeleccionado.Value = turno.MedicoId.ToString();
                lblMedicoSeleccionado.Text = "Dr. " + turno.Medico.Usuario.Apellido + ", " + turno.Medico.Usuario.Nombre + " - MP " + turno.Medico.Matricula;
                RecargarMedicosManteniendoSeleccion();

                txtFecha.Text = turno.FechaHora.ToString("yyyy-MM-dd");
                string hora = turno.FechaHora.ToString("HH:mm");
                hfHoraSeleccionada.Value = hora;

                ListItem itemHora = rblHora.Items.FindByValue(hora);
                if (itemHora != null)
                {
                    itemHora.Selected = true;
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar datos del turno: " + ex.Message, "danger");
            }
        }
    }
}
