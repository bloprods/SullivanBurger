using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SullivanBurger.Models
{
    public class Usuarionovno
    {
        [Key]
        [Range(10, 100, ErrorMessage = "La longitud del email es incorrecta")]
        [DisplayName("Email")]
        public string Email { get; set; }
        [Required]
        [Range(8, 32, ErrorMessage = "La contraseña tiene que comprender entre 8 y 32 caracteres")]
        [DisplayName("Contraseña")]
        public string Password { get; set; }
        [Required]
        [Range(10, 30, ErrorMessage = "El nombre introducido supera 30 caracteres")]
        [DisplayName("Nombre")]
        public string Nombre { get; set; }
        [Required]
        [Range(10, 60, ErrorMessage = "Los apellidos superan los 60 caracteres")]
        [DisplayName("Apellidos")]
        public string Apellidos { get; set; }
        [Range(9, 9, ErrorMessage = "El número de teléfono tiene que ser de 9 dígitos")]
        [DisplayName("Teléfono")]
        public int? Telefono { get; set; }
        [Range(5, 255, ErrorMessage = "La dirección tiene 255 caracteres como máximo")]
        [DisplayName("Dirección")]
        public string? Direccion { get; set; }
        [Range(1, 32)]
        [DisplayName("Rol")]
        public string Rol { get; set; } = "cliente";
        [DisplayName("Puntos")]
        public int Puntos { get; set; } = 0;

    }
}
