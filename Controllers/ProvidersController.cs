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
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _context;


        public ProvidersController(ApplicationDbContext db, IHttpContextAccessor context)
        {
          _db = db;
          _context = context;
        }

        // GET: Providers
        public async Task<IActionResult> Management()
        {
              return _db.Distribuidores != null ? 
                          View(await _db.Distribuidores.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Distribuidores'  is null.");
        }

        // GET: Providers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _db.Distribuidores == null)
            {
                return NotFound();
            }

            var distribuidor = await _db.Distribuidores
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
                _db.Add(distribuidor);
                await _db.SaveChangesAsync();
                TempData["success"] = "Se ha creado el distribuidor correctamente";
                return RedirectToAction("Management");
            }
            return View(distribuidor);
        }

        // GET: Providers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _db.Distribuidores == null)
            {
                return NotFound();
            }

            var distribuidor = await _db.Distribuidores.FindAsync(id);
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
                    _db.Update(distribuidor);
                    await _db.SaveChangesAsync();
                    TempData["success"] = "Se ha editado el distribuidor correctamente";

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
            if (id == null || _db.Distribuidores == null)
            {
                return NotFound();
            }

            var distribuidor = await _db.Distribuidores
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
            if (_db.Distribuidores == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Distribuidores'  is null.");
            }
            var distribuidor = await _db.Distribuidores.FindAsync(id);
            if (distribuidor != null)
            {
                _db.Distribuidores.Remove(distribuidor);
            }
            
            await _db.SaveChangesAsync();
            TempData["success"] = "Se ha eliminado el distribuidor correctamente";

            return RedirectToAction("Management");
        }

        private bool DistribuidorExists(string id)
        {
          return (_db.Distribuidores?.Any(e => e.Nombre == id)).GetValueOrDefault();
        }
    }
}
