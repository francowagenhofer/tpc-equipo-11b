using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio {
    public class Medico 
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }

        public string Matricula { get; set; }

        public bool Activo { get; set; }

        public Usuario Usuario { get; set; }
    }
}
