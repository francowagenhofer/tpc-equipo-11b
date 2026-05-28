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


        //public virtual ICollection<MedicoEspecialidad> MedicoEspecialidades { get; set; }
        //public virtual ICollection<Turno> Turnos { get; set; }
        //public virtual ICollection<DisponibilidadMedico> Disponibilidades { get; set; }

        //public Medico() {

        //    MedicoEspecialidades = new HashSet<MedicoEspecialidad>();
        //    Turnos = new HashSet<Turno>();
        //    Disponibilidades = new HashSet<DisponibilidadMedico>();
        
        //}

    }
}
