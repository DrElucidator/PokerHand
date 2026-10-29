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

        if (PossuiFlush(mao))
        {
            return ClassificacaoMao.Flush;
        }

        if (PossuiSequencia(mao))
        {
            return ClassificacaoMao.Sequencia;
        }

        return ClassificacaoMao.CartaAlta;
    }

    private static bool PossuiSequencia(MaoPoker mao)
    {
        var valoresOrdenados = mao.Cartas
            .Select(carta => (int)carta.Valor)
            .Order()
            .ToArray();

        var sequenciaComum = valoresOrdenados
            .Select((valor, indice) => valor == valoresOrdenados[0] + indice)
            .All(resultado => resultado);

        if (sequenciaComum)
        {
            return true;
        }

        return valoresOrdenados.SequenceEqual([2, 3, 4, 5, 14]);
    }

    private static bool PossuiFlush(MaoPoker mao)
    {
        return mao.Cartas
            .Select(carta => carta.Naipe)
            .Distinct()
            .Count() == 1;
    }
}
