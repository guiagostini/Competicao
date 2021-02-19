using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Competicao.Models
{
    public class Time
    {
        public long? ID { get; set; }
        public string Nome { get; set; }

        public Torneio Torneio { get; set; }
        public long? TorneioID { get; set; }
    }
}
