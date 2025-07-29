using HeroDotNet.Domain.Core;

namespace HeroDotNet.Application.Dto;

public sealed class ProductResponseDto
{
    public TbProdutoId Id { get; set; }
    public string? NomeProduto { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime? DataAlteracao { get; set; }
}
