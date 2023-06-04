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
    public class ProductsController : Controller
    {
      private readonly ApplicationDbContext _db;
      private readonly IHttpContextAccessor _context;


    public ProductsController(ApplicationDbContext db, IHttpContextAccessor context)
    {
      _db = db;
      _context = context;
    }

    // GET: Products
    public async Task<IActionResult> Management()
      {
          var applicationDbContext = _db.Productos.Include(p => p.Distribuidor);
          return View(await applicationDbContext.ToListAsync());
      }

      private bool ProductoExists(string id)
      {
        return (_db.Productos?.Any(e => e.Nombre == id)).GetValueOrDefault();
      }

      public IEnumerable<Producto> getHamburgesas()
      {
        return _db.Productos.Where(p => p.Tipo == "hamburguesa" && p.Stock > 0);
      }

      public IEnumerable<Producto> getComplementos()
      {
        return _db.Productos.Where(p => p.Tipo == "complemento" && p.Stock > 0);
      }

      public IEnumerable<Producto> getBebidas()
      {
        return _db.Productos.Where(p => p.Tipo == "bebida" && p.Stock > 0);
      }

      public IEnumerable<Producto> getPostres()
      {
        return _db.Productos.Where(p => p.Tipo == "postre" && p.Stock > 0);
      }

      // GET: Products/Create
      public IActionResult Create()
      {
        ViewData["DistribuidorId"] = new SelectList(_db.Distribuidores, "Nombre", "Nombre");
        return View();
      }

      // POST: Products/Create
      // To protect from overposting attacks, enable the specific properties you want to bind to.
      // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Create([Bind("Nombre,Descripcion,Precio,Imagen,Tipo,Stock,DistribuidorId")] Producto producto)
      {
      ModelState.Remove("Distribuidor");
      if (ModelState.IsValid)
        {
          _db.Add(producto);
          await _db.SaveChangesAsync();
          TempData["success"] = "Se ha creado el producto correctamente";
          return RedirectToAction("Management");
        }
        TempData["error"] = "Se ha producido un error al crear el producto";
        ViewData["DistribuidorId"] = new SelectList(_db.Distribuidores, "Nombre", "Nombre", producto.DistribuidorId);
        return View(producto);
      }

      // GET: Products/Edit/5
      public async Task<IActionResult> Edit(string id)
      {
          if (id == null || _db.Productos == null)
          {
              return NotFound();
          }

          var producto = await _db.Productos.FindAsync(id);
          if (producto == null)
          {
              return NotFound();
          }
          ViewData["DistribuidorId"] = new SelectList(_db.Distribuidores, "Nombre", "Nombre", producto.DistribuidorId);
          return View(producto);
      }

      // POST: Products/Edit/5
      // To protect from overposting attacks, enable the specific properties you want to bind to.
      // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Edit(string id, [Bind("Nombre,Descripcion,Precio,Imagen,Tipo,Stock,DistribuidorId")] Producto producto)
      {
        ModelState.Remove("Distribuidor");
        if (id != producto.Nombre)
        {
        TempData["error"] = "Se ha producido un error al editar el producto";
        return View();
        }

        if (ModelState.IsValid)
        {
          try
          {
            _db.Update(producto);
            await _db.SaveChangesAsync();
            TempData["success"] = "Se ha editado el producto correctamente";
        }
        catch (DbUpdateConcurrencyException)
          {
            if (!ProductoExists(producto.Nombre))
            {
            TempData["error"] = "No se ha encontrado el producto con ese nombre";
            return View();
            }
            else
            {
              throw;
            }
          }
          return RedirectToAction("Management");
        }

        TempData["error"] = "Se ha producido un error al editar el producto";
        ViewData["DistribuidorId"] = new SelectList(_db.Distribuidores, "Nombre", "Nombre", producto.DistribuidorId);
        return View(producto);
      }

      // GET: Products/Delete/5
      public async Task<IActionResult> Delete(string id)
        {
          if (id == null || _db.Productos == null)
          {
              return NotFound();
          }

          var producto = await _db.Productos
              .Include(p => p.Distribuidor)
              .FirstOrDefaultAsync(m => m.Nombre == id);
          if (producto == null)
          {
              return NotFound();
          }

          return View(producto);
      }

      // POST: Products/Delete/5
      [HttpPost, ActionName("Delete")]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> DeleteConfirmed(string id)
      {
          if (_db.Productos == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Productos'  is null.");
          }
          var producto = await _db.Productos.FindAsync(id);
          if (producto != null)
          {
              _db.Productos.Remove(producto);
          }
          
          await _db.SaveChangesAsync();
          TempData["success"] = "Se ha eliminado el producto correctamente";

          return RedirectToAction("Management");
      }

        


  }
}
