using System.ComponentModel.DataAnnotations;

namespace SullivanBurger.Models
{
  public class ProductoExtra
  {
    public Extra Extra { get; set; }
    public int Cantidad { get; set; }
  }
}
