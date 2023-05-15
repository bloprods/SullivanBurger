using Microsoft.AspNetCore.Mvc;
using SullivanBurger.Data;
using SullivanBurger.Models;

namespace SullivanBurger.Controllers
{
  public class ProductsController : Controller
  {
    private readonly ApplicationDbContext _db;

    public ProductsController(ApplicationDbContext db)
    {
      _db = db;
    }

    public IActionResult Index()
    {
      return View();
    }

    public IEnumerable<Producto> getHamburgesas()
    {
      return _db.Productos.Where(p => p.Tipo == "hamburguesa");
    }

    public IEnumerable<Producto> getComplementos()
    {
      return _db.Productos.Where(p => p.Tipo == "complemento");
    }

    public IEnumerable<Producto> getBebidas()
    {
      return _db.Productos.Where(p => p.Tipo == "bebida");
    }

    public IEnumerable<Producto> getPostres()
    {
      return _db.Productos.Where(p => p.Tipo == "postre");
    }
  }
}
