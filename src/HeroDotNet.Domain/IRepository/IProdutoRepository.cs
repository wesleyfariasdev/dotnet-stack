using HeroDotNet.Domain.Core;
using HeroDotNet.Domain.Entity;

namespace HeroDotNet.Domain.IRepository;

public interface IProdutoRepository
{
    Task AdicionarProduto(Produto produto);
    Task AdicionarProdutos(Produto[] produtos);
    Task<Produto?> ObterProdutoPorId(TbProdutoId produtoId);
    Task<Produto[]> ObterProdutosPorNome(string nomeProduto);
}
