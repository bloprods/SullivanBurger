using Microsoft.AspNetCore.Mvc;
using SullivanBurger.Data;
using SullivanBurger.Models;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text;

namespace SullivanBurger.Controllers
{
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _db;
    private readonly IHttpContextAccessor _context;
    private readonly ProductsController _productsController;

    public HomeController(ILogger<HomeController> logger, IHttpContextAccessor context, ApplicationDbContext db, IWebHostEnvironment env)
    {
      _logger = logger;
      _db = db;
      _context = context;
      _productsController = new ProductsController(_db, _context, env);
    }

    public IActionResult Index()
    {
      string? jsonUsuario = _context.HttpContext.Session.GetString("Usuario");
      ViewBag.Hamburguesas = _productsController.getHamburgesas();
      ViewBag.Valoraciones = _db.ValoracionesRestaurante.Include("Usuario");
      if (jsonUsuario != null)
      {
        Usuario? usuario = JsonSerializer.Deserialize<Usuario>(jsonUsuario);
        ViewBag.Usuario = usuario;
        ViewBag.Logeado = true;
        ViewBag.yaValorado = _db.ValoracionesRestaurante.Find(usuario.Email) == null ? false : true;
      } else
      {
        ViewBag.Logeado = false;
      }
      return View();
    }

    public IActionResult About()
    {
      //Usuario userAdmin = _db.Usuarios.Find("admin1@example.com");
      //_context.HttpContext.Session.SetString("Usuario", JsonSerializer.Serialize(userAdmin));
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

    [HttpPost]
    public IActionResult Contact(Email emailData)
    {
      if (!ModelState.IsValid) {
        TempData["error"] = "Hay campos vacíos o inválidos en el formulario.";

        return View();
      }

      string Body = emailData.Message + $" \n\nMensaje enviado por {emailData.Name} con email {emailData.From}.";
      string Mail = "sullivanburger4@gmail.com";
      try
      {
        MailMessage message = new MailMessage(Mail, Mail, emailData.Subject, Body);
        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
        smtpClient.EnableSsl = true;
        smtpClient.UseDefaultCredentials = false;
        smtpClient.Credentials = new NetworkCredential("sullivanburger4@gmail.com", "augoebdtkbvnnlwc");
        smtpClient.Port = 25;
        smtpClient.Send(message);

        TempData["success"] = "Se ha enviado el email, nos pondremos en contacto cuanto antes.";
      }
      catch (Exception ex) {
        TempData["error"] = "Se ha producido un error " + ex.Message.ToString();
      }
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

    public IActionResult Repartidor()
    {
      return View(_db.Pedidos.Include("Usuario").Where(x => x.Estado == "En camino"));
    }

    public IActionResult PedidoEntregado(int id)
    {
      _db.Pedidos.Find(id).Estado = "Entregado";
      _db.SaveChanges();
      TempData["success"] = "El pedido seleccionado se ha entregado";
      return RedirectToAction("Repartidor");
    }

    [HttpPost]
    public IActionResult NuevaValoracion(ValoracionRestaurante ValoracionObj)
    {
      if (!ModelState.IsValid)
      {
        TempData["error"] = "Es necesario que introduzcas los valores correctamente al enviar la valoración.";
        return RedirectToAction("Index");
      }

      _db.ValoracionesRestaurante.Add(ValoracionObj);
      _db.SaveChanges();

      return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult ComprarPromocion(int puntos)
    {
      string? jsonUsuario = _context.HttpContext.Session.GetString("Usuario");
      Usuario? usuario;

      if (jsonUsuario != null)
      {
        usuario = JsonSerializer.Deserialize<Usuario>(jsonUsuario);
        if (usuario.Puntos >= puntos)
        {
          usuario.Puntos -= puntos;
          _db.Usuarios.Update(usuario);
          _context.HttpContext.Session.SetString("Usuario", JsonSerializer.Serialize(usuario));
          TempData["success"] = "Has realizado la compra de una promoción, será enviada a tu domicilio.";
        }
        else
        {
          TempData["error"] = "No tienes suficientes puntos para comprar la promoción.";
        }

        return RedirectToAction("Index");

      }
      TempData["error"] = "Ha ocurrido un error procesando la petición.";

      return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
      public IActionResult Error()
      {
          return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
      }
  }
}