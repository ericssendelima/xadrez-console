using tabuleiro;

namespace xadrez
{
  public class Cavalo(Cor cor, Tabuleiro tabuleiro) : Peca(cor, tabuleiro)
  {
    private bool PodeMover(Posicao posicao)
    {
      Peca pecaNaPosicao = Tabuleiro.Peca(posicao);
      return pecaNaPosicao == null || pecaNaPosicao.Cor != this.Cor;
    }
    public override bool[,] MovimentosPossiveis()
    {
      bool[,] movimentosPossiveis = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];

      Posicao posicao = new(0, 0);

      //ACIMA - DIREITA
      posicao.DefinirValores(Posicao.Linha + 2, Posicao.Coluna + 1);
      if (Tabuleiro.PosicaoEhValida(posicao) && PodeMover(posicao))
      {
        movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;
      }

      //ABAIXO - DIREITA
      posicao.DefinirValores(Posicao.Linha - 2, Posicao.Coluna + 1);
      if (Tabuleiro.PosicaoEhValida(posicao) && PodeMover(posicao))
      {
        movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;
      }

      //ACIMA - ESQUERDA
      posicao.DefinirValores(Posicao.Linha + 2, Posicao.Coluna - 1);
      if (Tabuleiro.PosicaoEhValida(posicao) && PodeMover(posicao))
      {
        movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;
      }

      //ABAIXO - ESQUERDA
      posicao.DefinirValores(Posicao.Linha - 2, Posicao.Coluna - 1);
      if (Tabuleiro.PosicaoEhValida(posicao) && PodeMover(posicao))
      {
        movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;
      }


      return movimentosPossiveis;
    }

    public override string ToString()
    {
      return "C";
    }
  }
}