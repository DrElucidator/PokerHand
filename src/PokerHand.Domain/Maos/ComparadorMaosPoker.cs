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
        var resultado = CompararClassificacoes(classificacaoJogadorUm, classificacaoJogadorDois);

        return new ResultadoPartidaPoker(classificacaoJogadorUm, classificacaoJogadorDois, resultado);
    }

    private static ResultadoComparacao CompararClassificacoes(
        ClassificacaoMao classificacaoJogadorUm,
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

        return ResultadoComparacao.Empate;
    }
}
