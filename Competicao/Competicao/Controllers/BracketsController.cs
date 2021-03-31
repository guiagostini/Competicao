using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Competicao.Controllers
{
    public class BracketsController : Controller
    {
        // GET: Brackets
        public IActionResult Index()
        {
            return View();
        }

        // GET: Brackets/Details/5
        public IActionResult Details(int id)
        {
            return View();
        }

        // GET: Brackets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Brackets/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Brackets/Edit/5
        public IActionResult Edit(int id)
        {
            return View();
        }

        // POST: Brackets/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Brackets/Delete/5
        public IActionResult Delete(int id)
        {
            return View();
        }

        // POST: Brackets/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}