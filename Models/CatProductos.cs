#pragma warning disable CS8618
namespace ProductosCategorias.Models;

public class CatProductos{

    public Categoria? CatProd {get;set;}
    public List<Producto> ListaProductos {get;set;}
}