using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio {
    public class Paciente {

        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string DNI { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Direccion { get; set; }
        public int ObraSocialId { get; set; }
        public int GeneroId { get; set; }
        public bool Activo { get; set; } = true;

        public Usuario Usuario { get; set; }
        public Genero Genero { get; set; }
        public ObraSocial ObraSocial { get; set; }  
    }
}
