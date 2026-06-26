using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio {
    public class Usuario {

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ImagenUrl { get; set; }
        public DateTime FechaAlta { get; set; }
        public int RolId { get; set; }
        public bool Activo { get; set; } = true;

        public Rol Rol { get; set; }
        public Medico Medico { get; set; }
        public Paciente Paciente { get; set; }
    }
}
