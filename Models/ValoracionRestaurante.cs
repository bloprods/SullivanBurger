using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SullivanBurger.Models
{
  public class ValoracionRestaurante
  {
    [Key]
    [ForeignKey("Usuario")]
    public string? Email { get; set; }
    public Usuario? Usuario { get; set;}
    [Required]
    [Range(0,5,ErrorMessage = "La valoración comprende entre los números 0 y 5")]
    public double Valoracion { get; set;}
    [Required]
    [StringLength(255, ErrorMessage = "El máximo de caracteres es de 255")]
    public string? Comentario { get; set;}
  }
}
