using AlphaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AlphaAPI.Data;

public class ProdutoContext : DbContext
{
    public ProdutoContext(DbContextOptions<ProdutoContext> options): base(options)
    {
            
    }

    public DbSet<Produto> Produtos { get; set; }
}