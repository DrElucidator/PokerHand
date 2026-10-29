using PokerHand.Domain.Cartas;
using PokerHand.Domain.Maos;

namespace PokerHand.Domain.Tests.Maos;

public sealed class MaoPokerTests
{
    [Fact]
    public void Criar_ComCodigosSeparadosPorEspaco_DeveCriarMao()
    {
        var mao = MaoPoker.Criar("  2O   5C  8E JP  AC ");

        Assert.Equal(5, mao.Cartas.Count);
        Assert.Equal(ValorCarta.As, mao.Cartas[4].Valor);
        Assert.Equal(Naipe.Copas, mao.Cartas[1].Naipe);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("2O 5C 8E JP")]
    public void Criar_ComCodigosInvalidos_DeveLancarExcecao(string codigosDasCartas)
    {
        var acao = () => MaoPoker.Criar(codigosDasCartas);

        Assert.Throws<ArgumentException>(acao);
    }

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
        Assert.Throws<ArgumentNullException>(() => MaoPoker.Criar((IEnumerable<Carta>)null!));
    }

    private static Carta[] CriarCartas(params string[] codigos)
    {
        return codigos.Select(Carta.Criar).ToArray();
    }
}
