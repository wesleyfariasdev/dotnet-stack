using HeroDotNet.Domain.Core;
using HeroDotNet.Domain.Entity;
using HeroDotNet.Domain.IRepository;
using MediatR;

namespace HeroDotNet.Application.Commands.ProdutoCommand.Handlers;

public class CreateProdutoHandler(IProdutoRepository produtoRepository) : IRequestHandler<CreateProdutoCommand, TbProdutoId>
{
    public async Task<TbProdutoId> Handle(CreateProdutoCommand request, CancellationToken cancellationToken)
    {
        var produto = new Produto(request.NomeProduto);
        await produtoRepository.AdicionarProduto(produto);
        return produto.Id;
    }
}
