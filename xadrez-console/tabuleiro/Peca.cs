using tabuleiro;

namespace tabuleiro
{
  public abstract class Peca(Cor cor, Tabuleiro tabuleiro)
  {
    public Posicao Posicao { get; set; }
    public Cor Cor { get; protected set; } = cor;
    public Tabuleiro Tabuleiro { get; protected set; } = tabuleiro;
    public int QteMovimentos { get; protected set; } = 0;

    public void IncrementarQteMovimentos() => QteMovimentos++;
    public void DecrementarQteMovimentos() => QteMovimentos--;
    

    public bool ExisteMovimentosPossiveis()
    {
      bool[,] movimentosPossiveis = MovimentosPossiveis();
      for (int linha = 0; linha < Tabuleiro.Linhas; linha++)
      {
        for (int coluna = 0; coluna < Tabuleiro.Colunas; coluna++)
        {
          if (movimentosPossiveis[linha, coluna])
          {
            return true;
          }
        }
      }

      return false;
    }

    public bool PodeMoverPara(Posicao destino)
    {
      return MovimentosPossiveis()[destino.Linha, destino.Coluna];
    }

    public abstract bool[,] MovimentosPossiveis();
  }
}