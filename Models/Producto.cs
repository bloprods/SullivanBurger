using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SullivanBurger.Models
{
  public class Producto
  {
    [Key]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "La longitud del nombre es incorrecta")]
    [DisplayName("Nombre")]
    public string Nombre { get; set; }
    [Required]
    [StringLength(400, MinimumLength = 5, ErrorMessage = "La longitud del email es incorrecta")]
    [DisplayName("Descripción")]
    public string Descripcion { get; set; }
    [Required]
    [Range(0.5, 100, ErrorMessage = "El precio ha de encontrarse entre 0.50 y 100")]
    [DisplayName("Precio")]
    public float Precio { get; set; }
    [Required]
    [StringLength(100, MinimumLength = 5, ErrorMessage = "La longitud de la imagen es incorrecta")]
    [DisplayName("Imagen")]
    public string Imagen { get; set; }
    [Required]
    public string Tipo { get; set; }
    [Required]
    public int Stock { get; set; } = 0;
    [Required]
    [ForeignKey("Distribuidor")]
    public string DistribuidorId { get; set; }
    public Distribuidor Distribuidor { get; set; }
  }
}
