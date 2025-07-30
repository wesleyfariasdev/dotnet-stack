using HeroDotNet.Domain.Entity;

namespace HeroDotNet.Domain.Test;

public class ProdutoTest
{
    [Fact]
    public void Cria_Produto_Com_Sucesso_Test()
    {
        var produto = new Produto("Produto Teste");
        Assert.False(string.IsNullOrWhiteSpace(produto.Id.ToString()));
        Assert.Equal("Produto Teste", produto.NomeProduto);
        Assert.True(produto.DataCriacao < DateTime.UtcNow);
        Assert.Null(produto.DataAlteracao);
    }

    [Fact]
    public void Altera_Nome_Produto_Com_Sucesso_Test()
    {
        var produto = new Produto("Produto Teste");
        produto.AlterarNomeProduto("Produto Teste Alterado");
        Assert.Equal("Produto Teste Alterado", produto.NomeProduto);
        Assert.NotNull(produto.DataAlteracao);
        Assert.True(produto.DataAlteracao > produto.DataCriacao);
    }

    [Fact]
    public void CriarProduto_ComNomeNulo_DeveLancarExcecao_Teste() =>
        Assert.Throws<ArgumentException>(() => new Produto(null));

    [Fact]
    public void CriarProduto_ComNomeVazio_DeveLancarExcecao_Teste() =>
        Assert.Throws<ArgumentException>(() => new Produto(""));

    [Fact]
    public void DoisProdutos_DevemTerIdsDiferentes_Teste()
    {
        var produto1 = new Produto("Produto A");
        var produto2 = new Produto("Produto B");

        Assert.NotEqual(produto1.Id, produto2.Id);
    }

    [Fact]
    public void AlterarNomeProduto_NaoDeveAlterarIdOuDataCriacao_Teste()
    {
        var produto = new Produto("Produto A");
        var idOriginal = produto.Id;
        var dataCriacaoOriginal = produto.DataCriacao;

        produto.AlterarNomeProduto("Produto Alterado");

        Assert.Equal(idOriginal, produto.Id);
        Assert.Equal(dataCriacaoOriginal, produto.DataCriacao);
    }

    [Fact]
    public void AlterarNomeProduto_DeveAtualizarDataAlteracaoRecente_Teste()
    {
        var produto = new Produto("Produto A");
        produto.AlterarNomeProduto("Produto Alterado");

        Assert.NotNull(produto.DataAlteracao);
        Assert.InRange(produto.DataAlteracao.Value,
            DateTime.UtcNow.AddSeconds(-5),
            DateTime.UtcNow.AddSeconds(5));
    }

    [Fact]
    public void Id_DeveSerSomenteLeitura_Teste()
    {
        var produto = new Produto("Produto Teste");
        var property = typeof(Produto).GetProperty("Id");
        Assert.False(property?.CanWrite);
    }

}
