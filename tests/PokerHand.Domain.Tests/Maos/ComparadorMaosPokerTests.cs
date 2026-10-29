using PokerHand.Domain.Cartas;
using PokerHand.Domain.Maos;

namespace PokerHand.Domain.Tests.Maos;

public sealed class ComparadorMaosPokerTests
{
    [Fact]
    public void Avaliar_DeveIndicarJogadorDoisQuandoPossuirClassificacaoMaior()
    {
        var maoJogadorUm = MaoPoker.Criar(CriarCartas("2O", "9P", "AE", "AC", "AP"));
        var maoJogadorDois = MaoPoker.Criar(CriarCartas("3O", "6O", "7O", "DO", "QO"));

        var resultado = ComparadorMaosPoker.Avaliar(maoJogadorUm, maoJogadorDois);

        Assert.Equal(ClassificacaoMao.Trinca, resultado.ClassificacaoJogadorUm);
        Assert.Equal(ClassificacaoMao.Flush, resultado.ClassificacaoJogadorDois);
        Assert.Equal(ResultadoComparacao.JogadorDois, resultado.Resultado);
    }

    [Fact]
    public void Avaliar_DeveIndicarJogadorUmQuandoPossuirClassificacaoMaior()
    {
        var maoJogadorUm = MaoPoker.Criar(CriarCartas("3O", "4C", "5E", "6P", "7E"));
        var maoJogadorDois = MaoPoker.Criar(CriarCartas("2C", "2E", "7P", "7O", "7C"));

        var resultado = ComparadorMaosPoker.Avaliar(maoJogadorUm, maoJogadorDois);

        Assert.Equal(ClassificacaoMao.Sequencia, resultado.ClassificacaoJogadorUm);
        Assert.Equal(ClassificacaoMao.Trinca, resultado.ClassificacaoJogadorDois);
        Assert.Equal(ResultadoComparacao.JogadorUm, resultado.Resultado);
    }

    [Fact]
    public void Avaliar_DeveRejeitarMaosComCartaCompartilhada()
    {
        var maoJogadorUm = MaoPoker.Criar(CriarCartas("2O", "5C", "8E", "JP", "AC"));
        var maoJogadorDois = MaoPoker.Criar(CriarCartas("2O", "4P", "7C", "DO", "KE"));

        var acao = () => ComparadorMaosPoker.Avaliar(maoJogadorUm, maoJogadorDois);

        Assert.Throws<ArgumentException>(acao);
    }

    [Theory]
    [InlineData(
        "5C 5P 6E 7P KO", "2P 3E 8E 8O DO", ResultadoComparacao.JogadorDois)]
    [InlineData(
        "5O 8P 9E JE AP", "2P 5P 7O 8E QC", ResultadoComparacao.JogadorUm)]
    [InlineData(
        "4O 6E 9C QC QP", "3O 6O 7C QO QE", ResultadoComparacao.JogadorUm)]
    [InlineData(
        "4O 4C 8E 8P KO", "4E 4P 8O 8C QO", ResultadoComparacao.JogadorUm)]
    [InlineData(
        "2C 2O 4P 4O 4E", "3P 3O 3E 9E 9O", ResultadoComparacao.JogadorUm)]
    [InlineData(
        "2O 5C 8E JP AO", "2P 5E 8C JO AC", ResultadoComparacao.Empate)]
    public void Avaliar_DeveAplicarCriteriosDeDesempate(
        string codigosJogadorUm,
        string codigosJogadorDois,
        ResultadoComparacao resultadoEsperado)
    {
        var maoJogadorUm = MaoPoker.Criar(CriarCartas(codigosJogadorUm.Split(' ')));
        var maoJogadorDois = MaoPoker.Criar(CriarCartas(codigosJogadorDois.Split(' ')));

        var resultado = ComparadorMaosPoker.Avaliar(maoJogadorUm, maoJogadorDois);

        Assert.Equal(resultadoEsperado, resultado.Resultado);
    }

    private static Carta[] CriarCartas(params string[] codigos)
    {
        return codigos.Select(Carta.Criar).ToArray();
    }
}
