using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio {
    public class Medico 
    {
        public int Id { get; set; }
<<<<<<< HEAD
        public int UsuarioId { get; set; }
        public string Matricula { get; set; } = string.Empty;
        public bool Activo { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<MedicoEspecialidad> MedicoEspecialidades { get; set; }
        public virtual ICollection<Turno> Turnos { get; set; }
        public virtual ICollection<DisponibilidadMedico> Disponibilidades { get; set; }
=======
>>>>>>> bc495b47fc094bca4181228944e53bdedead87ed

        public int UsuarioId { get; set; }

<<<<<<< HEAD
        public Medico() {

            MedicoEspecialidades = new HashSet<MedicoEspecialidad>();
            Turnos = new HashSet<Turno>();
            Disponibilidades = new HashSet<DisponibilidadMedico>();
        
        }
=======
        public string Matricula { get; set; }

        public bool Activo { get; set; }
>>>>>>> bc495b47fc094bca4181228944e53bdedead87ed

        public Usuario Usuario { get; set; }
    }
}
