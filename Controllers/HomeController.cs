using Microsoft.AspNetCore.Mvc;
using SullivanBurger.Data;
using SullivanBurger.Models;
using System.Diagnostics;

namespace SullivanBurger.Controllers
{
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;

    private readonly ApplicationDbContext _db;
    private readonly ProductsController _productsController;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
    {
      _logger = logger;
      _db = db;
      _productsController = new ProductsController(_db);
    }

    public IActionResult Index()
    {
      ViewBag.Hamburguesas = _productsController.getHamburgesas();
      return View();
    }

    public IActionResult About()
    {
      return View();
    }

    
    public IActionResult Products()
    {
      ViewBag.Hamburguesas = _productsController.getHamburgesas();
      ViewBag.Complementos = _productsController.getComplementos();
      ViewBag.Bebidas = _productsController.getBebidas();
      ViewBag.Postres = _productsController.getPostres();
      return View();
    }
      
    public IActionResult Contact()
    {
      return View();
    }

    public IActionResult Login()
    {
      return View();
    }
    public IActionResult SignUp()
    {
      return View();
    }

    public IActionResult UserManagement()
    {
      return View();
    }

    public IActionResult Admin() { 
      return View();
    }

  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
      public IActionResult Error()
      {
          return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
      }
  }
}