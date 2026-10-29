namespace PokerHand.Domain.Maos;

public static class AvaliadorMaoPoker
{
    public static ClassificacaoMao Classificar(MaoPoker mao)
    {
        ArgumentNullException.ThrowIfNull(mao);

        var quantidadeDePares = mao.Cartas
            .GroupBy(carta => carta.Valor)
            .Count(grupo => grupo.Count() == 2);

        if (quantidadeDePares == 2)
        {
            return ClassificacaoMao.DoisPares;
        }

        if (quantidadeDePares == 1)
        {
            return ClassificacaoMao.UmPar;
        }

        return ClassificacaoMao.CartaAlta;
    }
}
