using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SullivanBurger.Data;
using SullivanBurger.Models;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using System.Text.Json;
using System.Text;

namespace SullivanBurger.Controllers
{
  public class OrdersController : Controller
  {
    private readonly ApplicationDbContext _db;
    private readonly IHttpContextAccessor _context;
    private readonly ProductsController _productsController;

    public OrdersController(IHttpContextAccessor context, ApplicationDbContext db, IWebHostEnvironment env)
    {
      _db = db;
      _context = context;
      _productsController = new ProductsController(_db, _context, env);
    }

    //GET
    public IActionResult Order()
    {
      ViewBag.Hamburguesas = _productsController.getHamburgesas();
      ViewBag.Complementos = _productsController.getComplementos();
      ViewBag.Bebidas = _productsController.getBebidas();
      ViewBag.Postres = _productsController.getPostres();
      return View();
    }

    //POST
    [HttpPost]
    public IActionResult Order(OrderViewModel productData)
    {
      //Si no introduce un número en el input, por defecto se añade 1 producto al carrito
      if(productData.Cantidad < 1){
        productData.Cantidad = 1;
      }

      //Si no hay carrito
      if (_context.HttpContext.Session.GetString("Carrito") == null)
      {
        if (productData.Cantidad > _db.Productos.Find(productData.Nombre).Stock)
        {
          TempData["error"] = "No tenemos suficiente stock para este producto";
          return RedirectToAction("Order");
        }
        List<OrderViewModel> carrito = new()
        {
          new OrderViewModel(_db.Productos.Find(productData.Nombre), productData.Cantidad)
        };

        _context.HttpContext.Session.SetString("Carrito", JsonSerializer.Serialize(carrito));
      } //Si hay ya carrito
      else
      {
        List<OrderViewModel> carrito = JsonSerializer.Deserialize<List<OrderViewModel>>(_context.HttpContext.Session.GetString("Carrito"));
        Producto producto = _db.Productos.Find(productData.Nombre);
        
        //Si el producto se encuentra en el carrito
        if (EnCarrito(productData.Nombre))
        {
          OrderViewModel itemCarrito = carrito.Find(x => x.Producto.Nombre.Contains(productData.Nombre));
          
          if (productData.Cantidad + itemCarrito.Cantidad > producto.Stock)
          {
            TempData["error"] = "No tenemos suficiente stock para este producto";
            return RedirectToAction("Order");
          }
          
          itemCarrito.Cantidad += productData.Cantidad;
          carrito[carrito.IndexOf(itemCarrito)] = itemCarrito;
          
        } else
        {
          if (productData.Cantidad > producto.Stock)
          {
            TempData["error"] = "No tenemos suficiente stock para este producto";
            return RedirectToAction("Order");
          }

          carrito.Add(new OrderViewModel(_db.Productos.Find(productData.Nombre), productData.Cantidad));
        }
        _context.HttpContext.Session.SetString("Carrito", JsonSerializer.Serialize(carrito));

      }

      if (productData.Cantidad > 1)
      {
        TempData["success"] = $"Se han añadido al carrito {productData.Cantidad} unidades de {productData.Nombre}";
      }
      else if (productData.Cantidad <= 1)
      {
        TempData["success"] = $"Se ha añadido al carrito una unidad de {productData.Nombre}";
      }


        return RedirectToAction("Order");
    }
    
    private bool EnCarrito(string producto)
    {
      List<OrderViewModel> carrito = JsonSerializer.Deserialize<List<OrderViewModel>>(_context.HttpContext.Session.GetString("Carrito"));
      foreach (var item in carrito)
      {
        if (item.Producto.Nombre == producto)
        {
          return true;
        }
      }
      return false;
    }
  

  //GET
  public IActionResult Carrito() {
      if (_context.HttpContext.Session.GetString("Carrito") == null)
      {
        return View("Order");
      }
      string? jsonUsuario = _context.HttpContext.Session.GetString("Usuario");
      ViewBag.userLogeado = jsonUsuario == null ? null : JsonSerializer.Deserialize<Usuario>(jsonUsuario); 
      IEnumerable<OrderViewModel>? carrito;
      string? jsonStringCarrito = _context.HttpContext.Session.GetString("Carrito");
      if (jsonStringCarrito != null)
      {
        carrito = JsonSerializer.Deserialize<List<OrderViewModel>>(jsonStringCarrito);
        return View(carrito);
      }
      return RedirectToAction("Order");
    }

