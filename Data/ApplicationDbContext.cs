using Microsoft.EntityFrameworkCore;
using SullivanBurger.Models;

namespace SullivanBurger.Data
{
    public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext()
    {
            
    }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
          
    }


    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Distribuidor> Distribuidores { get; set; }
    public DbSet<Producto> Productos { get; set; }
    public DbSet<Extra> Extras { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<ProductoPedido> ProductosPedidos { get; set; }
    public DbSet<ValoracionRestaurante> ValoracionesRestaurante { get; set; }
  }
}
