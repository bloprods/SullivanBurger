namespace SullivanBurger.Models
{
  public class OrderViewModel
  {
    public Producto Producto { get; set; }
    public string Nombre { get; set; }
    public int Cantidad { get; set; }

    public OrderViewModel() { }

    public OrderViewModel(Producto producto, int cantidad)
    {
      Producto = producto;
      Cantidad = cantidad;
    }
  }
}
