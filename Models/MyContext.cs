#pragma warning disable CS8618
using Microsoft.EntityFrameworkCore;
namespace ProductosCategorias.Models;

public class MyContext : DbContext{
    public DbSet<Producto> Productos { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Asociacion> Asociaciones { get; set; }

    public MyContext(DbContextOptions options) : base(options){}
}