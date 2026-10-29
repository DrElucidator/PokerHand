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

    private static Carta[] CriarCartas(params string[] codigos)
    {
        return codigos.Select(Carta.Criar).ToArray();
    }
}
