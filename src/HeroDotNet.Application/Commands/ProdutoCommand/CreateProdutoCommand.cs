using HeroDotNet.Domain.Core;
using MediatR;

namespace HeroDotNet.Application.Commands.ProdutoCommand;

public record CreateProdutoCommand(string NomeProduto) : IRequest<TbProdutoId>;
