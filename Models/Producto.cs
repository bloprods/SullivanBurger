using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SullivanBurger.Models
{
  public class Producto
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    [StringLength(50)]
    public string Nombre { get; set; }
    [Required]
    [StringLength(400)]
    public string Descripcion { get; set; }
    [Required]
    public float Precio { get; set; }
    [Required]
    public string Imagen { get; set; }
    [Required]
    public string Tipo { get; set; }
    public int Stock { get; set; } = 0;
    [Required]
    [ForeignKey("Distribuidor")]
    public int DistribuidorId { get; set; }
    public Distribuidor Distribuidor { get; set; }
  }
}
