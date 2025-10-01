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
          if (tabuleiro.Peca(linha, coluna) != null)
          {
            ImprimirPeca(tabuleiro.Peca(linha, coluna));
            Console.Write(" ");
          }
          else
          {
            Console.Write("- ");
          }
        }
        Console.WriteLine();
      }
      Console.WriteLine("  a b c d e f g h");
    }

    public static void ImprimirPeca(Peca peca)
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
    }

    public static PosicaoXadrez LerPosicaoXadrez()
    {
      string posicao = Console.ReadLine();
      return new PosicaoXadrez(posicao[0], int.Parse(posicao[1] + ""));
    }
  }
}