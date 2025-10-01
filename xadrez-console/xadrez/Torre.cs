using tabuleiro;

namespace xadrez
{
  public class Torre(Cor cor, Tabuleiro tabuleiro) : Peca(cor, tabuleiro)
  {
    public override string ToString()
    {
      return "T";
    }
  }
}