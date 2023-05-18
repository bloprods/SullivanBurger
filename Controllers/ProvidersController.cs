using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SullivanBurger.Data;
using SullivanBurger.Models;

namespace SullivanBurger.Controllers
{
    public class ProvidersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProvidersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Providers
        public async Task<IActionResult> Management()
        {
              return _context.Distribuidores != null ? 
                          View(await _context.Distribuidores.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Distribuidores'  is null.");
        }

        // GET: Providers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Distribuidores == null)
            {
                return NotFound();
            }

            var distribuidor = await _context.Distribuidores
                .FirstOrDefaultAsync(m => m.Nombre == id);
            if (distribuidor == null)
            {
                return NotFound();
            }

            return View(distribuidor);
        }

        // GET: Providers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Providers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,Direccion,Telefono")] Distribuidor distribuidor)
        {
            ModelState.Remove("Productos");

            if (ModelState.IsValid)
            {
                _context.Add(distribuidor);
                await _context.SaveChangesAsync();
                return RedirectToAction("Management");
            }
            return View(distribuidor);
        }

        // GET: Providers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Distribuidores == null)
            {
                return NotFound();
            }

            var distribuidor = await _context.Distribuidores.FindAsync(id);
            if (distribuidor == null)
            {
                return NotFound();
            }
            return View(distribuidor);
        }

        // POST: Providers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Nombre,Direccion,Telefono")] Distribuidor distribuidor)
        {
            ModelState.Remove("Productos");

            if (id != distribuidor.Nombre)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(distribuidor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DistribuidorExists(distribuidor.Nombre))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Management");
            }
            return View(distribuidor);
        }

        // GET: Providers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Distribuidores == null)
            {
                return NotFound();
            }

            var distribuidor = await _context.Distribuidores
                .FirstOrDefaultAsync(m => m.Nombre == id);
            if (distribuidor == null)
            {
                return NotFound();
            }

            return View(distribuidor);
        }

        // POST: Providers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Distribuidores == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Distribuidores'  is null.");
            }
            var distribuidor = await _context.Distribuidores.FindAsync(id);
            if (distribuidor != null)
            {
                _context.Distribuidores.Remove(distribuidor);
            }
            
            await _context.SaveChangesAsync();
                return RedirectToAction("Management");
        }

        private bool DistribuidorExists(string id)
        {
          return (_context.Distribuidores?.Any(e => e.Nombre == id)).GetValueOrDefault();
        }
    }
}
