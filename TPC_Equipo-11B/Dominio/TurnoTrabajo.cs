using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio {
    public class TurnoTrabajo {

        public int Id { get; set; }
        public string Nombre { get; set; } // o lo podemos llamar NombreHorario (mañana, tarde o noche, )
        public TimeSpan HoraEntrada { get; set; }
        public TimeSpan HorarioSalida { get; set; }




    }
}
