using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SullivanBurger.Models
{
  public class Extra
  {
    [Key]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "La longitud del nombre es incorrecta")]
    [DisplayName("Nombre")]
    public string Nombre { get; set; }
    [Required]
    public string Tipo { get; set; }
    [Required]
    public float Precio { get; set; }
  }
}