    //[HttpPost]
    //public IActionResult Carrito(string tipo)
    //{
      
    //}
    //GET
    public IActionResult DeleteCart()
    {
      _context.HttpContext.Session.Remove("Carrito");
      TempData["success"] = "Se ha eliminado tu carrito";
      return RedirectToAction("Order");
    }

    public IActionResult DeleteFromCart(string producto)
    {
      string? jsonStringCarrito = _context.HttpContext.Session.GetString("Carrito");
      List<OrderViewModel> carrito = JsonSerializer.Deserialize<List<OrderViewModel>>(jsonStringCarrito);

      carrito.RemoveAt(carrito.IndexOf(carrito.Find(x => x.Producto.Nombre == producto)));

      TempData["success"] = $"Se ha eliminado el producto {producto} de tu carrito";

      if (carrito.Count > 0) {
        _context.HttpContext.Session.SetString("Carrito", JsonSerializer.Serialize(carrito));
        return RedirectToAction("Carrito");
      } else
      {
        _context.HttpContext.Session.Remove("Carrito");
        return RedirectToAction("Order");
      }
    }

    [HttpPost]
    public IActionResult OrderCart(string tipo)
    {
      string? jsonStringCarrito = _context.HttpContext.Session.GetString("Carrito");
      List<OrderViewModel> carrito = JsonSerializer.Deserialize<List<OrderViewModel>>(jsonStringCarrito);
      string? jsonStringUsuario = _context.HttpContext.Session.GetString("Usuario");
      Usuario? usuarioLogeado = jsonStringUsuario == null ? null : JsonSerializer.Deserialize<Usuario>(jsonStringUsuario);
      Pedido pedidoNuevo;
      float precioTotal = 0;
      int puntosGanados = 0;

      if (usuarioLogeado == null)
      {
        pedidoNuevo = new Pedido(DateTime.Now, "En camino", tipo);
        _db.Pedidos.Add(pedidoNuevo);

      }
      else
      {
        pedidoNuevo = new Pedido(DateTime.Now, "En camino", tipo, usuarioLogeado.Email);
        _db.Pedidos.Add(pedidoNuevo);
      }
      _db.SaveChanges();

      foreach (var item in carrito)
      {
        Producto productoDb = _db.Productos.Find(item.Producto.Nombre);
        ProductoPedido lineaPedido = new ProductoPedido(pedidoNuevo.Id, productoDb.Nombre, item.Cantidad);
        productoDb.Stock -= item.Cantidad;
        _db.Productos.Update(productoDb);
        _db.ProductosPedidos.Add(lineaPedido);
        precioTotal += item.Cantidad * item.Producto.Precio;
      }

      if(usuarioLogeado != null)
      {
        puntosGanados = (int)(precioTotal / 100 * 90 * 0.4);
        usuarioLogeado.Puntos += puntosGanados;
        _db.Usuarios.Update(usuarioLogeado);
        _context.HttpContext.Session.SetString("Usuario", JsonSerializer.Serialize(usuarioLogeado));

        string Body = GenerarTablaEmail(pedidoNuevo);
        string Subject = $"Tu pedido está en camino";
        string Mail = "sullivanburger4@gmail.com";


        try
        {
          MailMessage message = new MailMessage(Mail, usuarioLogeado.Email, Subject, Body);
          message.IsBodyHtml = true;

          SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
          smtpClient.EnableSsl = true;
          smtpClient.UseDefaultCredentials = false;
          smtpClient.Credentials = new NetworkCredential("sullivanburger4@gmail.com", "augoebdtkbvnnlwc");
          smtpClient.Port = 25;
          smtpClient.Send(message);

        }
        catch (Exception) { }

      }

      _db.SaveChanges();


      _context.HttpContext.Session.Remove("Carrito");

      string toast = "Se ha realizado el pedido satisfactoriamente";

      TempData["success"] = usuarioLogeado != null ? toast + $". ¡Con la compra, has ganado {puntosGanados} puntos!" : toast;


      return RedirectToAction("Order");

    }

