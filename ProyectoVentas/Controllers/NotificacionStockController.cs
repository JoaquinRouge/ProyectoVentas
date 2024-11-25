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
    public class NotificacionStockController : Controller
    {
        private readonly LocalVentasDatabaseContext _context;

        public NotificacionStockController(LocalVentasDatabaseContext context)
        {
            _context = context;
        }

        // GET: NotificacionStock
        public async Task<IActionResult> Index()
        {

           //Guardo en la variable listaPrendas las prendas que estan por debajo de su stockMin

            var listaPrendas = _context.Prendas
                     .Where(p => p.stock < p.StockMinimo)
                     .ToList(); 

            //Recorre la listaPrendas para guardar en la var notificacionStock cada una de las 
            //prendas de la listaPrendas

            foreach (var prenda in listaPrendas)
            {
                var notificacionStock = new NotificacionStock
                {
                    PrendaId = prenda.Id,
                    Nombre = prenda.Nombre,
                    StockActual = prenda.stock,
                    StockMinimo = prenda.StockMinimo

                };

                //Consulto si ya existe si la prenda ya existe en la tabla de NotificacionesStock
                //para evitar duplicado cuando se hace click en Notificaciones(index)

               // var existeNotificacionStock = _context.NotificacionesStock
                 //    .Any(n => n.PrendaId == notificacionStock.PrendaId);

                var existeNotificacionStock = _context.NotificacionesStock
                 .FirstOrDefault(n => n.PrendaId == notificacionStock.PrendaId);


                //Si la prenda no existe en la tabla NotificacionesStock,
                //la agrega a la misma
                if (existeNotificacionStock == null)
                {
                    _context.NotificacionesStock.Add(notificacionStock);
                }
                else {
                    existeNotificacionStock.StockActual = notificacionStock.StockActual;
                }

            }
            //Guarda los cambios de la tabla NotificacionesStock
            await _context.SaveChangesAsync();

            //Guarda en la var todas las notificaciones de stock que se generaron y se
            // retornan a la vista(index)
            var localVentasDatabaseContext = _context.NotificacionesStock.Include(n => n.Prenda);
            return View(await localVentasDatabaseContext.ToListAsync());
        }

        // GET: NotificacionStock/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notificacionStock = await _context.NotificacionesStock
                .Include(n => n.Prenda)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (notificacionStock == null)
            {
                return NotFound();
            }

            return View(notificacionStock);
        }

        // GET: NotificacionStock/Create
        public IActionResult Create()
        {
            ViewData["PrendaId"] = new SelectList(_context.Prendas, "Id", "Id");
            return View();
        }

        // POST: NotificacionStock/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PrendaId,Nombre,StockActual,StockMinimo")] NotificacionStock notificacionStock)
        {
            if (ModelState.IsValid)
            {
                _context.Add(notificacionStock);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PrendaId"] = new SelectList(_context.Prendas, "Id", "Id", notificacionStock.PrendaId);
            return View(notificacionStock);
        }

        // GET: NotificacionStock/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notificacionStock = await _context.NotificacionesStock.FindAsync(id);
            if (notificacionStock == null)
            {
                return NotFound();
            }
            ViewData["PrendaId"] = new SelectList(_context.Prendas, "Id", "Id", notificacionStock.PrendaId);
            return View(notificacionStock);
        }

        // POST: NotificacionStock/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PrendaId,Nombre,StockActual,StockMinimo")] NotificacionStock notificacionStock)
        {
            if (id != notificacionStock.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(notificacionStock);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotificacionStockExists(notificacionStock.Id))
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
            ViewData["PrendaId"] = new SelectList(_context.Prendas, "Id", "Id", notificacionStock.PrendaId);
            return View(notificacionStock);
        }

        // GET: NotificacionStock/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notificacionStock = await _context.NotificacionesStock
                .Include(n => n.Prenda)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (notificacionStock == null)
            {
                return NotFound();
            }

            return View(notificacionStock);
        }

        // POST: NotificacionStock/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var notificacionStock = await _context.NotificacionesStock.FindAsync(id);
            if (notificacionStock != null)
            {
                _context.NotificacionesStock.Remove(notificacionStock);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotificacionStockExists(int id)
        {
            return _context.NotificacionesStock.Any(e => e.Id == id);
        }
    }
}
