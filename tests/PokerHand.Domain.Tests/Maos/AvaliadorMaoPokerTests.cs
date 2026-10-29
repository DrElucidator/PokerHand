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

    private static Carta[] CriarCartas(params string[] codigos)
    {
        return codigos.Select(Carta.Criar).ToArray();
    }
}
