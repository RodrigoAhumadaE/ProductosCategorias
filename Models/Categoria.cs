#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace ProductosCategorias.Models;

public class Categoria{
    [Key]
    public int CategoriaId {get;set;}

    [Required(ErrorMessage = "Debe ingresar este dato")]
    public string Nombre {get;set;}

    public DateTime FechaCreacion {get;set;} = DateTime.Now;
    public DateTime FechaActualizacion {get;set;} = DateTime.Now;

    public List<Asociacion> Asociaciones {get;set;} = new List<Asociacion>();

}