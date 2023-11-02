#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace ProductosCategorias.Models;

public class Producto{
    [Key]
    public int ProductoId {get;set;}

    [Required(ErrorMessage = "Debe ingresar este dato")]
    public string Nombre {get;set;}

    [Required(ErrorMessage = "Debe ingresar este dato")]
    public string Descripcion {get;set;}

    [Required(ErrorMessage = "Debe ingresar este dato")]
    public int Precio {get;set;}

    public DateTime FechaCreacion {get;set;} = DateTime.Now;
    public DateTime FechaActualizacion {get;set;} = DateTime.Now;

    public List<Asociacion> Asociaciones {get;set;} = new List<Asociacion>();
}