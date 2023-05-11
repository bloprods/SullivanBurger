using Microsoft.AspNetCore.Mvc;
using SullivanBurger.Data;
using SullivanBurger.Models;

namespace SullivanBurger.Controllers
{
    public class UsersController : Controller
  {
    private readonly ApplicationDbContext _db;

    public UsersController(ApplicationDbContext db) {
      _db = db;
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
        else if (userFromDb.Password != loginData.Password)
        {
          ViewBag.LoginError = "Las credenciales no son correctas. Por favor, vuelva a intentarlo.";
        } else
        {
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

    //GET
    public IActionResult Edit(string? email)
    {
      if (email == null)
      {
        return NotFound();
      }
      var userFromDb = _db.Usuarios.Find(email);
      
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
      TempData["Management"] = "Se ha editado el usuario correctamente.";
      _db.Usuarios.Update(obj);
        _db.SaveChanges();
        return RedirectToAction("Management");
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult SignUp(Usuario obj)
    {
      if (ModelState.IsValid)
      {
        _db.Usuarios.Add(obj);
        _db.SaveChanges();
        return RedirectToAction("Login");
      }
      return View();
    }

    public IActionResult Management()
    {
      IEnumerable<Usuario> objUsersList = _db.Usuarios;
      return View(objUsersList);
    }

    //GET
    public IActionResult Delete(string? email)
    {
      if (email == null)
      {
        return NotFound();
      }
      var userFromDb = _db.Usuarios.Find(email);

      if (userFromDb == null)
      {
        return NotFound();
      } else
      {
        TempData["Management"] = "Se ha eliminado el usuario con email " + userFromDb.Email;
        _db.Usuarios.Remove(userFromDb);
        _db.SaveChanges();
      }

      return RedirectToAction("Management");
    }

  }
}
