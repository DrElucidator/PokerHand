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
        var maoJogadorUm = MaoPoker.Criar(CriarCartas("3O", "4C", "5E", "6P", "7O"));
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

    private static Carta[] CriarCartas(params string[] codigos)
    {
        return codigos.Select(Carta.Criar).ToArray();
    }
}
