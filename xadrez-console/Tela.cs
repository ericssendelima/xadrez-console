using tabuleiro;
using xadrez;

namespace xadrez_console
{
  public class Tela
  {
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
      PosicaoXadrez posicaoXadrez = new(posicao[0], int.Parse(posicao[1] + ""));

        return posicaoXadrez;
    }
  }
}