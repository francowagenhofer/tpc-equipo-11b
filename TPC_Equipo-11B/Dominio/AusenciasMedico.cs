using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class AusenciaMedico
    {
        public int Id { get; set; }

        public int IdMedico { get; set; }

        public DateTime Fecha { get; set; }

        public string Motivo { get; set; }

        public Medico Medico { get; set; }
    }
}
