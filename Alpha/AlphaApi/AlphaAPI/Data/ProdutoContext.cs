using AlphaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AlphaAPI.Data;

public class ProdutoContext : DbContext
{
    public ProdutoContext(DbContextOptions<ProdutoContext> options): base(options)
    {
            
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Produto>()
            .Property(p => p.Price)
            .HasColumnType("decimal(18,2)");
    }

    public DbSet<Produto> Produtos { get; set; }
}