using tabuleiro;

namespace xadrez
{
  public class Peao(Cor cor, Tabuleiro tabuleiro) : Peca(cor, tabuleiro)
  {
    private bool ExisteInimigo(Posicao posicao)
    {
      Peca pecaNaPosicao = Tabuleiro.Peca(posicao);
      return pecaNaPosicao != null && pecaNaPosicao.Cor != this.Cor;
    }
    private bool Livre(Posicao posicao)
    {
      Peca pecaNaPosicao = Tabuleiro.Peca(posicao);
      return pecaNaPosicao == null;
    }
    public override bool[,] MovimentosPossiveis()
    {
      bool[,] movimentosPossiveis = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];

      Posicao posicao = new(0, 0);

      if (Cor == Cor.Branca)
      {
        //ACIMA
        posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
        if (Tabuleiro.PosicaoEhValida(posicao) && Livre(posicao))
        {
          movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;
        }
        posicao.DefinirValores(Posicao.Linha - 2, Posicao.Coluna);
        Posicao p1 = new(Posicao.Linha - 1, Posicao.Coluna);
        if (Tabuleiro.PosicaoEhValida(posicao) && Livre(p1) && Livre(posicao) && QteMovimentos == 0)
        {
          movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;
        }
        posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
        if (Tabuleiro.PosicaoEhValida(posicao) && ExisteInimigo(posicao))
        {
          movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;
        }
        posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
        if (Tabuleiro.PosicaoEhValida(posicao) && ExisteInimigo(posicao))
        {
          movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;
        }
      }
      else
      {
        //AAIXO
        posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
        if (Tabuleiro.PosicaoEhValida(posicao) && Livre(posicao))
        {
          movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;
        }
        posicao.DefinirValores(Posicao.Linha + 2, Posicao.Coluna);
        Posicao p1 = new(Posicao.Linha + 1, Posicao.Coluna);
        if (Tabuleiro.PosicaoEhValida(posicao) && Livre(p1) && Livre(posicao) && QteMovimentos == 0)
        {
          movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;
        }
        posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
        if (Tabuleiro.PosicaoEhValida(posicao) && ExisteInimigo(posicao))
        {
          movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;
        }
        posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
        if (Tabuleiro.PosicaoEhValida(posicao) && ExisteInimigo(posicao))
        {
          movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;
        }
      }
      return movimentosPossiveis;
    }

    public override string ToString()
    {
      return "P";
    }
  }
}