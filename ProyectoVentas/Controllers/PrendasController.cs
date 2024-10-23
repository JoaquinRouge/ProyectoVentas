using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoVentas.Context;
using ProyectoVentas.Models;

namespace ProyectoVentas.Controllers
{
    public class PrendasController : Controller
    {
        private readonly LocalVentasDatabaseContext _context;

        public PrendasController(LocalVentasDatabaseContext context)
        {
            _context = context;
        }

        // GET: Prendas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Prendas.ToListAsync());
        }

        public IActionResult Tarjetas()
        {
            var prendas = _context.Prendas;

            return View(prendas);
        }
        // GET: Prendas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prenda = await _context.Prendas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prenda == null)
            {
                return NotFound();
            }

            return View(prenda);
        }

        // GET: Prendas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Prendas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,precio,stock,talle,Descripcion,publico,tipo")] Prenda prenda)
        {
            if (ModelState.IsValid)
            {
                _context.Add(prenda);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(prenda);
        }

        // GET: Prendas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prenda = await _context.Prendas.FindAsync(id);
            if (prenda == null)
            {
                return NotFound();
            }
            return View(prenda);
        }

        // POST: Prendas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,precio,stock,talle,Descripcion,publico,tipo")] Prenda prenda)
        {
            if (id != prenda.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prenda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrendaExists(prenda.Id))
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
            return View(prenda);
        }

        // GET: Prendas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prenda = await _context.Prendas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prenda == null)
            {
                return NotFound();
            }

            return View(prenda);
        }

        // POST: Prendas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prenda = await _context.Prendas.FindAsync(id);
            if (prenda != null)
            {
                _context.Prendas.Remove(prenda);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrendaExists(int id)
        {
            return _context.Prendas.Any(e => e.Id == id);
        }
    }
}
