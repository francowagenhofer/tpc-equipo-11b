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
    public partial class HistorialClinico : PaginaProtegida
    {
        private List<HistoriaClinica> lista;

        protected void Page_Load(object sender, EventArgs e)
        {
            ValidarRoles("Administrador", "Medico", "Paciente");

            if (!IsPostBack)
            {
                CargarGrilla();
                CargarResumen();
            }
        }

        private void CargarGrilla()
        {
            HistoriaClinicaNegocio negocio = new HistoriaClinicaNegocio();

            if (UsuarioLogueado.Rol.Nombre == "Administrador")
                lista = negocio.ListarHC();
            else if (UsuarioLogueado.Rol.Nombre == "Medico")
                lista = negocio.ListarHCPorMedico(UsuarioLogueado.Medico.Id);
            else
                lista = negocio.ListarHCPorPaciente(UsuarioLogueado.Paciente.Id);

            // Filtro por fecha
            if (!string.IsNullOrWhiteSpace(txtFecha.Text))
            {
                DateTime fecha = DateTime.Parse(txtFecha.Text);

                lista = lista.FindAll(x => x.Fecha.Date == fecha.Date);
            }

            // Filtro por búsqueda
            string texto = txtBuscar.Text.Trim().ToLower();

            if (!string.IsNullOrWhiteSpace(texto))
            {
                lista = lista.FindAll(x =>
                    x.Diagnostico.ToLower().Contains(texto)
                    || x.Medico.Usuario.Nombre.ToLower().Contains(texto)
                    || x.Medico.Usuario.Apellido.ToLower().Contains(texto));
            }

            Session["listaHC"] = lista;

            dgvHistoriaClinica.DataSource = lista;
            dgvHistoriaClinica.DataBind();

            AjustarColumnasSegunRol();
        }

        private void CargarResumen()
        {
            List<HistoriaClinica> lista = Session["listaHC"] as List<HistoriaClinica>;

            if (lista == null)
                return;

            lblTotalHistorias.Text = lista.Count.ToString();

            lblEsteMes.Text = lista.Count(x =>
                                x.Fecha.Month == DateTime.Today.Month &&
                                x.Fecha.Year == DateTime.Today.Year).ToString();

            if (lista.Any())
                lblUltimaConsulta.Text = lista.Max(x => x.Fecha).ToString("dd/MM/yyyy");
            else
                lblUltimaConsulta.Text = "-";
        }

        private void AjustarColumnasSegunRol()
        {
            if (UsuarioLogueado.Rol.Nombre == "Medico")
            {
                dgvHistoriaClinica.Columns[3].Visible = false; 
                dgvHistoriaClinica.Columns[4].Visible = false; 
            }
            else if (UsuarioLogueado.Rol.Nombre == "Paciente")
            {
                dgvHistoriaClinica.Columns[2].Visible = false;
            }
        }

        protected void txtFecha_TextChanged(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        protected void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
            txtFecha.Text = "";

            CargarGrilla();
        }

        protected void dgvHistoriaClinica_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvHistoriaClinica.PageIndex = e.NewPageIndex;
            CargarGrilla();
        }

        protected void dgvHistoriaClinica_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Detalle")
            {
                int id = Convert.ToInt32(e.CommandArgument);

                Response.Redirect($"HistoriaClinicaDetalle.aspx?id={id}");
            }
        }

    }
}