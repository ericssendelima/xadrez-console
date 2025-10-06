using tabuleiro;

namespace xadrez
{
  public class Peao(Cor cor, Tabuleiro tabuleiro, PartidaDeXadrez partida) : Peca(cor, tabuleiro)
  {
    private readonly PartidaDeXadrez _partida = partida;

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

        //#Jogada especial - En Passant
        if (Posicao.Linha == 3)
        {
          Posicao esquerda = new(Posicao.Linha, Posicao.Coluna - 1);
          if (Tabuleiro.PosicaoEhValida(esquerda) && ExisteInimigo(esquerda) && Tabuleiro.Peca(esquerda) == _partida.VulneravelEnPassant)
          {
            movimentosPossiveis[esquerda.Linha - 1, esquerda.Coluna] = true;
          }
          Posicao direita = new(Posicao.Linha, Posicao.Coluna + 1);
          if (Tabuleiro.PosicaoEhValida(direita) && ExisteInimigo(direita) && Tabuleiro.Peca(direita) == _partida.VulneravelEnPassant)
          {
            movimentosPossiveis[direita.Linha - 1, direita.Coluna] = true;
          }
        }
      }
      else
      {
        //ABAIXO
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

        //#Jogada especial - En Passant
        if (Posicao.Linha == 4)
        {
          Posicao esquerda = new(Posicao.Linha, Posicao.Coluna - 1);
          if (Tabuleiro.PosicaoEhValida(esquerda) && ExisteInimigo(esquerda) && Tabuleiro.Peca(esquerda) == _partida.VulneravelEnPassant)
          {
            movimentosPossiveis[esquerda.Linha + 1, esquerda.Coluna] = true;
          }
          Posicao direita = new(Posicao.Linha, Posicao.Coluna + 1);
          if (Tabuleiro.PosicaoEhValida(direita) && ExisteInimigo(direita) && Tabuleiro.Peca(direita) == _partida.VulneravelEnPassant)
          {
            movimentosPossiveis[direita.Linha + 1, direita.Coluna] = true;
          }
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