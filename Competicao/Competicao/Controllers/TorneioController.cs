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
using System.Security.Principal;
using Microsoft.AspNetCore.Identity;
using Competicao.Models.Infra;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Competicao.Controllers
{
    [Authorize]
    public class TorneioController : Controller
    {
        private readonly TorneioDbContext _context;
        private readonly TorneioDAL _torneioDAL;
        private readonly TimeDAL _timeDAL;
        private readonly UserManager<Usuario> _userManager;

        public TorneioController(TorneioDbContext context, UserManager<Usuario> userManager)
        {
            this._context = context;
            this._userManager = userManager;
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            _torneioDAL = new TorneioDAL(context);
            _timeDAL = new TimeDAL(context);
        }

        // GET: Torneio
        public async Task<IActionResult> Index()
        {
            var usuario = _userManager.GetUserId(User);
            return View(await _torneioDAL.ListarPorNome(usuario).ToListAsync());
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
            var resul = _timeDAL.ListarTimesPorTorneio((long)id);
            ViewBag.Data = resul;

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
        public async Task<IActionResult> Create([Bind("Nome, TipoTorneio")] Torneio torneio, List<string> Timestags)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var time = new Time();
                    torneio.UsuarioID = _userManager.GetUserId(User);
                    await _torneioDAL.GravarTorneio(torneio);

                    time.ID = null;
                    time.TorneioID = torneio.ID;
                    time.Nome = Timestags;
                    await _timeDAL.GravarTime(time);

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
            var resul = _timeDAL.ListarTimesPorTorneio((long)id);
            ViewBag.Data = resul;

            return await ObterVisaoTorneioID((long)id);
        }

        // POST: Torneio /Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("ID,Nome,Criacao")] Torneio torneio, List<string> Timestags)
        {
            if (id != torneio.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    torneio.UsuarioID = _userManager.GetUserId(User);
                    await _torneioDAL.GravarTorneio(torneio);


                    var time = _context.Times.FirstOrDefault(i => i.TorneioID == id);
                    time.Nome = Timestags;
                    await _timeDAL.GravarTime(time);

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
