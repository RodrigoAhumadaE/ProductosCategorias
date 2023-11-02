using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProductosCategorias.Models;

namespace ProductosCategorias.Controllers;

public class CategoriaController : Controller{
    private readonly ILogger<CategoriaController> _logger;
    private MyContext _context;

    public CategoriaController(ILogger<CategoriaController> logger, MyContext context){
        _logger = logger;
        _context = context;
    }

    [HttpGet("Categoria")]
    public IActionResult Categoria(){
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
}