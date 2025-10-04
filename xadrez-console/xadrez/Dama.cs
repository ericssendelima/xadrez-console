using tabuleiro;

namespace xadrez
{
  public class Dama(Cor cor, Tabuleiro tabuleiro) : Peca(cor, tabuleiro)
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
      while (Tabuleiro.PosicaoEhValida(posicao) && PodeMover(posicao))
      {
        movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;
        if (Tabuleiro.ExistePeca(posicao) && Tabuleiro.Peca(posicao).Cor != Cor)
        {
          break;
        }
        posicao.Linha -= 1;
      }
      //ABAIXO
      posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
      while (Tabuleiro.PosicaoEhValida(posicao) && PodeMover(posicao))
      {
        movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;
        if (Tabuleiro.ExistePeca(posicao) && Tabuleiro.Peca(posicao).Cor != Cor)
        {
          break;
        }
        posicao.Linha += 1;
      }
      //DIREITA
      posicao.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);
      while (Tabuleiro.PosicaoEhValida(posicao) && PodeMover(posicao))
      {
        movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;
        if (Tabuleiro.ExistePeca(posicao) && Tabuleiro.Peca(posicao).Cor != Cor)
        {
          break;
        }
        posicao.Coluna += 1;
      }
      //ESQUERDA
      posicao.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);
      while (Tabuleiro.PosicaoEhValida(posicao) && PodeMover(posicao))
      {
        movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;
        if (Tabuleiro.ExistePeca(posicao) && Tabuleiro.Peca(posicao).Cor != Cor)
        {
          break;
        }
        posicao.Coluna -= 1;
      }

      //NE
      posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
      while (Tabuleiro.PosicaoEhValida(posicao) && PodeMover(posicao))
      {
        movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;
        if (Tabuleiro.ExistePeca(posicao) && Tabuleiro.Peca(posicao).Cor != Cor)
        {
          break;
        }
        posicao.Linha -= 1;
        posicao.Coluna += 1;
      }

      //SE
      posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
      while (Tabuleiro.PosicaoEhValida(posicao) && PodeMover(posicao))
      {
        movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;
        if (Tabuleiro.ExistePeca(posicao) && Tabuleiro.Peca(posicao).Cor != Cor)
        {
          break;
        }
        posicao.Linha += 1;
        posicao.Coluna += 1;
      }

      //SO
      posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
      while (Tabuleiro.PosicaoEhValida(posicao) && PodeMover(posicao))
      {
        movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;
        if (Tabuleiro.ExistePeca(posicao) && Tabuleiro.Peca(posicao).Cor != Cor)
        {
          break;
        }
        posicao.Linha += 1;
        posicao.Coluna -= 1;
      }

      //NO
      posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
      while (Tabuleiro.PosicaoEhValida(posicao) && PodeMover(posicao))
      {
        movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;
        if (Tabuleiro.ExistePeca(posicao) && Tabuleiro.Peca(posicao).Cor != Cor)
        {
          break;
        }
        posicao.Linha -= 1;
        posicao.Coluna -= 1;

      }


      return movimentosPossiveis;
    }
    public override string ToString()
    {
      return "D";
    }
  }
}