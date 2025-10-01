using tabuleiro;

namespace tabuleiro
{
  public class Peca(Cor cor, Tabuleiro tabuleiro)
  {
    public Posicao Posicao { get; set; }
    public Cor Cor { get; protected set; } = cor;
    public Tabuleiro Tab { get; protected set; } = tabuleiro;
    public int QteMovimentos { get; protected set; } = 0;

    public void IncrementarQteMovimentos()
    {
      QteMovimentos++;
    }
  }
}