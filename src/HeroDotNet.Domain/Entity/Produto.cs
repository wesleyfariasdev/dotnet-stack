using HeroDotNet.Domain.Core;

namespace HeroDotNet.Domain.Entity;

public sealed class Produto
{
    public Produto(string? nomeProduto)
    {
        Id = TbProdutoId.New();
        NomeProduto = nomeProduto;
        DataCriacao = DateTime.UtcNow;
    }

    public void AlterarNomeProduto(string nomeProduto)
    {
        NomeProduto = nomeProduto;
        DataAlteracao = DateTime.UtcNow;
    }

    public TbProdutoId Id { get; private set; }
    public string? NomeProduto { get; private set; }
    public DateTime DataCriacao { get; private set; }
    public DateTime? DataAlteracao { get; private set; }
}
