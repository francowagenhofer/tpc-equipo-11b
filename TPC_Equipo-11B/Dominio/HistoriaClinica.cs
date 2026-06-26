using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class HistoriaClinica
    {
        public int Id { get; set; }

        public int PacienteId { get; set; }

        public int MedicoId { get; set; }

        public int? TurnoId { get; set; }

        public DateTime Fecha { get; set; }

        public string Diagnostico { get; set; }

        public string Tratamiento { get; set; }

        public string Observaciones { get; set; }

        public bool Activo { get; set; }

        public Paciente Paciente { get; set; }

        public Medico Medico { get; set; }

        public Turno Turno { get; set; }
    }
}
