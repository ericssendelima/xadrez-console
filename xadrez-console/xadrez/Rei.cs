using tabuleiro;

namespace xadrez
{
  public class Rei(Cor cor, Tabuleiro tabuleiro, PartidaDeXadrez partida) : Peca(cor, tabuleiro)
  {
    private readonly PartidaDeXadrez _partida = partida;
    private bool PodeMover(Posicao posicao)
    {
      Peca pecaNaPosicao = Tabuleiro.Peca(posicao);
      return pecaNaPosicao == null || pecaNaPosicao.Cor != this.Cor;
    }

    private bool TesteTorreParaRoque(Posicao posicao)
    {
      Peca torre = Tabuleiro.Peca(posicao);
      return torre != null && torre is Torre && torre.Cor == Cor && torre.QteMovimentos == 0;
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
      //#Jogada especial
      if (QteMovimentos == 0 && !_partida.Xeque)
      {
        //#Roque pequeno
        Posicao posicaoTorre1 = new(Posicao.Linha, Posicao.Coluna + 3);
        if (TesteTorreParaRoque(posicaoTorre1))
        {
          Posicao posicaoVizinho1Rei = new(Posicao.Linha, Posicao.Coluna + 1);
          Posicao posicaoVizinho2Rei = new(Posicao.Linha, Posicao.Coluna + 2);
          if (Tabuleiro.Peca(posicaoVizinho1Rei) == null && Tabuleiro.Peca(posicaoVizinho2Rei) == null)
          {
            movimentosPossiveis[Posicao.Linha, Posicao.Coluna + 2] = true;
          }
        }

        //#Roque grande
        Posicao posicaoTorre2 = new(Posicao.Linha, Posicao.Coluna - 4);
        if (TesteTorreParaRoque(posicaoTorre2))
        {
          Posicao posicaoVizinho1Rei = new(Posicao.Linha, Posicao.Coluna - 1);
          Posicao posicaoVizinho2Rei = new(Posicao.Linha, Posicao.Coluna - 2);
          Posicao posicaoVizinho3Rei = new(Posicao.Linha, Posicao.Coluna - 3);
          if (Tabuleiro.Peca(posicaoVizinho1Rei) == null && Tabuleiro.Peca(posicaoVizinho2Rei) == null && Tabuleiro.Peca(posicaoVizinho3Rei) == null)
          {
            movimentosPossiveis[Posicao.Linha, Posicao.Coluna - 2] = true;
          }
        }
      }



      return movimentosPossiveis;
    }

    public override string ToString()
    {
      return "R";
    }
  }
}