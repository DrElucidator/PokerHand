using PokerHand.Domain.Cartas;

namespace PokerHand.Domain.Maos;

public sealed class MaoPoker
{
    private MaoPoker(IReadOnlyList<Carta> cartas)
    {
        Cartas = cartas;
    }

    public IReadOnlyList<Carta> Cartas { get; }

    public static MaoPoker Criar(IEnumerable<Carta> cartas)
    {
        ArgumentNullException.ThrowIfNull(cartas);

        var cartasDaMao = cartas.ToArray();

        if (cartasDaMao.Length != 5)
        {
            throw new ArgumentException("Uma mão de poker deve possuir exatamente cinco cartas.", nameof(cartas));
        }

        if (cartasDaMao.Any(carta => carta is null))
        {
            throw new ArgumentException("Uma mão de poker não pode possuir cartas nulas.", nameof(cartas));
        }

        if (cartasDaMao.Distinct().Count() != cartasDaMao.Length)
        {
            throw new ArgumentException("Uma mão de poker não pode possuir cartas repetidas.", nameof(cartas));
        }

        return new MaoPoker(Array.AsReadOnly(cartasDaMao));
    }
}
