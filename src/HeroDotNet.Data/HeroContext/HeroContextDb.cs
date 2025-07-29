using HeroDotNet.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace HeroDotNet.Data.HeroContext;

public class HeroContextDb(DbContextOptions<HeroContextDb> opt) : DbContext(opt)
{
    public DbSet<Produto> Produtos { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Produto>().ToTable("PRODUTO");
        modelBuilder.Entity<Produto>().HasKey(p => p.Id);
        modelBuilder.Entity<Produto>().Property(p => p.NomeProduto).HasMaxLength(30).IsRequired();
        modelBuilder.Entity<Produto>().Property(p => p.DataCriacao).IsRequired();
        modelBuilder.Entity<Produto>().Property(p => p.DataAlteracao);
    }
}
