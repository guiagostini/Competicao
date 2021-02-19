using System.Linq;
using Competicao.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
namespace Competicao.Data.DAL
{
    public class TimeDAL
    {
        private TorneioDbContext _context;

        public TimeDAL(TorneioDbContext context)
        {
            this._context = context;
        }

        public IQueryable<Time> ListarPorNome()
        {
            return _context.Times.Include(i => i.Torneio).OrderBy(b => b.Nome);
        }

        public async Task<Time> ListarPorID(long id)
        {
            var time = await _context.Times.SingleOrDefaultAsync(m => m.ID == id);
            _context.Torneios.Where(i => time.TorneioID == i.ID).Load(); ;
            return time;
        }

        public async Task<Time> GravarTime(Time time)
        {
            if (time.ID == null)
            {
                _context.Times.Add(time);
            }
            else
            {
                _context.Update(time);
            }

            await _context.SaveChangesAsync();
            return time;
        }
        public async Task<Time> ExcluirTimePorID(long id)
        {
            Time time = await ListarPorID(id);
            _context.Times.Remove(time);
            await _context.SaveChangesAsync();
            return time;
        }

        public IQueryable<Time> ListarTimesPorTorneio(long torneioID)
        {
            var times = _context.Times.Where(t => t.TorneioID == torneioID).OrderBy(t => t.Nome);
            return times;
        }
    }
}