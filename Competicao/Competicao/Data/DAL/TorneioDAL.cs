using System.Linq;
using Competicao.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
namespace Competicao.Data.DAL
{
    public class TorneioDAL
    {
        private TorneioDbContext _context;

        public TorneioDAL(TorneioDbContext context)
        {
            this._context = context;
        }

        public IQueryable<Torneio> ListarPorNome()
        {
            return _context.Torneios.OrderBy(b => b.Nome);
        }

        public async Task<Torneio> ListarPorID(long id)
        {
            return await _context.Torneios.SingleOrDefaultAsync(d => d.ID == id);
        }

        public async Task<Torneio> GravarTorneio(Torneio torneio)
        {
            if (torneio.ID == null)
            {
                _context.Torneios.Add(torneio);
            }
            else
            {
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