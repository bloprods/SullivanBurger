using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace SullivanBurger.Models
{
  
  public class Pedido
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public string Estado { get; set; }
    public string Tipo { get; set; }
    public List<ProductoPedido> Productos { get; set; }
    [ForeignKey("Usuario")]
    public string? Email { get; set; }
    public Usuario Usuario { get; set; }


    public Pedido() { }

    public Pedido(DateTime Fecha, string Estado, string Tipo)
    {
      this.Fecha = Fecha;
      this.Estado = Estado;
      this.Tipo = Tipo;
    }

    public Pedido(DateTime Fecha, string Estado, string Tipo, string Email)
    {
      this.Fecha = Fecha;
      this.Estado = Estado;
      this.Tipo = Tipo;
      this.Email = Email;
    }
    
  }
}
