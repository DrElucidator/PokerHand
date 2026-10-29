using PokerHand.Domain.Maos;

Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.WriteLine("Validador de Mão de Poker");
Console.WriteLine("Informe cinco cartas separadas por espaço. Exemplo: 5C 5P 6E 7P KO");
Console.WriteLine("Valores: 2 a 9, D (dez), J, Q, K e A (ás).");
Console.WriteLine("Naipes: O (ouro), C (copas), E (espadas) e P (paus).");
Console.WriteLine("Cada carta usa exatamente dois caracteres e não pode ser usada pelos dois jogadores.");
Console.WriteLine();

try
{
    Console.Write("Jogador 1: ");
    var entradaJogadorUm = Console.ReadLine();

    Console.Write("Jogador 2: ");
    var entradaJogadorDois = Console.ReadLine();

    var maoJogadorUm = MaoPoker.Criar(entradaJogadorUm ?? string.Empty);
    var maoJogadorDois = MaoPoker.Criar(entradaJogadorDois ?? string.Empty);
    var resultado = ComparadorMaosPoker.Avaliar(maoJogadorUm, maoJogadorDois);

    Console.WriteLine();
    Console.WriteLine($"Jogador 1: {Descrever(resultado.ClassificacaoJogadorUm)}");
    Console.WriteLine($"Jogador 2: {Descrever(resultado.ClassificacaoJogadorDois)}");
    Console.WriteLine($"Resultado: {DescreverResultado(resultado.Resultado)}");
}
catch (ArgumentException excecao)
{
    Console.WriteLine();
    Console.WriteLine($"Erro: {excecao.Message}");
}

static string Descrever(ClassificacaoMao classificacao)
{
    return classificacao switch
    {
        ClassificacaoMao.CartaAlta => "Carta Alta",
        ClassificacaoMao.UmPar => "Um Par",
        ClassificacaoMao.DoisPares => "Dois Pares",
        ClassificacaoMao.Trinca => "Trinca",
        ClassificacaoMao.Sequencia => "Sequência",
        ClassificacaoMao.Flush => "Flush",
        ClassificacaoMao.FullHouse => "Full House",
        ClassificacaoMao.Quadra => "Quadra",
        ClassificacaoMao.StraightFlush => "Straight Flush",
        ClassificacaoMao.RoyalStraightFlush => "Royal Straight Flush",
        _ => throw new ArgumentOutOfRangeException(nameof(classificacao))
    };
}

static string DescreverResultado(ResultadoComparacao resultado)
{
    return resultado switch
    {
        ResultadoComparacao.JogadorUm => "Jogador 1 venceu",
        ResultadoComparacao.JogadorDois => "Jogador 2 venceu",
        ResultadoComparacao.Empate => "Empate",
        _ => throw new ArgumentOutOfRangeException(nameof(resultado))
    };
}
