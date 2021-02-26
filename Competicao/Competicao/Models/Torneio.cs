using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Competicao.Models
{
    public class Torneio
    {
        public long? ID { get; set; }
        public string Nome { get; set; }
        public DateTime Criacao { get; } = DateTime.Now;
        public DateTime Modificacao { get; set; }

        public virtual ICollection<Time> Times { get; set; }
    }
}
