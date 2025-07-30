using HeroDotNet.Application.Dto;
using HeroDotNet.Domain.IRepository;
using MediatR;

namespace HeroDotNet.Application.Queries.ProdutoQueries.Handlers;

public class GetProdutoByIdHandler(IProdutoRepository produtoRepository) : IRequestHandler<GetProdutoByIdQuery, ProductResponseDto?>
{
    public async Task<ProductResponseDto?> Handle(GetProdutoByIdQuery request, CancellationToken cancellationToken)
    {
        var produto = await produtoRepository.ObterProdutoPorId(request.Id);
        return produto is null ? null : new ProductResponseDto
        {
            Id = request.Id,
            NomeProduto = produto.NomeProduto,
            DataCriacao = produto.DataCriacao,
            DataAlteracao = produto.DataAlteracao
        };
    }
}
