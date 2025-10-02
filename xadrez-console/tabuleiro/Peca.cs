using tabuleiro;

namespace tabuleiro
{
  public abstract class Peca(Cor cor, Tabuleiro tabuleiro)
  {
    public Posicao Posicao { get; set; }
    public Cor Cor { get; protected set; } = cor;
    public Tabuleiro Tabuleiro { get; protected set; } = tabuleiro;
    public int QteMovimentos { get; protected set; } = 0;

    public void IncrementarQteMovimentos()
    {
      QteMovimentos++;
    }

    public abstract bool[,] MovimentosPossiveis();
  }
}