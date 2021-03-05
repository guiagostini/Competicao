using System.ComponentModel.DataAnnotations;
namespace Competicao.Models.Infra
{
    public class AcessarViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Display(Name = "Manter conectado")]
        public bool ManterConectado { get; set; }
    }
}