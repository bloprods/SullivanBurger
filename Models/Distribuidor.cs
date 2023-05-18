﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SullivanBurger.Models
{
  public class Distribuidor
  {
    [Key]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "La longitud del nombre es incorrecta")]
    public string Nombre { get; set; }
    [Required]
    [StringLength(255, MinimumLength = 5, ErrorMessage = "La dirección tiene 255 caracteres como máximo")]
    [DisplayName("Dirección")]
    public string Direccion { get; set; }
    [Required]
    [Range(600000000, 999999999, ErrorMessage = "El número de teléfono tiene que ser de 9 dígitos")]
    [DisplayName("Teléfono")]
    public int Telefono { get; set;}

    public ICollection<Producto> Productos { get; set; }
  }
}
