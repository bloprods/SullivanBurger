using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SullivanBurger.Data;
using SullivanBurger.Models;
using System.Collections.Generic;
using System.Text.Json;

namespace SullivanBurger.Controllers
{
  public class OrdersController : Controller
  {
    private readonly ApplicationDbContext _db;
    private readonly IHttpContextAccessor _context;
    private readonly ProductsController _productsController;

    public OrdersController(IHttpContextAccessor context, ApplicationDbContext db)
    {
      _db = db;
      _context = context;
      _productsController = new ProductsController(_db, _context);
    }


    public IActionResult Order()
    {
      ViewBag.Hamburguesas = _productsController.getHamburgesas();
      ViewBag.Complementos = _productsController.getComplementos();
      ViewBag.Bebidas = _productsController.getBebidas();
      ViewBag.Postres = _productsController.getPostres();
      _context.HttpContext.Session.SetString("Carrito", JsonSerializer.Serialize(_db.Productos));
      return View();
    }

    public IActionResult Carrito() {
      IEnumerable<Producto>? carrito;
      string? jsonStringCarrito = _context.HttpContext.Session.GetString("Carrito");
      if (jsonStringCarrito != null)
      {
        carrito = JsonSerializer.Deserialize<List<Producto>>(jsonStringCarrito);
        return View(carrito);
      }
      return RedirectToAction("Order");
    }
  }
}
