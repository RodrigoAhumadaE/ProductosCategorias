#pragma warning disable CS8618
namespace ProductosCategorias.Models;

public class ProdCategorias{

    public Producto? ProdCat  {get; set;}
    public List<Categoria> ListaCategorias {get; set;}
}