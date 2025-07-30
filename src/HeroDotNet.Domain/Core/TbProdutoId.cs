using System.Text.Json.Serialization;

namespace HeroDotNet.Domain.Core;

[JsonConverter(typeof(TbProdutoIdJsonConverter))]
public readonly struct TbProdutoId
{
    private readonly Guid _value;

    private TbProdutoId(Guid value) =>
        _value = value == Guid.Empty ? throw new ArgumentException("ProdutoId não pode ser Guid.Empty") : value;

    public static TbProdutoId New() => new TbProdutoId(Guid.NewGuid());

    public static TbProdutoId FromGuid(Guid guid) => new TbProdutoId(guid);

    public static TbProdutoId FromString(string guidString)
    {
        if (!Guid.TryParse(guidString, out var guid))
            throw new ArgumentException("Formato inválido para ProdutoId.");
        return new TbProdutoId(guid);
    }
    public static implicit operator Guid(TbProdutoId produtoId) => produtoId._value;

    public override string ToString() => _value.ToString();

    public override bool Equals(object? obj) => obj is TbProdutoId other && _value == other._value;

    public override int GetHashCode() => _value.GetHashCode();

    public static bool operator ==(TbProdutoId left, TbProdutoId right) => left.Equals(right);

    public static bool operator !=(TbProdutoId left, TbProdutoId right) => !left.Equals(right);
}