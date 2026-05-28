using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio {
    public class DisponibilidadMedico
    {
        public int Id { get; set; }

        public int MedicoId { get; set; }

        public int DiaSemana { get; set; }

        public TimeSpan HoraInicio { get; set; }

        public TimeSpan HoraFin { get; set; }

        public bool Activo { get; set; }

        public Medico Medico { get; set; }
    }
}
