using PokerHand.Domain.Cartas;
using PokerHand.Domain.Maos;

namespace PokerHand.Domain.Tests.Maos;

public sealed class AvaliadorMaoPokerTests
{
    [Fact]
    public void Classificar_DeveIdentificarCartaAlta()
    {
        var mao = MaoPoker.Criar(CriarCartas("2O", "5C", "8E", "JP", "AC"));

        var classificacao = AvaliadorMaoPoker.Classificar(mao);

        Assert.Equal(ClassificacaoMao.CartaAlta, classificacao);
    }

    [Theory]
    [InlineData("5C", "5P", "6E", "7P", "KO")]
    [InlineData("AC", "AP", "2E", "3C", "4O")]
    public void Classificar_DeveIdentificarUmPar(params string[] codigos)
    {
        var mao = MaoPoker.Criar(CriarCartas(codigos));

        var classificacao = AvaliadorMaoPoker.Classificar(mao);

        Assert.Equal(ClassificacaoMao.UmPar, classificacao);
    }

    [Theory]
    [InlineData("5C", "5P", "6E", "6O", "KO")]
    [InlineData("AC", "AP", "2E", "2C", "4O")]
    public void Classificar_DeveIdentificarDoisPares(params string[] codigos)
    {
        var mao = MaoPoker.Criar(CriarCartas(codigos));

        var classificacao = AvaliadorMaoPoker.Classificar(mao);

        Assert.Equal(ClassificacaoMao.DoisPares, classificacao);
    }

    [Theory]
    [InlineData("5C", "5P", "5E", "7P", "KO")]
    [InlineData("AC", "AP", "AE", "3C", "4O")]
    public void Classificar_DeveIdentificarTrinca(params string[] codigos)
    {
        var mao = MaoPoker.Criar(CriarCartas(codigos));

        var classificacao = AvaliadorMaoPoker.Classificar(mao);

        Assert.Equal(ClassificacaoMao.Trinca, classificacao);
    }

    [Theory]
    [InlineData("9O", "DC", "JE", "QP", "KO")]
    [InlineData("DO", "JC", "QE", "KP", "AO")]
    [InlineData("AO", "2C", "3E", "4P", "5O")]
    public void Classificar_DeveIdentificarSequencia(params string[] codigos)
    {
        var mao = MaoPoker.Criar(CriarCartas(codigos));

        var classificacao = AvaliadorMaoPoker.Classificar(mao);

        Assert.Equal(ClassificacaoMao.Sequencia, classificacao);
    }

    [Fact]
    public void Classificar_NaoDeveConsiderarSequenciaComValoresQueNaoSaoConsecutivos()
    {
        var mao = MaoPoker.Criar(CriarCartas("KO", "AC", "2E", "3P", "4O"));

        var classificacao = AvaliadorMaoPoker.Classificar(mao);

        Assert.Equal(ClassificacaoMao.CartaAlta, classificacao);
    }

    [Theory]
    [InlineData("2C", "5C", "8C", "JC", "AC")]
    [InlineData("3P", "6P", "9P", "QP", "KP")]
    public void Classificar_DeveIdentificarFlush(params string[] codigos)
    {
        var mao = MaoPoker.Criar(CriarCartas(codigos));

        var classificacao = AvaliadorMaoPoker.Classificar(mao);

        Assert.Equal(ClassificacaoMao.Flush, classificacao);
    }

    [Theory]
    [InlineData("5C", "5P", "5E", "KO", "KP")]
    [InlineData("AC", "AP", "AE", "2C", "2O")]
    public void Classificar_DeveIdentificarFullHouse(params string[] codigos)
    {
        var mao = MaoPoker.Criar(CriarCartas(codigos));

        var classificacao = AvaliadorMaoPoker.Classificar(mao);

        Assert.Equal(ClassificacaoMao.FullHouse, classificacao);
    }

    [Theory]
    [InlineData("5C", "5P", "5E", "5O", "KO")]
    [InlineData("AC", "AP", "AE", "AO", "2C")]
    public void Classificar_DeveIdentificarQuadra(params string[] codigos)
    {
        var mao = MaoPoker.Criar(CriarCartas(codigos));

        var classificacao = AvaliadorMaoPoker.Classificar(mao);

        Assert.Equal(ClassificacaoMao.Quadra, classificacao);
    }

    private static Carta[] CriarCartas(params string[] codigos)
    {
        return codigos.Select(Carta.Criar).ToArray();
    }
}
