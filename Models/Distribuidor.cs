using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SullivanBurger.Models
{
  public class Distribuidor
  {
    [Key]
    [Required]
    [StringLength(50)]
    public string Nombre { get; set; }
    [Required]
    [StringLength(50)]
    public string Direccion { get; set; }
    [Required]
    [StringLength(9)]
    public int Telefono { get; set;}

    public ICollection<Producto> Productos { get; set; }
  }
}
