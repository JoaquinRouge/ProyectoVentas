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
    public class ReposicionController : Controller
    {
        private readonly LocalVentasDatabaseContext _context;

        public ReposicionController(LocalVentasDatabaseContext context)
        {
            _context = context;
        }

        // GET: Reposicion
        public async Task<IActionResult> Index()
        {
            var localVentasDatabaseContext = _context.Reposiciones.Include(r => r.Prenda);
            return View(await localVentasDatabaseContext.ToListAsync());
        }

        // GET: Reposicion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reposicion = await _context.Reposiciones
                .Include(r => r.Prenda)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reposicion == null)
            {
                return NotFound();
            }

            return View(reposicion);
        }

        // GET: Reposicion/Create
        public IActionResult Create()
        {
            ViewData["PrendaId"] = new SelectList(_context.Prendas, "Id", "Nombre");
            return View();
        }

        // POST: Reposicion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PrendaId,Cantidad,FechaReposicion")] Reposicion reposicion)
        {
            if (ModelState.IsValid)
            {
                var prenda = _context.Prendas.Find(reposicion.PrendaId);
                if (prenda != null)
                {
                    //Suma la cantidad de prendas al stock
                    prenda.stock += reposicion.Cantidad;

                    //Actualiza la prenda en la base de datos
                    _context.Prendas.Update(prenda);
                }
                _context.Add(reposicion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PrendaId"] = new SelectList(_context.Prendas, "Id", "Nombre", reposicion.PrendaId);
            return View(reposicion);
        }

        // GET: Reposicion/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reposicion = await _context.Reposiciones.FindAsync(id);
            if (reposicion == null)
            {
                return NotFound();
            }
            ViewData["PrendaId"] = new SelectList(_context.Prendas, "Id", "Id", reposicion.PrendaId);
            return View(reposicion);
        }

        // POST: Reposicion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PrendaId,Cantidad,FechaReposicion")] Reposicion reposicion)
        {
            if (id != reposicion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Obtenemos la reposición anterior 
                    var reposicionAnt = await _context.Reposiciones
                        .AsNoTracking()  // Evita que haya dos reposiciones con el mismo Id y se utiliza una solo para consulta
                        .FirstOrDefaultAsync(r => r.Id == reposicion.Id);

                    // Si no existe la reposición anterior, devolvemos NotFound
                    if (reposicionAnt == null)
                    {
                        return NotFound();
                    }

                    var cantRepoAnte = reposicionAnt.Cantidad;
                    
                    var prenda = _context.Prendas.Find(reposicion.PrendaId);
                    if (prenda != null)
                    {
                        //Resto  la cantidad  anterior de prendas que ingrese mal al stock
                        prenda.stock -= cantRepoAnte;

                        //Suma la cantidad de prendas al stock
                        prenda.stock += reposicion.Cantidad;

                        //Actualiza la prenda en la base de datos
                        _context.Prendas.Update(prenda);
                       
                    }

                    //

                    _context.Update(reposicion);
                        await _context.SaveChangesAsync();
                    




                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReposicionExists(reposicion.Id))
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
            ViewData["PrendaId"] = new SelectList(_context.Prendas, "Id", "Id", reposicion.PrendaId);
            return View(reposicion);
        }

        // GET: Reposicion/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reposicion = await _context.Reposiciones
                .Include(r => r.Prenda)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reposicion == null)
            {
                return NotFound();
            }

            return View(reposicion);
        }

        // POST: Reposicion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reposicion = await _context.Reposiciones.FindAsync(id);
            if (reposicion != null)
            {
                _context.Reposiciones.Remove(reposicion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReposicionExists(int id)
        {
            return _context.Reposiciones.Any(e => e.Id == id);
        }
    }
}
