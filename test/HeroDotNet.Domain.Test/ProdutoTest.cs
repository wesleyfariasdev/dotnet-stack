using HeroDotNet.Domain.Entity;

namespace HeroDotNet.Domain.Test;

public class ProdutoTest
{
    [Fact]
    public void CriaProdutoComSucessoTest()
    {
        var produto = new Produto("Produto Teste");
        Assert.NotNull(produto.Id);
        Assert.Equal("Produto Teste", produto.NomeProduto);
        Assert.NotNull(produto.DataCriacao);
        Assert.Null(produto.DataAlteracao);
    }

    [Fact]
    public void AlteraNomeProdutoComSucessoTest()
    {
        var produto = new Produto("Produto Teste");
        produto.AlterarNomeProduto("Produto Teste Alterado");
        Assert.Equal("Produto Teste Alterado", produto.NomeProduto);
        Assert.NotNull(produto.DataAlteracao);
        Assert.True(produto.DataAlteracao > produto.DataCriacao);
    }
}
