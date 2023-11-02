using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
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

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(){
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
