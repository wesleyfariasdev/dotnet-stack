using HeroDotNet.Domain.Core;
using HeroDotNet.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HeroDotNet.Data.HeroContext;

public class HeroContextDb(DbContextOptions<HeroContextDb> opt) : DbContext(opt)
{
    public DbSet<Produto> Produtos { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var converterProdutoId = new ValueConverter<TbProdutoId, Guid>
                                     (v => v, v => TbProdutoId.FromGuid(v));

        modelBuilder.Entity<Produto>().ToTable("PRODUTO");
        modelBuilder.Entity<Produto>().HasKey(p => p.Id);
        modelBuilder.Entity<Produto>().Property(p => p.Id).HasConversion(converterProdutoId)
                                                          .ValueGeneratedNever()
                                                          .HasColumnName("ID");

        modelBuilder.Entity<Produto>().Property(p => p.NomeProduto).HasMaxLength(30)
                                                                   .IsRequired()
                                                                   .HasColumnName("NOME_PRODUTO");

        modelBuilder.Entity<Produto>().Property(p => p.DataCriacao).IsRequired()
                                                                   .HasColumnName("DT_CRIACAO");
        modelBuilder.Entity<Produto>().Property(p => p.DataAlteracao).HasColumnName("DT_ALTERACAO");
    }
}
