using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio {
    public class Paciente {

        public int Id { get; set; }
<<<<<<< HEAD
        public string Nombre { get; set; }        
=======

        public string Nombre { get; set; }
        
>>>>>>> bc495b47fc094bca4181228944e53bdedead87ed
        public string Apellido { get; set; }
        
        public string DNI { get; set; }
<<<<<<< HEAD
        public DateTime FechaNacimiento { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
=======
        
        public DateTime FechaNacimiento { get; set; }
        
        public string Email { get; set; }
        
        public string Telefono { get; set; }
        
>>>>>>> bc495b47fc094bca4181228944e53bdedead87ed
        public string Direccion { get; set; }
        
        public string ObraSocial { get; set; }
        
        public bool Activo { get; set; } = true;
        public virtual ICollection<Turno> Turnos { get; set; }
        public virtual ICollection<HistoriaClinica> HistoriasClinicas { get; set; }

<<<<<<< HEAD
        public Paciente() {

            Turnos = new HashSet<Turno>();
            HistoriasClinicas = new HashSet<HistoriaClinica>();
        
        }

=======
>>>>>>> bc495b47fc094bca4181228944e53bdedead87ed
    }
}
