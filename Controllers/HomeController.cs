using Microsoft.AspNetCore.Mvc;
using SullivanBurger.Data;
using SullivanBurger.Models;
using System.Diagnostics;
using System.Text.Json;

namespace SullivanBurger.Controllers
{
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _db;
    private readonly IHttpContextAccessor _context;
    private readonly ProductsController _productsController;

    public HomeController(ILogger<HomeController> logger, IHttpContextAccessor context, ApplicationDbContext db)
    {
      _logger = logger;
      _db = db;
      _context = context;
      _productsController = new ProductsController(_db, _context);
    }

    public IActionResult Index()
    {
      ViewBag.Hamburguesas = _productsController.getHamburgesas();
      Usuario userAdmin = _db.Usuarios.Find("admin1@example.com");
      _context.HttpContext.Session.SetString("Usuario", JsonSerializer.Serialize(userAdmin));
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