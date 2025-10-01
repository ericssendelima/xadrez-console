using tabuleiro;

namespace tabuleiro
{
  public class Peca(Posicao posicao, Cor cor, Tabuleiro tabuleiro)
  {
    public Posicao Posicao { get; set; } = posicao;
    public Cor Cor { get; protected set; } = cor;
    public Tabuleiro Tab { get; protected set; } = tabuleiro;
    public int QteMovimentos { get; protected set; }
  }
}