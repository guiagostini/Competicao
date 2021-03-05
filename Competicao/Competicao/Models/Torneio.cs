using System.Globalization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Competicao.Models.Infra;

namespace Competicao.Models
{
    public class Torneio
    {
        public long? ID { get; set; }

        [Display(Name = "Nome do Torneio")]
        [MaxLength(35, ErrorMessage = "O nome do torneio deve ter no máximo 35 caracteres")]
        [Required]
        public string Nome { get; set; }

        [Display(Name = "Data de Criação")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime Criacao { get; set; }

        [Display(Name = "Data de Modificação")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime Modificacao { get; set; }

        public virtual ICollection<Time> Times { get; set; }

        public ICollection<Time> Times { get; set; }

        public string UsuarioID { get; set; }
        public Usuario Usuario { get; set; }
    }
}
