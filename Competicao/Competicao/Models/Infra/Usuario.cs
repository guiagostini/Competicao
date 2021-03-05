using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Competicao.Models.Infra
{
    public class Usuario : IdentityUser
    {
        public virtual ICollection<Torneio> Torneios { get; set; }
    }
}