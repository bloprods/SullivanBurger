using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
      IEnumerable<OrderViewModel>? carrito;
      string? jsonStringCarrito = _context.HttpContext.Session.GetString("Carrito");
      if (jsonStringCarrito != null)
      {
        carrito = JsonSerializer.Deserialize<List<OrderViewModel>>(jsonStringCarrito);
        return View(carrito);
      }
      return RedirectToAction("Order");
    }

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

    public IActionResult OrderCart()
    {
      string? jsonStringCarrito = _context.HttpContext.Session.GetString("Carrito");
      List<OrderViewModel> carrito = JsonSerializer.Deserialize<List<OrderViewModel>>(jsonStringCarrito);
      string? jsonStringUsuario = _context.HttpContext.Session.GetString("Usuario");
      Usuario usuarioLogeado = jsonStringUsuario == null ? null : JsonSerializer.Deserialize<Usuario>(jsonStringUsuario);
      Pedido pedidoNuevo;

      if (usuarioLogeado == null)
      {
        pedidoNuevo = new Pedido(DateTime.Now, "encamino", "recoger");
        _db.Pedidos.Add(pedidoNuevo);

      }
      else
      {
        pedidoNuevo = new Pedido(DateTime.Now, "encamino", "recoger", usuarioLogeado.Email);
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
      }

      _db.SaveChanges();

      _context.HttpContext.Session.Remove("Carrito");

      TempData["success"] = "Se ha realizado el pedido correctamente";

      return RedirectToAction("Order");

    }
  }

}
