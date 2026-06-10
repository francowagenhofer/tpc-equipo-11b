using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace Presentación
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                MostrarDashboard();
            }

        }

        private void MostrarDashboard()
        {
            // Simulación hasta implementar login
            string rol = "Recepcionista";

            OcultarTodos();

            switch (rol)
            {
                case "Administrador":
                    pnlAdministrador.Visible = true;
                    break;

                case "Recepcionista":
                    pnlRecepcionista.Visible = true;
                    break;

                case "Medico":
                    pnlMedico.Visible = true;
                    break;

                case "Paciente":
                    pnlPaciente.Visible = true;
                    break;
            }
        }


        private void OcultarTodos()
        {
            pnlAdministrador.Visible = false;
            pnlRecepcionista.Visible = false;
            pnlMedico.Visible = false;
            pnlPaciente.Visible = false;
        }
    }
}