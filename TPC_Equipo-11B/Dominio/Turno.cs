using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio {
    public class Turno {

        public int Id { get; set; }
        public int Numero { get; set; }
        public int PacienteId { get; set; }
        public int MedicoId { get; set; }
        public int Especialidad { get; set; }
        public DateTime FechaHora { get; set; }
        public string ObservacionesPaciente { get; set; }
        public string ObservacionesMedico { get; set; }
        public EstadoTurno Estado { get; set; } = EstadoTurno.nuevo;
        public DateTime FechaModificacion { get; set; } = DateTime.Now;
        public DateTime? FechaModificacion { get; set; }

    }
}
