﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Competicao.Data;
using Competicao.Data.DAL;
using Competicao.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Competicao.Models.Infra;

namespace Competicao.Controllers
{
    [Authorize]
    public class TimeController : Controller
    {
        private readonly TorneioDbContext _context;
        private readonly TimeDAL _timeDAL;
        private readonly TorneioDAL _torneioDAL;
        private readonly UserManager<Usuario> _userManager;

        public TimeController(TorneioDbContext context, UserManager<Usuario> userManager)
        {
            this._context = context;
            this._userManager = userManager;
            _timeDAL = new TimeDAL(context);
            _torneioDAL = new TorneioDAL(context);
        }

        // GET: TimeController
        public async Task<IActionResult> Index()
        {
            var usuario = _userManager.GetUserId(User);
            return View(await _timeDAL.ListarPorNome(usuario).ToListAsync());
        }

        // GET: TimeController/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            return await ObterVisaoTimeID(id);
        }

        // GET: TimeController/Create
        public IActionResult Create()
        {
            var usuario = _userManager.GetUserId(User);
            var torneios = _torneioDAL.ListarPorNome(usuario).ToList();
            torneios.Insert(0, new Torneio() { ID = 0, Nome = "Selecione o Torneio" });
            ViewBag.Torneios = torneios;
            return View();
        }

        // POST: TimeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome, TorneioID")] Time time)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _timeDAL.GravarTime(time);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Não foi possível inserir o dado");
            }
            return View(time);
        }

        // GET: TimeController/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            var usuario = _userManager.GetUserId(User);
            ViewResult visaoTime = (ViewResult)await ObterVisaoTimeID(id);
            Time time = (Time)visaoTime.Model;

            ViewBag.Torneios = new SelectList(_torneioDAL.ListarPorNome(usuario).ToList(), "ID", "Nome", time.TorneioID);
            return visaoTime;
        }

        private async Task<IActionResult> ObterVisaoTimeID(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var time = await _timeDAL.ListarPorID((long)(id));
            if (time == null)
            {
                return NotFound();
            }

            return View(time);
        }

        // POST: Torneio /Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("Nome, ID, TorneioID")] Time time)
        {
            if (id != time.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _timeDAL.GravarTime(time);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await TimeExists(time.ID))
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

            var usuario = _userManager.GetUserId(User);
            ViewBag.Torneios = new SelectList(_torneioDAL.ListarPorNome(usuario), "ID", "Nome", time.TorneioID);

            return View(time);
        }

        // GET: Torneio /Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            return await ObterVisaoTimeID((long)id);
        }

        // POST: Torneio /Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var time = await _timeDAL.ExcluirTimePorID((long)id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> TimeExists(long? id)
        {
            return await _timeDAL.ListarPorID((long)id) != null;
        }
    }
}

