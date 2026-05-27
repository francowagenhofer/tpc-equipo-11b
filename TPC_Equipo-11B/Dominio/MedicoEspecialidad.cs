using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class MedicoEspecialidad
    {
        public int Id { get; set; }

        public int MedicoId { get; set; }

        public int EspecialidadId { get; set; }

        public Medico Medico { get; set; }

        public Especialidad Especialidad { get; set; }
    }
}
