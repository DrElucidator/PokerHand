# PokerHand

Validador e comparador de mãos de poker desenvolvido em C# para o quebra-cuca da Academia do Programador.

O projeto recebe duas mãos com cinco cartas, classifica cada uma, aplica as regras de desempate e informa o vencedor ou um empate. A lógica foi construída primeiro no domínio, com testes unitários cobrindo as regras principais antes da interface de Console.

## Funcionalidades

- Interpreta cartas no formato `valor + naipe`, como `AC` e `DO`.
- Valida mãos com exatamente cinco cartas.
- Rejeita valores, naipes e formatos inválidos.
- Rejeita cartas repetidas na mesma mão ou compartilhadas entre jogadores.
- Classifica as dez combinações de poker.
- Compara mãos de classificações diferentes.
- Aplica kickers e demais critérios de desempate quando as classificações são iguais.
- Trata naipes como equivalentes para desempate.
- Aceita `A-2-3-4-5` como a sequência mais baixa.

## Formato das cartas

Cada carta possui exatamente dois caracteres: valor seguido de naipe.

| Tipo | Símbolos aceitos |
| --- | --- |
| Valores numéricos | `2` a `9` |
| Dez | `D` |
| Valete | `J` |
| Dama | `Q` |
| Rei | `K` |
| Ás | `A` |
| Ouro | `O` |
| Copas | `C` |
| Espadas | `E` |
| Paus | `P` |

Exemplos:

```text
AC  Ás de Copas
DO  Dez de Ouro
7P  Sete de Paus
```

## Classificações

| Ranking | Classificação |
| ---: | --- |
| 1 | Carta Alta |
| 2 | Um Par |
| 3 | Dois Pares |
| 4 | Trinca |
| 5 | Sequência |
| 6 | Flush |
| 7 | Full House |
| 8 | Quadra |
| 9 | Straight Flush |
| 10 | Royal Straight Flush |

O Ás vale `14` como carta alta. Por decisão de regra adicional, ele também pode ser usado como carta baixa na sequência `A-2-3-4-5`.

## Como executar

### Pré-requisito

- .NET SDK 10 ou versão compatível com `net10.0`.

No diretório raiz do projeto, execute:

```powershell
dotnet run --project src/PokerHand.Console
```

Exemplo de uso:

```text
Jogador 1: DC 6C 4C 2C AC
Jogador 2: 2P 3P 4E 5O 6O

Jogador 1: Flush
Jogador 2: Sequência
Resultado: Jogador 1 venceu
```

As duas mãos não podem compartilhar uma carta. Por exemplo, se o Jogador 1 usar `2C`, o Jogador 2 deve usar outra carta, como `2P`, `2E` ou `2O`.

## Testes

Os testes unitários utilizam **xUnit** e cobrem criação de cartas, validação de mãos, classificações, comparações, desempates e cenários inválidos.

Para executá-los:

```powershell
dotnet test tests/PokerHand.Domain.Tests
```

Também podem ser executados pelo Test Explorer da IDE.

## Estrutura da solução

```text
PokerHand
├── src
│   ├── PokerHand.Domain
│   │   ├── Cartas
│   │   └── Maos
│   └── PokerHand.Console
└── tests
    └── PokerHand.Domain.Tests
```

### `PokerHand.Domain`

Camada responsável pelas regras de negócio:

- `Carta`, `ValorCarta` e `Naipe` representam o baralho.
- `MaoPoker` valida e representa uma mão de cinco cartas.
- `AvaliadorMaoPoker` identifica a classificação da mão.
- `ComparadorMaosPoker` determina o vencedor e aplica os desempates.

### `PokerHand.Console`

Adaptador de entrada e saída para execução manual. Ele lê as duas mãos, delega a avaliação ao domínio e apresenta o resultado em português.

### `PokerHand.Domain.Tests`

Testes unitários isolados para preservar o comportamento esperado das regras de poker.

## Tecnologias

- C#
- .NET 10
- xUnit
- Console Application
- Organização orientada ao domínio

## Evoluções possíveis

- Repetir partidas sem reiniciar o Console.
- Exibir detalhes do critério que decidiu cada vitória.
- Criar uma API HTTP sobre a camada de domínio.
- Adicionar uma interface visual ou integração com reconhecimento de cartas.

