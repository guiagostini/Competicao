using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Competicao.Models.Infra
{
    public class Usuario : IdentityUser
    {
        public string FotoMimeType { get; set; }
        public byte[] Foto { get; set; }

        [NotMapped]
        public IFormFile formFile { get; set; }

        public virtual ICollection<Torneio> Torneios { get; set; }
    }
}