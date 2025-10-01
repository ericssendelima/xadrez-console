using tabuleiro;

namespace xadrez
{
  public class Rei(Cor cor, Tabuleiro tabuleiro) : Peca(cor, tabuleiro)
  {
    public override string ToString()
    {
      return "R";
    }
  }
}