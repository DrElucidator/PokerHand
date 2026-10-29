using PokerHand.Domain.Cartas;
using PokerHand.Domain.Maos;

namespace PokerHand.Domain.Tests.Maos;

public sealed class MaoPokerTests
{
    [Fact]
    public void Criar_DeveAceitarExatamenteCincoCartasDiferentes()
    {
        var cartas = CriarCartas("2O", "5C", "8E", "JP", "AC");

        var mao = MaoPoker.Criar(cartas);

        Assert.Equal(5, mao.Cartas.Count);
        Assert.Equal(cartas, mao.Cartas);
    }

    [Theory]
    [InlineData("2O", "5C", "8E", "JP")]
    [InlineData("2O", "5C", "8E", "JP", "AC", "DO")]
    public void Criar_DeveRejeitarQuantidadeDiferenteDeCinco(params string[] codigos)
    {
        var cartas = CriarCartas(codigos);

        Assert.Throws<ArgumentException>(() => MaoPoker.Criar(cartas));
    }

    [Fact]
    public void Criar_DeveRejeitarCartaRepetida()
    {
        var cartas = CriarCartas("2O", "5C", "8E", "JP", "2O");

        Assert.Throws<ArgumentException>(() => MaoPoker.Criar(cartas));
    }

    [Fact]
    public void Criar_DeveRejeitarColecaoNula()
    {
        Assert.Throws<ArgumentNullException>(() => MaoPoker.Criar(null!));
    }

    private static Carta[] CriarCartas(params string[] codigos)
    {
        return codigos.Select(Carta.Criar).ToArray();
    }
}
