using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio {
    public class Especialidad 
    {

        public int Id { get; set; }
        
        public string Nombre { get; set; }
<<<<<<< HEAD
        public string Descripcion { get; set; }
=======
        
        public string Descripcion { get; set; }
        
>>>>>>> bc495b47fc094bca4181228944e53bdedead87ed
        public bool Activo { get; set; }
        public virtual ICollection<MedicoEspecialidad> MedicoEspecialidades { get; set; }

<<<<<<< HEAD
        public Especialidad() {

            MedicoEspecialidades = new HashSet<MedicoEspecialidad>();
        
        }

=======
>>>>>>> bc495b47fc094bca4181228944e53bdedead87ed
    }
}
