using tabuleiro;

namespace xadrez
{
  public class Rei(Cor cor, Tabuleiro tabuleiro) : Peca(cor, tabuleiro)
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

      //ACIMA
      posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
      if (Tabuleiro.PosicaoEhValida(posicao) && PodeMover(posicao))
      {
        movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;
      }

      //NE
      posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
      if (Tabuleiro.PosicaoEhValida(posicao) && PodeMover(posicao))
      {
        movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;
      }

      //E
      posicao.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);
      if (Tabuleiro.PosicaoEhValida(posicao) && PodeMover(posicao))
      {
        movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;
      }

      //SE
      posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
      if (Tabuleiro.PosicaoEhValida(posicao) && PodeMover(posicao))
      {
        movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;
      }

      //S - Abaixo
      posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
      if (Tabuleiro.PosicaoEhValida(posicao) && PodeMover(posicao))
      {
        movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;
      }

      //SO
      posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
      if (Tabuleiro.PosicaoEhValida(posicao) && PodeMover(posicao))
      {
        movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;
      }

      //O - Esquerda
      posicao.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);
      if (Tabuleiro.PosicaoEhValida(posicao) && PodeMover(posicao))
      {
        movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;
      }

      //NO
      posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
      if (Tabuleiro.PosicaoEhValida(posicao) && PodeMover(posicao))
      {
        movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;
      }

      return movimentosPossiveis;
    }

    public override string ToString()
    {
      return "R";
    }
  }
}