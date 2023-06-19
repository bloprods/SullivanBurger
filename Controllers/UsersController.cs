using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using SullivanBurger.Data;
using SullivanBurger.Models;

namespace SullivanBurger.Controllers
{
    public class UsersController : Controller
  {
    private readonly ApplicationDbContext _db;
    private readonly IHttpContextAccessor _context;


    public UsersController(ApplicationDbContext db, IHttpContextAccessor context)
    {
      _db = db;
      _context = context;
    }

    //GET
    public IActionResult Login()
    {
      return View();
    }

    //POST
    [HttpPost]
    public IActionResult Login(LoginViewModel loginData)
    {
      if (loginData == null || loginData.Email == null || loginData.Password == null)
      {
        ViewBag.LoginError = "Tienes que introducir todos los campos para iniciar sesión.";
      } else
      {
        var userFromDb = _db.Usuarios.Find(loginData.Email);
        if (userFromDb == null)
        {
          ViewBag.LoginError = "La cuenta introducida no existe. Por favor, vuelva a intentarlo.";
        }
        else if (Usuario.DecodeFrom64(userFromDb.Password) != loginData.Password)
        {
          ViewBag.LoginError = "Las credenciales no son correctas. Por favor, vuelva a intentarlo.";
        } else
        {
          TempData["success"] = "Has iniciado sesión correctamente";
          _context.HttpContext.Session.SetString("Usuario", JsonSerializer.Serialize(userFromDb));
          return RedirectToAction("Index", "Home");
        }
      }

      return View();
    }

    //GET
    public IActionResult SignUp()
    {
      return View();
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult SignUp(Usuario obj)
    {
      if (ModelState.IsValid)
      {
        try
        {

          obj.Password = Usuario.EncodePasswordToBase64(obj.Password);
          _db.Usuarios.Add(obj);
          _db.SaveChanges();
          TempData["success"] = "Se ha registrado correctamente";

          return RedirectToAction("Login");
        }
        catch (DbUpdateException) {
          TempData["error"] = "La cuenta introducida ya existe";

        }

      }
      return View();
    }

    //GET
    public IActionResult Logout()
    {
      _context.HttpContext.Session.Clear();
      TempData["success"] = "Has cerrado sesión correctamente";
      return RedirectToAction("Index", "Home");
    }

    //GET
    public IActionResult Management()
    {
      IEnumerable<Usuario> objUsersList = _db.Usuarios;
      return View(objUsersList);
    }

    //GET
    public IActionResult Create()
    {
      return View();
    }

    //POST
    [HttpPost]
    public IActionResult Create(Usuario obj)
    {
      if (ModelState.IsValid)
      {
        TempData["success"] = "Se ha creado el usuario correctamente.";
        _db.Usuarios.Add(obj);
        _db.SaveChanges();
        return RedirectToAction("Management");
      }

      TempData["error"] = "Se ha producido un error al crear el usuario";

      return View();
    }

    //GET
    public IActionResult Edit(string? id)
    {
      if (id == null)
      {
        return NotFound();
      }
      var userFromDb = _db.Usuarios.Find(id);
      
      if (userFromDb == null)
      {

        return NotFound();
      }

      return View(userFromDb);
    }

    //POST
    [HttpPost]
    public IActionResult Edit(Usuario obj)
    {
      if (ModelState.IsValid)
      {
        TempData["success"] = "Se ha editado el usuario correctamente";
        _db.Usuarios.Update(obj);
        _db.SaveChanges();
        return RedirectToAction("Management");
      }

      TempData["error"] = "Se ha producido un error al editar el usuario";
      return View();
    }

    //GET
    public IActionResult EditProfile(string? id)
    {
      if (id == null)
      {
        return NotFound();
      }
      var userFromDb = _db.Usuarios.Find(id);

      if (userFromDb == null)
      {

        return NotFound();
      }
      IEnumerable<Pedido> pedidos = _db.Pedidos.Where(p => p.Usuario == userFromDb).Include("Productos.Producto");
      
      ViewBag.Pedidos = pedidos.Count() > 0 ? pedidos : null;
      return View(userFromDb);
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditProfile(string id, Usuario obj)
    {
      ModelState.Remove("Email");
      ModelState.Remove("Password");
      ModelState.Remove("Rol");
      ModelState.Remove("Puntos");

      var userFromDb = _db.Usuarios.Find(id);
      userFromDb.Nombre = obj.Nombre;
      userFromDb.Apellidos = obj.Apellidos;
      userFromDb.Telefono = obj.Telefono;
      userFromDb.Direccion = obj.Direccion;

      if (ModelState.IsValid)
      {
        try
        {
          _db.Update(userFromDb);
          await _db.SaveChangesAsync();
          TempData["success"] = "Los cambios de tu perfil se han guardado";
          _context.HttpContext.Session.SetString("Usuario", JsonSerializer.Serialize(userFromDb));


        }
        catch (DbUpdateConcurrencyException)
        {
          if (!UsuarioExists(id))
          {
            return NotFound();
          }
          else
          {
            throw;
          }
        }
        return RedirectToAction("Index", "Home");
      }
      return View(obj);
    }


    // GET: Providers/Delete/5
    public async Task<IActionResult> Delete(string id)
    {
      if (id == null || _db.Usuarios == null)
      {
        return NotFound();
      }

      var usuario = await _db.Usuarios.FirstOrDefaultAsync(m => m.Email == id);
      if (usuario == null)
      {
        return NotFound();
      }

      return View(usuario);
    }

    // POST: Providers/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
      if (_db.Usuarios == null)
      {
        return Problem("Entity set 'ApplicationDbContext.Usuarios'  is null.");
      }
      var usuario = await _db.Usuarios.FindAsync(id);
      if (usuario != null)
      {
        await _db.Pedidos.Where(p => p.Email == usuario.Email).ForEachAsync(item => item.Email = null);
        _db.Usuarios.Remove(usuario);
      }

      await _db.SaveChangesAsync();
      TempData["success"] = "Se ha eliminado el usuario correctamente";

      return RedirectToAction("Management");
    }

    private bool UsuarioExists(string id)
    {
      return (_db.Usuarios?.Any(e => e.Email == id)).GetValueOrDefault();
    }
  }
}
