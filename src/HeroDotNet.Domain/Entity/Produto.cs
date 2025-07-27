namespace HeroDotNet.Domain.Entity;

public readonly struct ProdutoId
{
    public Guid Value { get; }

    public ProdutoId(Guid value) => Value = value;

    public static ProdutoId NewGuid() => new(Guid.NewGuid());
    public override string ToString() => Value.ToString();

    public static implicit operator Guid(ProdutoId id) => id.Value;
    public static explicit operator ProdutoId(Guid id) => new(id);
}

public sealed class Produto
{
    public Produto(string? nomeProduto)
    {
        NomeProduto = nomeProduto;
        DataCriacao = DateTime.UtcNow;
    }

    public void AlterarNomeProduto(string nomeProduto)
    {
        NomeProduto = nomeProduto;
        DataAlteracao = DateTime.UtcNow;
    }

    public ProdutoId Id { get; }
    public string? NomeProduto { get; private set; }
    public DateTime DataCriacao { get; private set; }
    public DateTime? DataAlteracao { get; private set; }
}
