using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace Presentación
{
    public class PaginaProtegida: Page
    {
        protected Usuario UsuarioLogueado
        {
            get
            {
                return Session["usuarioLogueado"] as Usuario;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            if (UsuarioLogueado == null)
            {
                Response.Redirect("~/Login.aspx?acceso=denegado");
                return;
            }

            base.OnLoad(e);
        }

        protected void ValidarRoles(params string[] rolesPermitidos)
        {
            if (!rolesPermitidos.Contains(UsuarioLogueado.Rol.Nombre))
            {
                Response.Redirect("~/Default.aspx");
            }
        }
    }
}