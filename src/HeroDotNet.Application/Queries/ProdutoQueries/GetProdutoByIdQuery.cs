using HeroDotNet.Application.Dto;
using HeroDotNet.Domain.Core;
using MediatR;

namespace HeroDotNet.Application.Queries.ProdutoQueries;

public record GetProdutoByIdQuery(TbProdutoId Id) : IRequest<ProductResponseDto>;