    public string GenerarTablaEmail(Pedido pedido)
    {
      Pedido? pedidoDB = _db.Pedidos.Include("Productos.Producto").FirstOrDefault(p => p.Id == pedido.Id);
      StringBuilder sb = new StringBuilder();
      float total = 0;

      sb.AppendLine("<body>");
      sb.AppendLine($"<h1>Pedido realizado el {pedido.Fecha}</h1>");
      sb.AppendLine("<table>");
      sb.AppendLine("<thead><tr><th>Producto</th><th>Cantidad</th><th>Precio</th></tr><thead>");
      foreach (ProductoPedido linea in pedido.Productos)
      {
        total += linea.Cantidad * linea.Producto.Precio;
        string imagen = "/img/products/" + string.Join("", linea.Producto.Tipo, "s") + "/" + linea.Producto.Imagen;
        sb.AppendLine("<tbody><tr>");

        sb.AppendLine($"<td>{linea.Producto.Nombre}</td>");
        sb.AppendLine($"<td>{linea.Cantidad}</td>");
        sb.AppendLine($"<td>{string.Format("{0:0.00}", linea.Producto.Precio)}€</td>");
        sb.AppendLine("</tr>");

      }
      sb.AppendLine($"<tr><td>TOTAL</td><td></td>{string.Format("{0:0.00}", total)}€<td></td><tr>");
      sb.AppendLine("</tbody></table>");
      sb.AppendLine("</body>");

      return sb.ToString();
    }

    // GET: Pedidos
    public async Task<IActionResult> Management()
    {
      var applicationDbContext = _db.Pedidos.Include(p => p.Usuario);
      return View(await applicationDbContext.ToListAsync());
    }

    // GET: Pedidos/Details/5
    public async Task<IActionResult> Details(int? id)
    {
      if (id == null || _db.Pedidos == null)
      {
        return NotFound();
      }

      var pedido = await _db.Pedidos
          .Include(p => p.Usuario)
          .FirstOrDefaultAsync(m => m.Id == id);
      if (pedido == null)
      {
        return NotFound();
      }

      return View(pedido);
    }

    // GET: Pedidos/Create
    public IActionResult Create()
    {
      ViewData["Email"] = new SelectList(_db.Usuarios, "Email", "Email");
      return View();
    }

    // POST: Pedidos/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Fecha,Estado,Tipo,Email")] Pedido pedido)
    {
      ModelState.Remove("Productos");
      ModelState.Remove("Usuario");

      if (ModelState.IsValid)
      {
        _db.Add(pedido);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Management));
      }
      ViewData["Email"] = new SelectList(_db.Usuarios, "Email", "Email", pedido.Email);
      return View(pedido);
    }

    // GET: Pedidos/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
      if (id == null || _db.Pedidos == null)
      {
        return NotFound();
      }

      var pedido = await _db.Pedidos.FindAsync(id);
      if (pedido == null)
      {
        return NotFound();
      }
      ViewData["Email"] = new SelectList(_db.Usuarios, "Email", "Email", pedido.Email);
      return View(pedido);
    }

    // POST: Pedidos/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Fecha,Estado,Tipo,Email")] Pedido pedido)
    {
      ModelState.Remove("Productos");
      ModelState.Remove("Usuario");
      if (id != pedido.Id)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          _db.Update(pedido);
          await _db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!PedidoExists(pedido.Id))
          {
            TempData["error"] = "El pedido no existe";

            return View();
          }
          else
          {
            TempData["error"] = "Ha ocurrido un error crítico con este pedido";

            return View();
          }
        }
        TempData["success"] = "Se ha editado el pedido satisfactoriamente";

        return RedirectToAction(nameof(Management));
      }
      ViewData["Email"] = new SelectList(_db.Usuarios, "Email", "Email", pedido.Email);

      return View(pedido);
    }

    // GET: Pedidos/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
      if (id == null || _db.Pedidos == null)
      {
        return NotFound();
      }

      var pedido = await _db.Pedidos
          .Include(p => p.Usuario)
          .FirstOrDefaultAsync(m => m.Id == id);
      if (pedido == null)
      {
        TempData["error"] = "El pedido no existe";
        return RedirectToAction("Management");
      }

      return View(pedido);
    }

    // POST: Pedidos/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      if (_db.Pedidos == null)
      {
        TempData["error"] = "El pedido no existe";

        return Problem("Entity set 'ApplicationDbContext.Pedidos'  is null.");
      }
      var pedido = await _db.Pedidos.FindAsync(id);
      if (pedido != null)
      {

        _db.Pedidos.Remove(pedido);
      }

      await _db.SaveChangesAsync();
      TempData["success"] = "Se ha eliminado el pedido satisfactoriamente";

      return RedirectToAction(nameof(Management));
    }

    private bool PedidoExists(int id)
    {
      return (_db.Pedidos?.Any(e => e.Id == id)).GetValueOrDefault();
    }


  }

}
