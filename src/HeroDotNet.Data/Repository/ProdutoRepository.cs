using HeroDotNet.Data.HeroContext;
using HeroDotNet.Domain.Core;
using HeroDotNet.Domain.Entity;
using HeroDotNet.Domain.IRepository;
using Microsoft.EntityFrameworkCore;

namespace HeroDotNet.Data.Repository;

public class ProdutoRepository(HeroContextDb context) : IProdutoRepository
{
    public async Task AdicionarProduto(Produto produto)
    {
        if (produto == null)
            throw new ArgumentNullException(nameof(produto), "Produto cannot be null.");

        await context.Produtos.AddAsync(produto);
    }

    public async Task AdicionarProdutos(Produto[] produtos)
    {
        if (!produtos.Any())
            throw new Exception("No products to add. The array is empty.");

        await context.Produtos.AddRangeAsync(produtos);
    }

    public async Task<Produto?> ObterProdutoPorId(TbProdutoId produtoId) =>
           await context.Produtos.FirstOrDefaultAsync(p => p.Id == produtoId);

    public async Task<Produto[]> ObterProdutosPorNome(string nomeProduto) =>
           await context.Produtos.Where(p => p.NomeProduto == nomeProduto).ToArrayAsync();
}
