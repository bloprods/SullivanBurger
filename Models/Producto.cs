using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SullivanBurger.Models
{
  public class Producto
  {
    [Key]
    [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Nombre se encuentra vacío")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "La longitud del nombre es incorrecta")]
    [DisplayName("Nombre")]
    public string Nombre { get; set; }
    [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Descripción se encuentra vacío")]
    [StringLength(400, MinimumLength = 5, ErrorMessage = "La longitud del email es incorrecta")]
    [DisplayName("Descripción")]
    public string Descripcion { get; set; }
    [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Precio se encuentra vacío")]
    [Range(0.5, 100, ErrorMessage = "El precio ha de encontrarse entre 0.50 y 100")]
    [DisplayName("Precio")]
    public float Precio { get; set; }
    [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Imagen se encuentra vacío")]
    [StringLength(100, MinimumLength = 5, ErrorMessage = "La longitud de la imagen es incorrecta")]
    [DisplayName("Imagen")]
    public string Imagen { get; set; }
    [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Tipo se encuentra vacío")]
    public string Tipo { get; set; }
    [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Stock se encuentra vacío")]
    [Range(0, 200, ErrorMessage = "El stock ha de encontrarse entre 0 y 200")]
    public int Stock { get; set; } = 0;
    [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Distribuidor se encuentra vacío")]
    [ForeignKey("Distribuidor")]
    public string DistribuidorId { get; set; }
    public Distribuidor Distribuidor { get; set; }
  }
}
