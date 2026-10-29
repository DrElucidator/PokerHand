namespace PokerHand.Domain.Maos;

public sealed record ResultadoPartidaPoker(
    ClassificacaoMao ClassificacaoJogadorUm,
    ClassificacaoMao ClassificacaoJogadorDois,
    ResultadoComparacao Resultado);
