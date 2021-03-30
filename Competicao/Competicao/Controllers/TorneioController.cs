using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Competicao.Data;
using Competicao.Data.DAL;
using Competicao.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Competicao.Controllers
{
    [Authorize]
    public class TorneioController : Controller
    {
        private readonly TorneioDbContext _context;
        private readonly TorneioDAL _torneioDAL;

        public TorneioController(TorneioDbContext context)
        {
            this._context = context;
            _torneioDAL = new TorneioDAL(context);
        }

        // GET: Torneio
        public async Task<IActionResult> Index()
        {
            return View(await _torneioDAL.ListarPorNome().ToListAsync());
        }

        private async Task<IActionResult> ObterVisaoTorneioID(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var torneios = await _torneioDAL.ListarPorID((long)(id));
            if (torneios == null)
            {
                return NotFound();
            }

            return View(torneios);
        }

        // GET: Torneio /Details/5
        public async Task<IActionResult> Details(long? id)
        {
            return await ObterVisaoTorneioID(id);
        }

        // GET: Torneio /Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Torneio /Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome")] Torneio torneio)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _torneioDAL.GravarTorneio(torneio);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Não foi possível inserir o dado");
            }
            return View(torneio);
        }

        // GET: Torneio /Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            return await ObterVisaoTorneioID((long)id);
        }

        // POST: Torneio /Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("Nome, ID")] Torneio torneio)
        {
            if (id != torneio.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _torneioDAL.GravarTorneio(torneio);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await TorneioExists(torneio.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(torneio);
        }

        // GET: Torneio /Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            return await ObterVisaoTorneioID((long)id);
        }

        // POST: Torneio /Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var torneio = await _torneioDAL.ExcluirTorneioPorID((long)id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> TorneioExists(long? id)
        {
            return await _torneioDAL.ListarPorID((long)id) != null;
        }
    }
}
