using HeroDotNet.Data.HeroContext;
using HeroDotNet.Domain.Core;
using HeroDotNet.Domain.Entity;
using HeroDotNet.Domain.IRepository;
using Microsoft.EntityFrameworkCore;

namespace HeroDotNet.Data.Repository;

internal class ProdutoRepository(HeroContextDb context) : IProdutoRepository
{
    public async Task AdicionarProduto(Produto produto)
    {
        if (produto == null)
            throw new ArgumentNullException(nameof(produto), "Produto cannot be null.");

        await context.Produtos.AddAsync(produto);
        await context.SaveChangesAsync();
    }

    public async Task AdicionarProdutos(Produto[] produtos)
    {
        if (!produtos.Any())
            throw new Exception("No products to add. The array is empty.");

        await context.Produtos.AddRangeAsync(produtos);
        await context.SaveChangesAsync();
    }

    public async Task<Produto?> ObterProdutoPorId(TbProdutoId produtoId) =>
           await context.Produtos.FirstOrDefaultAsync(p => p.Id == produtoId);

    public async Task<Produto[]> ObterProdutosPorNome(string nomeProduto) =>
           await context.Produtos.Where(p => p.NomeProduto == nomeProduto).ToArrayAsync();
}
