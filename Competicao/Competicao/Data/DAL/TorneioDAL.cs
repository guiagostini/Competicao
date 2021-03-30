using System.Transactions;
using System.Linq;
using Competicao.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Competicao.Models.Infra;

namespace Competicao.Data.DAL
{
    public class TorneioDAL
    {
        private TorneioDbContext _context;

        public TorneioDAL(TorneioDbContext context)
        {
            this._context = context;
        }

        public IQueryable<Torneio> ListarPorNome(string usuario)
        {
            return _context.Torneios.Where(t => t.UsuarioID == usuario).OrderBy(b => b.Nome);
        }

        public async Task<Torneio> ListarPorID(long id)
        {
            return await _context.Torneios.SingleOrDefaultAsync(d => d.ID == id);
        }

        public async Task<Torneio> GravarTorneio(Torneio torneio)
        {
            if (torneio.ID == null)
            {
                torneio.Criacao = DateTime.Now;
                _context.Torneios.Add(torneio);
            }
            else
            {
                torneio.Modificacao = DateTime.Now;
                _context.Update(torneio);
            }

            await _context.SaveChangesAsync();
            return torneio;
        }

        public async Task<Torneio> ExcluirTorneioPorID(long id)
        {
            Torneio torneio = await ListarPorID(id);
            _context.Torneios.Remove(torneio);
            await _context.SaveChangesAsync();
            return torneio;
        }
    }
}