namespace PokerHand.Domain.Cartas;

public sealed record Carta(ValorCarta Valor, Naipe Naipe)
{
    public static Carta Criar(string codigo)
    {
        ArgumentNullException.ThrowIfNull(codigo);

        if (codigo.Length != 2)
        {
            throw new ArgumentException("Uma carta deve possuir exatamente dois caracteres.", nameof(codigo));
        }

        var valor = InterpretarValor(codigo[0]);
        var naipe = InterpretarNaipe(codigo[1]);

        return new Carta(valor, naipe);
    }

    private static ValorCarta InterpretarValor(char simbolo)
    {
        return char.ToUpperInvariant(simbolo) switch
        {
            '2' => ValorCarta.Dois,
            '3' => ValorCarta.Tres,
            '4' => ValorCarta.Quatro,
            '5' => ValorCarta.Cinco,
            '6' => ValorCarta.Seis,
            '7' => ValorCarta.Sete,
            '8' => ValorCarta.Oito,
            '9' => ValorCarta.Nove,
            'D' => ValorCarta.Dez,
            'J' => ValorCarta.Valete,
            'Q' => ValorCarta.Dama,
            'K' => ValorCarta.Rei,
            'A' => ValorCarta.As,
            _ => throw new ArgumentException("O valor da carta é inválido.", nameof(simbolo))
        };
    }

    private static Naipe InterpretarNaipe(char simbolo)
    {
        return char.ToUpperInvariant(simbolo) switch
        {
            'O' => Naipe.Ouro,
            'C' => Naipe.Copas,
            'E' => Naipe.Espadas,
            'P' => Naipe.Paus,
            _ => throw new ArgumentException("O naipe da carta é inválido.", nameof(simbolo))
        };
    }
}
