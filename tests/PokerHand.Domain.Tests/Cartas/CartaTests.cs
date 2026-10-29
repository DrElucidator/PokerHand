using PokerHand.Domain.Cartas;

namespace PokerHand.Domain.Tests.Cartas;

public sealed class CartaTests
{
    [Theory]
    [InlineData("2O", ValorCarta.Dois, Naipe.Ouro)]
    [InlineData("9C", ValorCarta.Nove, Naipe.Copas)]
    [InlineData("DE", ValorCarta.Dez, Naipe.Espadas)]
    [InlineData("JP", ValorCarta.Valete, Naipe.Paus)]
    [InlineData("QO", ValorCarta.Dama, Naipe.Ouro)]
    [InlineData("KE", ValorCarta.Rei, Naipe.Espadas)]
    [InlineData("AC", ValorCarta.As, Naipe.Copas)]
    public void Criar_DeveInterpretarCodigoValido(string codigo, ValorCarta valor, Naipe naipe)
    {
        var carta = Carta.Criar(codigo);

        Assert.Equal(valor, carta.Valor);
        Assert.Equal(naipe, carta.Naipe);
    }

    [Theory]
    [InlineData("")]
    [InlineData("A")]
    [InlineData("ACO")]
    [InlineData("1C")]
    [InlineData("10O")]
    [InlineData("7S")]
    [InlineData("XP")]
    public void Criar_DeveRejeitarCodigoInvalido(string codigo)
    {
        Assert.ThrowsAny<ArgumentException>(() => Carta.Criar(codigo));
    }

    [Fact]
    public void Criar_DeveRejeitarCodigoNulo()
    {
        Assert.Throws<ArgumentNullException>(() => Carta.Criar(null!));
    }
}
