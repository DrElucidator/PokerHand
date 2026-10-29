namespace PokerHand.Domain.Maos;

public static class AvaliadorMaoPoker
{
    public static ClassificacaoMao Classificar(MaoPoker mao)
    {
        ArgumentNullException.ThrowIfNull(mao);

        var gruposPorValor = mao.Cartas
            .GroupBy(carta => carta.Valor)
            .ToArray();

        if (gruposPorValor.Any(grupo => grupo.Count() == 3))
        {
            return ClassificacaoMao.Trinca;
        }

        var quantidadeDePares = gruposPorValor
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
