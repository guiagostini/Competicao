using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace Competicao.Models.Infra
{
    public class RegistrarNovoUsuarioViewModel
    {
        // [Required]
        // public string UserName{get; set;}

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "A {0} precisa ter ao menos {2} e no máximo {1} caracteres de comprimento.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar senha")]
        [Compare("Senha", ErrorMessage = "Os valores informados para SENHA devem ser iguais.")]
        public string ConfirmaSenha { get; set; }

        public string FotoMimeType { get; set; }
        public byte[] Foto { get; set; }

        [NotMapped]
        public IFormFile formFile { get; set; }
    }
}