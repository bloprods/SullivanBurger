using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SullivanBurger.Models
{
  public class Usuario
  {
    [Key]
    [StringLength(100, MinimumLength = 10, ErrorMessage = "La longitud del email es incorrecta")]
    [DisplayName("Email")]
    public string Email { get; set; }
    [Required]
    [StringLength(32, MinimumLength = 8, ErrorMessage = "La contraseña tiene que comprender entre 8 y 32 caracteres")]
    [DisplayName("Contraseña")]
    public string Password { get; set; }
    [Required]
    [StringLength(30, ErrorMessage = "El nombre introducido supera 30 caracteres")]
    [DisplayName("Nombre")]
    public string Nombre { get; set; }
    [Required]
    [StringLength(60, ErrorMessage = "Los apellidos superan los 60 caracteres")]
    [DisplayName("Apellidos")]
    public string Apellidos { get; set; }
    [Range(600000000, 999999999, ErrorMessage = "El número de teléfono tiene que ser de 9 dígitos")]
    [DisplayName("Teléfono")]
    public int? Telefono { get; set; }
    [StringLength(255, MinimumLength = 5, ErrorMessage = "La dirección tiene 255 caracteres como máximo")]
    [DisplayName("Dirección")]
    public string? Direccion { get; set; }
    [StringLength(32, MinimumLength = 1)]
    [DisplayName("Rol")]
    public string Rol { get; set; } = "cliente";
    [DisplayName("Puntos")]
    public int Puntos { get; set; } = 0;


    public bool esAdmin()
    {
      return Rol == "admin" ? true : false;
    }
  }
}
