#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace ProductosCategorias.Models;

public class Asociacion{
    [Key]
    public int AsociacionId {get;set;}

    public int ProductoId {get;set;}

    public int CategoriaId {get;set;}

    public Producto? Producto {get;set;}

    public Categoria? Categoria {get;set;}
}