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
        public string Matricula { get; set; } = string.Empty;
        public bool Activo { get; set; }

        public virtual Usuario Usuario { get; set; }
        public Especialidad Especialidad { get; set; }

    }
}
