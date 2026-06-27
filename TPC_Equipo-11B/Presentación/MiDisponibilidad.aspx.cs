using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Presentación {
    public partial class MiDisponibilidad : PaginaProtegida {
        private int MedicoIdActual
        {
            get
            {
                if (EsModoAdmin)
                {
                    return Convert.ToInt32(Request.QueryString["idMedico"]);
                }
                return UsuarioLogueado.Medico.Id;
            }
        }

        private bool EsModoAdmin
        {
            get
            {
                return UsuarioLogueado.Rol.Nombre == "Administrador"
                    && Request.QueryString["idMedico"] != null
                    && int.TryParse(Request.QueryString["idMedico"], out _);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ValidarRoles("Medico", "Administrador");

            if (!IsPostBack)
            {
                CargarHorasDisponibles();

                if (!CargarEncabezadoSiCorresponde())
                    return;

                CargarDisponibilidades();
                LimpiarFormulario();
            }
        }

        protected void ddlHoraInicio_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarHorasFin();
        }


        private void CargarHorasDisponibles()
        {
            ddlHoraInicio.Items.Clear();

            for (int hora = 8; hora <= 19; hora++)
            {
                string valor = hora.ToString("D2") + ":00";
                ddlHoraInicio.Items.Add(new ListItem(valor, valor));
            }

            CargarHorasFin();
        }

        private void CargarHorasFin()
        {
            string horaInicioSeleccionada = ddlHoraInicio.SelectedValue;
            ddlHoraFin.Items.Clear();

            int horaInicioInt = 8;
            if (!string.IsNullOrEmpty(horaInicioSeleccionada))
            {
                horaInicioInt = int.Parse(horaInicioSeleccionada.Split(':')[0]);
            }

            for (int hora = horaInicioInt + 1; hora <= 19; hora++)
            {
                string valor = hora.ToString("D2") + ":00";
                ddlHoraFin.Items.Add(new ListItem(valor, valor));
            }
        }

        private bool CargarEncabezadoSiCorresponde()
        {
            if (!EsModoAdmin)
            {
                pnlEncabezadoAdmin.Visible = false;
                return true;
            }

            MedicoNegocio medicoNegocio = new MedicoNegocio();
            Medico medico = medicoNegocio.ObtenerMedicoPorId(MedicoIdActual);

            if (medico == null)
            {
                MostrarError("El médico solicitado no existe.");
                pnlFormulario.Visible = false;
                pnlEncabezadoAdmin.Visible = false;
                return false;
            }

            litNombreMedicoAdmin.Text = $"Dr. {medico.Usuario.Apellido}, {medico.Usuario.Nombre} (Mat: {medico.Matricula})";
            pnlEncabezadoAdmin.Visible = true;
            return true;
        }

        protected string ObtenerNombreDia(int diaSemana)
        {
            switch (diaSemana)
            {
                case 1: return "Lunes";
                case 2: return "Martes";
                case 3: return "Miércoles";
                case 4: return "Jueves";
                case 5: return "Viernes";
                case 6: return "Sábado";
                case 7: return "Domingo";
                default: return "";
            }
        }

        private List<int> ObtenerDiasSeleccionados()
        {
            List<int> dias = new List<int>();

            if (chkLunes.Checked) dias.Add(1);
            if (chkMartes.Checked) dias.Add(2);
            if (chkMiercoles.Checked) dias.Add(3);
            if (chkJueves.Checked) dias.Add(4);
            if (chkViernes.Checked) dias.Add(5);
            if (chkSabado.Checked) dias.Add(6);
            if (chkDomingo.Checked) dias.Add(7);

            return dias;
        }

        private void CargarDisponibilidades()
        {
            DisponibilidadMedicoNegocio negocio = new DisponibilidadMedicoNegocio();

            dgvDisponibilidad.DataSource =
                negocio.ListarDisponibilidadesPorMedico(MedicoIdActual);

            dgvDisponibilidad.DataBind();

            pnlAvisoSinConfigurar.Visible = !negocio.TieneDisponibilidad(MedicoIdActual);
        }

        protected void btnGuardarDisponibilidad_Click(object sender, EventArgs e)
        {
            lblMensajeError.Visible = false;

            try
            {
                if (string.IsNullOrWhiteSpace(ddlHoraInicio.SelectedValue) ||
                    string.IsNullOrWhiteSpace(ddlHoraFin.SelectedValue))
                {
                    MostrarError("Debe completar todos los campos.");
                    return;
                }

                List<int> diasSeleccionados = ObtenerDiasSeleccionados();

                if (diasSeleccionados.Count == 0)
                {
                    MostrarError("Debe seleccionar al menos un día.");
                    return;
                }

                TimeSpan horaInicio = TimeSpan.Parse(ddlHoraInicio.SelectedValue);
                TimeSpan horaFin = TimeSpan.Parse(ddlHoraFin.SelectedValue);

                if (horaInicio >= horaFin)
                {
                    MostrarError("La hora de inicio debe ser menor que la hora de fin.");
                    return;
                }

                DisponibilidadMedicoNegocio negocio = new DisponibilidadMedicoNegocio();

                // MODIFICAR
                if (!string.IsNullOrWhiteSpace(hfIdDisponibilidad.Value))
                {
                    if (diasSeleccionados.Count > 1)
                    {
                        MostrarError("Al modificar un horario existente, seleccioná un solo día.");
                        return;
                    }

                    DisponibilidadMedico disponibilidad = new DisponibilidadMedico();

                    disponibilidad.Id = int.Parse(hfIdDisponibilidad.Value);
                    disponibilidad.MedicoId = MedicoIdActual;
                    disponibilidad.DiaSemana = diasSeleccionados[0];
                    disponibilidad.HoraInicio = horaInicio;
                    disponibilidad.HoraFin = horaFin;
                    disponibilidad.Activo = true;

                    if (negocio.ExisteSuperposicion(disponibilidad))
                    {
                        MostrarError("Ya existe un horario que se superpone.");
                        return;
                    }

                    negocio.ModificarDisponibilidad(disponibilidad);
                }
                else
                {
                    // AGREGAR
                    foreach (int dia in diasSeleccionados)
                    {
                        DisponibilidadMedico disponibilidad = new DisponibilidadMedico();

                        disponibilidad.MedicoId = MedicoIdActual;
                        disponibilidad.DiaSemana = dia;
                        disponibilidad.HoraInicio = horaInicio;
                        disponibilidad.HoraFin = horaFin;
                        disponibilidad.Activo = true;

                        if (!negocio.ExisteSuperposicion(disponibilidad))
                        {
                            negocio.AgregarDisponibilidad(disponibilidad);
                        }
                    }
                }

                LimpiarFormulario();

                CargarDisponibilidades();
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }

        protected void dgvDisponibilidad_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            DisponibilidadMedicoNegocio negocio = new DisponibilidadMedicoNegocio();

            if (e.CommandName == "Modificar")
            {
                int id = Convert.ToInt32(e.CommandArgument);

                DisponibilidadMedico disponibilidad = negocio.ObtenerPorId(id);

                if (disponibilidad == null)
                    return;

                chkLunes.Checked = false;
                chkMartes.Checked = false;
                chkMiercoles.Checked = false;
                chkJueves.Checked = false;
                chkViernes.Checked = false;
                chkSabado.Checked = false;
                chkDomingo.Checked = false;

                switch (disponibilidad.DiaSemana)
                {
                    case 1: chkLunes.Checked = true; break;
                    case 2: chkMartes.Checked = true; break;
                    case 3: chkMiercoles.Checked = true; break;
                    case 4: chkJueves.Checked = true; break;
                    case 5: chkViernes.Checked = true; break;
                    case 6: chkSabado.Checked = true; break;
                    case 7: chkDomingo.Checked = true; break;
                }

                ddlHoraInicio.SelectedValue = disponibilidad.HoraInicio.ToString(@"hh\:mm");
                CargarHorasFin(); // repobla ddlHoraFin según la nueva hora de inicio
                ddlHoraFin.SelectedValue = disponibilidad.HoraFin.ToString(@"hh\:mm");

                hfIdDisponibilidad.Value = disponibilidad.Id.ToString();

                btnGuardarDisponibilidad.Text = "Actualizar";
            }
            else if (e.CommandName == "Eliminar")
            {
                int id = Convert.ToInt32(e.CommandArgument);

                negocio.EliminarDisponibilidad(id);

                LimpiarFormulario();

                CargarDisponibilidades();
            }
        }

        private void LimpiarFormulario()
        {
            hfIdDisponibilidad.Value = "";

            chkLunes.Checked = false;
            chkMartes.Checked = false;
            chkMiercoles.Checked = false;
            chkJueves.Checked = false;
            chkViernes.Checked = false;
            chkSabado.Checked = false;
            chkDomingo.Checked = false;

            ddlHoraInicio.SelectedIndex = 0;
            ddlHoraFin.SelectedIndex = 0;

            btnGuardarDisponibilidad.Text = "Guardar disponibilidad";

            lblMensajeError.Visible = false;
        }

        private void MostrarError(string mensaje)
        {
            lblMensajeError.Text = mensaje;
            lblMensajeError.Visible = true;
        }
    }
}