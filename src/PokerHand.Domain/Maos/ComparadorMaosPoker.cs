namespace PokerHand.Domain.Maos;

public static class ComparadorMaosPoker
{
    public static ResultadoPartidaPoker Avaliar(MaoPoker maoJogadorUm, MaoPoker maoJogadorDois)
    {
        ArgumentNullException.ThrowIfNull(maoJogadorUm);
        ArgumentNullException.ThrowIfNull(maoJogadorDois);

        if (maoJogadorUm.Cartas.Intersect(maoJogadorDois.Cartas).Any())
        {
            throw new ArgumentException("As mãos dos jogadores não podem compartilhar cartas.", nameof(maoJogadorDois));
        }

        var classificacaoJogadorUm = AvaliadorMaoPoker.Classificar(maoJogadorUm);
        var classificacaoJogadorDois = AvaliadorMaoPoker.Classificar(maoJogadorDois);
        var resultado = CompararMaos(
            maoJogadorUm,
            classificacaoJogadorUm,
            maoJogadorDois,
            classificacaoJogadorDois);

        return new ResultadoPartidaPoker(classificacaoJogadorUm, classificacaoJogadorDois, resultado);
    }

    private static ResultadoComparacao CompararMaos(
        MaoPoker maoJogadorUm,
        ClassificacaoMao classificacaoJogadorUm,
        MaoPoker maoJogadorDois,
        ClassificacaoMao classificacaoJogadorDois)
    {
        if (classificacaoJogadorUm > classificacaoJogadorDois)
        {
            return ResultadoComparacao.JogadorUm;
        }

        if (classificacaoJogadorDois > classificacaoJogadorUm)
        {
            return ResultadoComparacao.JogadorDois;
        }

        var criteriosJogadorUm = ObterCriteriosDesempate(maoJogadorUm, classificacaoJogadorUm);
        var criteriosJogadorDois = ObterCriteriosDesempate(maoJogadorDois, classificacaoJogadorDois);

        for (var indice = 0; indice < criteriosJogadorUm.Length; indice++)
        {
            if (criteriosJogadorUm[indice] > criteriosJogadorDois[indice])
            {
                return ResultadoComparacao.JogadorUm;
            }

            if (criteriosJogadorDois[indice] > criteriosJogadorUm[indice])
            {
                return ResultadoComparacao.JogadorDois;
            }
        }

        return ResultadoComparacao.Empate;
    }

    private static int[] ObterCriteriosDesempate(MaoPoker mao, ClassificacaoMao classificacao)
    {
        var gruposPorValor = mao.Cartas
            .GroupBy(carta => (int)carta.Valor)
            .ToArray();
        var valoresDecrescentes = mao.Cartas
            .Select(carta => (int)carta.Valor)
            .OrderByDescending(valor => valor)
            .ToArray();

        return classificacao switch
        {
            ClassificacaoMao.CartaAlta or ClassificacaoMao.Flush => valoresDecrescentes,
            ClassificacaoMao.UmPar => ConcatenarValores(
                gruposPorValor.Where(grupo => grupo.Count() == 2).Select(grupo => grupo.Key),
                gruposPorValor.Where(grupo => grupo.Count() == 1).Select(grupo => grupo.Key)),
            ClassificacaoMao.DoisPares => ConcatenarValores(
                gruposPorValor.Where(grupo => grupo.Count() == 2).Select(grupo => grupo.Key),
                gruposPorValor.Where(grupo => grupo.Count() == 1).Select(grupo => grupo.Key)),
            ClassificacaoMao.Trinca => ConcatenarValores(
                gruposPorValor.Where(grupo => grupo.Count() == 3).Select(grupo => grupo.Key),
                gruposPorValor.Where(grupo => grupo.Count() == 1).Select(grupo => grupo.Key)),
            ClassificacaoMao.Sequencia or ClassificacaoMao.StraightFlush => [ObterMaiorValorSequencia(valoresDecrescentes)],
            ClassificacaoMao.FullHouse => ConcatenarValores(
                gruposPorValor.Where(grupo => grupo.Count() == 3).Select(grupo => grupo.Key),
                gruposPorValor.Where(grupo => grupo.Count() == 2).Select(grupo => grupo.Key)),
            ClassificacaoMao.Quadra => ConcatenarValores(
                gruposPorValor.Where(grupo => grupo.Count() == 4).Select(grupo => grupo.Key),
                gruposPorValor.Where(grupo => grupo.Count() == 1).Select(grupo => grupo.Key)),
            ClassificacaoMao.RoyalStraightFlush => [],
            _ => throw new ArgumentOutOfRangeException(nameof(classificacao))
        };
    }

    private static int[] ConcatenarValores(IEnumerable<int> valoresDaCombinacao, IEnumerable<int> kickers)
    {
        return valoresDaCombinacao
            .OrderByDescending(valor => valor)
            .Concat(kickers.OrderByDescending(valor => valor))
            .ToArray();
    }

    private static int ObterMaiorValorSequencia(int[] valoresDecrescentes)
    {
        return valoresDecrescentes.SequenceEqual([14, 5, 4, 3, 2])
            ? 5
            : valoresDecrescentes[0];
    }
}
