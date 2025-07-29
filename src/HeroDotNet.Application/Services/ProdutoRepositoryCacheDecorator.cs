using HeroDotNet.Domain.Core;
using HeroDotNet.Domain.Entity;
using HeroDotNet.Domain.IRepository;
using StackExchange.Redis;
using System.Text.Json;

namespace HeroDotNet.Application.Services;

internal class ProdutoRepositoryCacheDecorator(IProdutoRepository produtoRepository,
                                               IDatabase dbCacheRedis) : IProdutoRepository
{
    public async Task AdicionarProduto(Produto produto)
    {
        await produtoRepository.AdicionarProduto(produto);
        await dbCacheRedis.KeyDeleteAsync(produto.Id.ToString());
    }

    public async Task AdicionarProdutos(Produto[] produtos)
    {
        await produtoRepository.AdicionarProdutos(produtos);
        foreach (var produto in produtos)
        {
            await dbCacheRedis.KeyDeleteAsync(produto.Id.ToString());
        }
    }

    public async Task<Produto?> ObterProdutoPorId(TbProdutoId produtoId)
    {
        var cacheProduto = await dbCacheRedis.StringGetAsync(produtoId.ToString());

        if (cacheProduto.HasValue)
            return JsonSerializer.Deserialize<Produto>(cacheProduto);

        var produto = await produtoRepository.ObterProdutoPorId(produtoId);

        if (produto is not null)
        {
            await dbCacheRedis.StringSetAsync(
                produtoId.ToString(),
                JsonSerializer.Serialize(produto),
                TimeSpan.FromMinutes(10)
            );
        }

        return produto;
    }

    public async Task<Produto[]?> ObterProdutosPorNome(string nomeProduto)
    {
        var cacheProduto = await dbCacheRedis.StringGetAsync(nomeProduto);

        if (cacheProduto.HasValue)
            return JsonSerializer.Deserialize<Produto[]?>(cacheProduto);

        var produto = await produtoRepository.ObterProdutosPorNome(nomeProduto);

        if (produto is not null)
        {
            await dbCacheRedis.StringSetAsync(
                nomeProduto,
                JsonSerializer.Serialize(produto),
                TimeSpan.FromMinutes(10)
            );
        }

        return produto;
    }
}
