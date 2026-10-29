namespace PokerHand.Domain.Maos;

public static class AvaliadorMaoPoker
{
    public static ClassificacaoMao Classificar(MaoPoker mao)
    {
        ArgumentNullException.ThrowIfNull(mao);

        var possuiPar = mao.Cartas
            .GroupBy(carta => carta.Valor)
            .Any(grupo => grupo.Count() == 2);

        if (possuiPar)
        {
            return ClassificacaoMao.UmPar;
        }

        return ClassificacaoMao.CartaAlta;
    }
}
