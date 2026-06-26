using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class MedicoObraSocial
    {
        public int Id { get; set; }

        public int IdMedico { get; set; }

        public int IdObraSocial { get; set; }

        public ObraSocial ObraSocial { get; set; }

        public Medico Medico { get; set; }
    }
}
