using Bogus;
using HeroDotNet.Domain.Entity;
namespace HeroDotNet.Domain.Test;

public class ProdutoTest
{
    private readonly Faker _faker = new("pt_BR");

    [Fact]
    [Trait("Approach", "Faker Unit Test")]
    public void Cria_Produto_Com_Sucesso_Test()
    {
        var nomeProduto = _faker.Commerce.ProductName();
        var produto = new Produto(nomeProduto);
        Assert.False(string.IsNullOrWhiteSpace(produto.Id.ToString()));
        Assert.Equal(nomeProduto, produto.NomeProduto);
        Assert.True(produto.DataCriacao < DateTime.UtcNow);
        Assert.Null(produto.DataAlteracao);
    }

    [Fact]
    [Trait("Approach", "Faker Unit Test")]
    public void Altera_Nome_Produto_Com_Sucesso_Test()
    {
        var nomeProduto = _faker.Commerce.ProductName();
        var produto = new Produto(nomeProduto);
        produto.AlterarNomeProduto("Produto Teste Alterado");
        Assert.Equal("Produto Teste Alterado", produto.NomeProduto);
        Assert.NotNull(produto.DataAlteracao);
        Assert.True(produto.DataAlteracao > produto.DataCriacao);
    }

    [Fact]
    [Trait("Approach", "Faker Unit Test")]
    public void CriarProduto_ComNomeNulo_DeveLancarExcecao_Teste() =>
        Assert.Throws<ArgumentException>(() => new Produto(null));

    [Fact]
    [Trait("Approach", "Faker Unit Test")]
    public void CriarProduto_ComNomeVazio_DeveLancarExcecao_Teste() =>
        Assert.Throws<ArgumentException>(() => new Produto(""));

    [Fact]
    [Trait("Approach", "Faker Unit Test")]
    public void DoisProdutos_ComMesmo_Nome_DevemTerIdsDiferentes_Teste()
    {
        var nomeProduto = _faker.Commerce.ProductName();
        var produto1 = new Produto(nomeProduto);
        var produto2 = new Produto(nomeProduto);

        Assert.NotEqual(produto1.Id, produto2.Id);
    }

    [Fact]
    [Trait("Approach", "Faker Unit Test")]
    public void AlterarNomeProduto_NaoDeveAlterarIdOuDataCriacao_Teste()
    {
        var nomeProduto = _faker.Commerce.ProductName();
        var produto = new Produto(nomeProduto);
        var idOriginal = produto.Id;
        var dataCriacaoOriginal = produto.DataCriacao;

        var novoNomeProduto = _faker.Commerce.ProductName();
        produto.AlterarNomeProduto(novoNomeProduto);

        Assert.Equal(idOriginal, produto.Id);
        Assert.Equal(dataCriacaoOriginal, produto.DataCriacao);
    }

    [Fact]
    [Trait("Approach", "Faker Unit Test")]
    public void AlterarNomeProduto_DeveAtualizarDataAlteracaoRecente_Teste()
    {
        var nomeProduto = _faker.Commerce.ProductName();
        var novoNomeProduto = _faker.Commerce.ProductName();
        var produto = new Produto(nomeProduto);
        produto.AlterarNomeProduto(novoNomeProduto);

        Assert.NotNull(produto.DataAlteracao);
        Assert.InRange(produto.DataAlteracao.Value,
            DateTime.UtcNow.AddSeconds(-5),
            DateTime.UtcNow.AddSeconds(5));
    }

    [Fact]
    [Trait("Approach", "Faker Unit Test")]
    public void Id_DeveSerSomenteLeitura_Teste()
    {
        var nomeProduto = _faker.Commerce.ProductName();
        var produto = new Produto(nomeProduto);
        var property = typeof(Produto).GetProperty("Id");
        Assert.False(property?.CanWrite);
    }
}
