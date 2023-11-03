using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductosCategorias.Models;

namespace ProductosCategorias.Controllers;

public class ProductoController : Controller{
    private readonly ILogger<ProductoController> _logger;
    private MyContext _context;

    public ProductoController(ILogger<ProductoController> logger, MyContext context){
        _logger = logger;
        _context = context;
    }

    [HttpGet("")]
    public IActionResult Index(){
        ProductoProductos MisProductos = new ProductoProductos{
            ListaProductos = _context.Productos.ToList()
        };
        return View("Index", MisProductos);
    }

    [HttpGet("detalle/producto/{ProductoId}")]
    public IActionResult DetalleProducto(int productoId){
        List<Categoria> listaCategorias = _context.Categorias.ToList();
        if(listaCategorias != null){
            Producto? ProductoConCategorias = _context.Productos.Include(a => a.Asociaciones).ThenInclude(aso => aso.Categoria).FirstOrDefault(prod => prod.ProductoId == productoId);
            HttpContext.Session.SetInt32("productoId", (Int32)productoId);
            if(ProductoConCategorias != null){
                HttpContext.Session.SetString("NombreProducto", ProductoConCategorias.Nombre);
            }            
            ProdCategorias prodCat = new ProdCategorias{
                ListaCategorias = listaCategorias,
                ProdCat = ProductoConCategorias
            };
            return View("DetalleProducto", prodCat);
        }
        ProductoProductos MisProductos = new ProductoProductos{
            ListaProductos = _context.Productos.ToList()
        };
        return View("Index", MisProductos);
    }

    // POST
    [HttpPost("procesa/producto")]
    public IActionResult ProcesaProducto(Producto producto){
        if(ModelState.IsValid){
            _context.Productos.Add(producto);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View("Index");
    }    

    [HttpPost("agregar/categoria")]
    public IActionResult AgregarCategoria(Asociacion asociacion){
        int ProductoId = asociacion.ProductoId;
        if(ModelState.IsValid){
            int count = _context.Asociaciones.Count(a => a.ProductoId == ProductoId && a.CategoriaId == asociacion.CategoriaId);
            if (count > 0){
                ModelState.AddModelError("Categoria", "La categoría ya existe");
                return Redirect($"../detalle/producto/{ProductoId}");
            }
            _context.Asociaciones.Add(asociacion);
            _context.SaveChanges();
            return Redirect($"../detalle/producto/{ProductoId}");
        }
        return View("DetalleProducto", ProductoId);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(){
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}