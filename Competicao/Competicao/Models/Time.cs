using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Competicao.Models
{
    public class Time
    {
        public long? ID { get; set; }

        [Display(Name = "Nome do Time")]
        [Required]
        public IList<string> Nome { get; set; }

        public Torneio Torneio { get; set; }
        public long? TorneioID { get; set; }
    }
}
