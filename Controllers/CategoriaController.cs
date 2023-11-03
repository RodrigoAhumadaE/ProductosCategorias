using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductosCategorias.Models;

namespace ProductosCategorias.Controllers;

public class CategoriaController : Controller{
    private readonly ILogger<CategoriaController> _logger;
    private MyContext _context;

    public CategoriaController(ILogger<CategoriaController> logger, MyContext context){
        _logger = logger;
        _context = context;
    }

    [HttpGet("categoria")]
    public IActionResult Categoria(){
        CategoriaCategorias MisCategorias = new CategoriaCategorias{
            ListaCategorias = _context.Categorias.ToList()
        };
        return View("Categoria", MisCategorias);
    }

    [HttpGet("detalle/categoria/{CategoriaId}")]
    public IActionResult DetalleCategoria(int categoriaId){
        List<Producto> listaProductos = _context.Productos.ToList();
        if(listaProductos != null){
            Categoria? CategoriaConProductos = _context.Categorias.Include(a => a.Asociaciones).ThenInclude(aso => aso.Producto).FirstOrDefault(cat => cat.CategoriaId == categoriaId);
            HttpContext.Session.SetInt32("categoriaId", (Int32)categoriaId);
            if(CategoriaConProductos != null){
                HttpContext.Session.SetString("NombreCategoria", CategoriaConProductos.Nombre);
            }
            CatProductos catProd = new CatProductos{
                ListaProductos = listaProductos,
                CatProd = CategoriaConProductos
            };
            return View("DetalleCategoria", catProd);
        }
        CategoriaCategorias MisCategorias = new CategoriaCategorias{
            ListaCategorias = _context.Categorias.ToList()
        };
        return View("Categoria", MisCategorias);
    }

    // POST

    [HttpPost("procesa/categoria")]
    public IActionResult ProcesaCategoria(Categoria categoria){
        if(ModelState.IsValid){
            _context.Categorias.Add(categoria);
            _context.SaveChanges();
            return RedirectToAction("Categoria");
        }
        return View("Categoria");
    }

    [HttpPost("agregar/producto")]
    public IActionResult AgregarProducto(Asociacion asociacion){
        int CategoriaId = asociacion.CategoriaId;
        if(ModelState.IsValid){
            int count = _context.Asociaciones.Count(a => a.CategoriaId == CategoriaId && a.ProductoId == asociacion.ProductoId);
            if (count > 0){
                ModelState.AddModelError("Producto", "El Producto ya existe");
                return Redirect($"../detalle/categoria/{CategoriaId}");
            }
            _context.Asociaciones.Add(asociacion);
            _context.SaveChanges();
            return Redirect($"../detalle/categoria/{CategoriaId}");
        }
        return View("DetalleCategoria", CategoriaId);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(){
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}