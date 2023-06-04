using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace SullivanBurger.Models
{
  [PrimaryKey(nameof(Id), nameof(IdPedido), nameof(NombreProducto))]
  public class ProductoPedido
  {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [ForeignKey("Pedido")]
    public int IdPedido { get; set; }
    public Pedido Pedido { get; set; }

    [ForeignKey("Producto")]
    public string NombreProducto { get; set; }
    public Producto Producto { get; set; }
    
    public int Cantidad { get; set; }
    //public List<ProductoExtra> Extras { get; set; }

    public ProductoPedido() { }
    public ProductoPedido(int IdPedido, string NombreProducto, int Cantidad)
    {
      this.IdPedido = IdPedido;
      this.NombreProducto = NombreProducto;
      this.Cantidad = Cantidad;
    }
  }
}
