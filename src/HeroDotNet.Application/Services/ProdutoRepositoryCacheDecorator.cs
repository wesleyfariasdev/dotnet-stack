using HeroDotNet.Application.Services.IServices;
using HeroDotNet.Domain.Core;
using HeroDotNet.Domain.Entity;
using HeroDotNet.Domain.IRepository;

namespace HeroDotNet.Application.Services;

internal class ProdutoRepositoryCacheDecorator(IProdutoRepository produtoRepository) : IProdutoServices
{
    public Task AdicionarProduto(Produto produto)
    {
        throw new NotImplementedException();
    }

    public Task AdicionarProdutos(Produto[] produtos)
    {
        throw new NotImplementedException();
    }

    public Task<Produto?> ObterProdutoPorId(TbProdutoId produtoId)
    {
        throw new NotImplementedException();
    }

    public Task<Produto[]> ObterProdutosPorNome(string nomeProduto)
    {
        throw new NotImplementedException();
    }
}
