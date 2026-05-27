using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio {
    public class Turno {

<<<<<<< HEAD
        public int Id { get; set; }     
        public string Codigo { get; set; }      
        public int PacienteId { get; set; }     
        public int MedicoId { get; set; }
        public int EspecialidadId { get; set; }
        public Especialidad Especialidad { get; set; }
        public DateTime FechaHora { get; set; }
        public int EstadoTurnoId { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; } = DateTime.Now;
        public Paciente Paciente { get; set; }
        public Medico Medico { get; set; }
        public EstadoTurno EstadoTurno { get; set; }
        public string ObservacionesPaciente { get; set; }
        public string ObservacionesMedico { get; set; }
=======
        public int Id { get; set; }
        
        public string Codigo { get; set; }
        
        public int PacienteId { get; set; }
        
        public int MedicoId { get; set; }

        public DateTime FechaHora { get; set; }

        public int EstadoTurnoId { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaModificacion { get; set; } = DateTime.Now;

        public Paciente Paciente { get; set; }

        public Medico Medico { get; set; }

        public EstadoTurno EstadoTurno { get; set; }
>>>>>>> bc495b47fc094bca4181228944e53bdedead87ed

    }
}
