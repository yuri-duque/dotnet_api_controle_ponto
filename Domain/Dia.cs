using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Dia
    {
        public long Id { get; set; }
        public string DiaSemana { get; set; }
        public DateTime Data { get; set; }

        public IList<Registro> Registros { get; set; }
    }
}
