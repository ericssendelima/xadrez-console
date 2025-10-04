using tabuleiro;
using xadrez;

namespace xadrez_console
{
  public class Tela
  {
    public static void ImprimirPartida(PartidaDeXadrez partida)
    {
      ImprimirTabuleiro(partida.Tabuleiro);
      Console.WriteLine();
      ImprimirPecasCapturadas(partida);
      Console.WriteLine();
      Console.WriteLine($"Turno: {partida.Turno}");
      Console.WriteLine($"Aguardando jogada: {partida.JogadorAtual}");
      if (partida.Xeque)
      {
        Console.WriteLine("XEQUE!");
      }

    }

    public static void ImprimirPecasCapturadas(PartidaDeXadrez partida)
    {
      Console.WriteLine("Peças capturadas: ");
      Console.Write("Brancas: ");
      ImprimirConjunto(partida.PecasCapturadas(Cor.Branca));
      Console.WriteLine();
      Console.Write("Pretas: ");
      ConsoleColor aux = Console.ForegroundColor;
      Console.ForegroundColor = ConsoleColor.Red;
      ImprimirConjunto(partida.PecasCapturadas(Cor.Preta));
      Console.ForegroundColor = aux;
      Console.WriteLine();
    }

    public static void ImprimirConjunto(HashSet<Peca> pecas)
    {
      Console.Write("[");
      foreach (Peca peca in pecas)
      {
        Console.Write(peca + " ");
      }
      Console.Write("]");
    }
    public static void ImprimirTabuleiro(Tabuleiro tabuleiro)
    {
      for (int linha = 0; linha < tabuleiro.Linhas; linha++)
      {
        Console.Write(8 - linha + " ");
        for (int coluna = 0; coluna < tabuleiro.Colunas; coluna++)
        {
          ImprimirPeca(tabuleiro.Peca(linha, coluna));
        }
        Console.WriteLine();
      }
      Console.WriteLine("  a b c d e f g h");
    }
    public static void ImprimirTabuleiro(Tabuleiro tabuleiro, bool[,] movimentosPossiveis)
    {
      ConsoleColor fundoOriginal = Console.BackgroundColor;
      ConsoleColor fundoAlterado = ConsoleColor.DarkMagenta;

      for (int linha = 0; linha < tabuleiro.Linhas; linha++)
      {
        Console.Write(8 - linha + " ");
        for (int coluna = 0; coluna < tabuleiro.Colunas; coluna++)
        {
          if (movimentosPossiveis[linha, coluna])
          {
            Console.BackgroundColor = fundoAlterado;
          }
          else
          {
            Console.BackgroundColor = fundoOriginal;
          }
          ImprimirPeca(tabuleiro.Peca(linha, coluna));
          Console.BackgroundColor = fundoOriginal;
        }
        Console.WriteLine();
      }
      Console.WriteLine("  a b c d e f g h");
      Console.BackgroundColor = fundoOriginal;
    }

    public static void ImprimirPeca(Peca peca)
    {
      if (peca == null)
      {
        Console.Write("- ");
      }
      else
      {
        if (peca.Cor == Cor.Branca)
        {
          Console.Write(peca);
        }
        else
        {
          ConsoleColor aux = Console.ForegroundColor;
          Console.ForegroundColor = ConsoleColor.Red;
          Console.Write(peca);
          Console.ForegroundColor = aux;
        }
        Console.Write(" ");
      }
    }

    public static PosicaoXadrez LerPosicaoXadrez()
    {
      string posicao = Console.ReadLine();

      if (posicao.Length < 2)
      {
        throw new TabuleiroException("Posição inválida!");
      }

      PosicaoXadrez posicaoXadrez = new(posicao[0], int.Parse(posicao[1] + ""));

      return posicaoXadrez;
    }
  }
}