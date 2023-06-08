using System.ComponentModel.DataAnnotations;

namespace SullivanBurger.Models
{
  public class Email
  {
    [Required]
    public string From { get; set; }
    [Required]
    public string Subject { get; set; }
    [Required]
    public string Message { get; set; }
    [Required]
    public string Name { get; set; }
  }
}
