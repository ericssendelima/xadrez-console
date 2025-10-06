using System.Drawing;
using tabuleiro;

namespace xadrez
{
  public class PartidaDeXadrez
  {
    private HashSet<Peca> _pecas;
    private HashSet<Peca> _pecasCapturadas;
    public Peca VulneravelEnPassant { get; private set; }

    public int Turno { get; private set; }
    public Cor JogadorAtual { get; private set; }
    public Tabuleiro Tabuleiro { get; private set; }
    public bool Terminada { get; private set; }
    public bool Xeque { get; private set; }

    public PartidaDeXadrez()
    {
      Tabuleiro = new(8, 8);
      Turno = 1;
      JogadorAtual = Cor.Branca;
      Xeque = false;
      VulneravelEnPassant = null;
      _pecas = [];
      _pecasCapturadas = [];
      Terminada = false;
      ColocarPecas();
    }

    private Peca ExecutaMovimento(Posicao origem, Posicao destino)
    {
      Peca pecaDaJogada = Tabuleiro.RetirarPeca(origem);
      pecaDaJogada.IncrementarQteMovimentos();
      Peca pecaCapturada = Tabuleiro.RetirarPeca(destino);
      if (pecaCapturada != null)
      {
        _pecasCapturadas.Add(pecaCapturada);
      }
      Tabuleiro.ColocarPeca(pecaDaJogada, destino);

      //#Jogada especial - roque pequeno
      if (pecaDaJogada is Rei && destino.Coluna == origem.Coluna + 2)
      {
        Posicao posicaoOrigemTorre = new(origem.Linha, origem.Coluna + 3);
        Posicao posicaoDestinoTorre = new(origem.Linha, origem.Coluna + 1);
        Peca torre = Tabuleiro.RetirarPeca(posicaoOrigemTorre);
        torre.IncrementarQteMovimentos();
        Tabuleiro.ColocarPeca(torre, posicaoDestinoTorre);
      }

      //#Jogada especial - roque grande
      if (pecaDaJogada is Rei && destino.Coluna == origem.Coluna - 2)
      {
        Posicao posicaoOrigemTorre = new(origem.Linha, origem.Coluna - 4);
        Posicao posicaoDestinoTorre = new(origem.Linha, origem.Coluna - 1);
        Peca torre = Tabuleiro.RetirarPeca(posicaoOrigemTorre);
        torre.IncrementarQteMovimentos();
        Tabuleiro.ColocarPeca(torre, posicaoDestinoTorre);
      }

      //#Jogada especial - en passant
      if (pecaDaJogada is Peao)
      {
        if (origem.Coluna != destino.Coluna && pecaCapturada == null)
        {
          Posicao posP;
          if (pecaDaJogada.Cor == Cor.Branca)
          {
            posP = new Posicao(destino.Linha + 1, destino.Coluna);
          }
          else
          {
            posP = new Posicao(destino.Linha - 1, destino.Coluna);
          }
          pecaCapturada = Tabuleiro.RetirarPeca(posP);
          _pecasCapturadas.Add(pecaCapturada);
        }
      }


      return pecaCapturada;
    }

    public void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
    {
      Peca pecaDaJogada = Tabuleiro.RetirarPeca(destino);
      pecaDaJogada.DecrementarQteMovimentos();
      if (pecaCapturada != null)
      {
        Tabuleiro.ColocarPeca(pecaCapturada, destino);
        _pecasCapturadas.Remove(pecaCapturada);
      }
      Tabuleiro.ColocarPeca(pecaDaJogada, origem);

      //#Jogada especial - roque pequeno
      if (pecaDaJogada is Rei && destino.Coluna == origem.Coluna + 2)
      {
        Posicao posicaoOrigemTorre = new(origem.Linha, origem.Coluna + 3);
        Posicao posicaoDestinoTorre = new(origem.Linha, origem.Coluna + 1);
        Peca torre = Tabuleiro.RetirarPeca(posicaoDestinoTorre);
        torre.DecrementarQteMovimentos();
        Tabuleiro.ColocarPeca(torre, posicaoOrigemTorre);
      }

      //#Jogada especial - roque grande
      if (pecaDaJogada is Rei && destino.Coluna == origem.Coluna - 2)
      {
        Posicao posicaoOrigemTorre = new(origem.Linha, origem.Coluna - 4);
        Posicao posicaoDestinoTorre = new(origem.Linha, origem.Coluna - 1);
        Peca torre = Tabuleiro.RetirarPeca(posicaoDestinoTorre);
        torre.DecrementarQteMovimentos();
        Tabuleiro.ColocarPeca(torre, posicaoOrigemTorre);
      }

      //#Jogada especial - en passant
      if (pecaDaJogada is Peao)
      {
        if (origem.Coluna != destino.Coluna && pecaCapturada == VulneravelEnPassant)
        {
          Peca peao = Tabuleiro.RetirarPeca(destino);
          Posicao posP;
          if (pecaDaJogada.Cor == Cor.Branca)
          {
            posP = new(3, destino.Coluna);
          }
          else
          {
            posP = new(4, destino.Coluna);
          }
          Tabuleiro.ColocarPeca(peao, posP);
        }
      }
    }

    public void RealizaJogada(Posicao origem, Posicao destino)
    {
      Peca pecaCapturada = ExecutaMovimento(origem, destino);

      if (EstaEmXeque(JogadorAtual))
      {
        DesfazMovimento(origem, destino, pecaCapturada);
        throw new TabuleiroException("Você não pode se colocar em xeque!");
      }

      Peca pecaDaJogada = Tabuleiro.Peca(destino);

      //Jogada especial - Promoção
      if (pecaDaJogada is Peao)
      {
        if (pecaDaJogada.Cor == Cor.Branca && destino.Linha == 0 || pecaDaJogada.Cor == Cor.Preta && destino.Linha == 7)
        {
          pecaDaJogada = Tabuleiro.RetirarPeca(destino);
          _pecas.Remove(pecaDaJogada);
          Peca dama = new Dama(pecaDaJogada.Cor, Tabuleiro);
          Tabuleiro.ColocarPeca(dama, destino);
          _pecas.Add(dama);
        }
      }


      Xeque = EstaEmXeque(Adversaria(JogadorAtual));

      if (TesteXequemate(Adversaria(JogadorAtual)))
      {
        Terminada = true;
      }
      else
      {
        Turno++;
        MudaJogador();
      }

      //#Jogada especial En Passant
      if (pecaDaJogada is Peao && (destino.Linha == origem.Linha - 2 || destino.Linha == origem.Linha + 2))
      {
        VulneravelEnPassant = pecaDaJogada;
      }
      else
      {
        VulneravelEnPassant = null;
      }
    }

    public void ValidarPosicaoDeOrigem(Posicao origem)
    {
      Tabuleiro.ValidarPosicao(origem);
      if (Tabuleiro.Peca(origem) == null)
      {
        throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
      }
      if (JogadorAtual != Tabuleiro.Peca(origem).Cor)
      {
        throw new TabuleiroException("A peça de origem escolhida não é sua!");
      }
      if (!Tabuleiro.Peca(origem).ExisteMovimentosPossiveis())
      {
        throw new TabuleiroException("Não há movimentos possíveis para a peça de origem escolhida!");
      }
    }
    public void ValidarPosicaoDeDestino(Posicao origem, Posicao destino)
    {
      Tabuleiro.ValidarPosicao(destino);
      if (!Tabuleiro.Peca(origem).MovimentoPossivel(destino))
      {
        throw new TabuleiroException("Posição de destino inválida!");
      }
    }
    private void MudaJogador()
    {
      if (JogadorAtual == Cor.Branca)
      {
        JogadorAtual = Cor.Preta;
      }
      else
      {
        JogadorAtual = Cor.Branca;
      }
    }

    public HashSet<Peca> PecasCapturadas(Cor cor)
    {
      HashSet<Peca> aux = [];
      foreach (Peca peca in _pecasCapturadas)
      {
        if (peca.Cor == cor)
        {
          aux.Add(peca);
        }
      }
      return aux;
    }
    public HashSet<Peca> PecasEmJogo(Cor cor)
    {
      HashSet<Peca> aux = [];
      foreach (Peca peca in _pecas)
      {
        if (peca.Cor == cor)
        {
          aux.Add(peca);
        }
      }
      aux.ExceptWith(PecasCapturadas(cor));
      return aux;
    }

    private Cor Adversaria(Cor cor)
    {
      return cor == Cor.Branca ? Cor.Preta : Cor.Branca;
    }

    private Peca Rei(Cor cor)
    {
      foreach (Peca peca in PecasEmJogo(cor))
      {
        if (peca is Rei)
        {
          return peca;
        }
      }
      return null;
    }

    public bool EstaEmXeque(Cor cor)
    {
      Peca rei = Rei(cor) ?? throw new TabuleiroException($"Não tem rei da cor {cor} no tabuleiro!");

      foreach (Peca peca in PecasEmJogo(Adversaria(cor)))
      {
        bool[,] movimentosPossiveis = peca.MovimentosPossiveis();
        if (movimentosPossiveis[rei.Posicao.Linha, rei.Posicao.Coluna])
        {
          return true;
        }
      }
      return false;
    }

    public bool TesteXequemate(Cor cor)
    {
      if (!EstaEmXeque(cor)) return false;

      foreach (Peca peca in PecasEmJogo(cor))
      {
        Posicao posicaoOrigem = peca.Posicao;
        bool[,] movimentosPossiveis = peca.MovimentosPossiveis();
        for (int linha = 0; linha < Tabuleiro.Linhas; linha++)
        {
          for (int coluna = 0; coluna < Tabuleiro.Colunas; coluna++)
          {
            if (movimentosPossiveis[linha, coluna])
            {
              Posicao destinoTeste = new(linha, coluna);
              Peca pecaCapturada = ExecutaMovimento(posicaoOrigem, destinoTeste);
              bool testeXeque = EstaEmXeque(cor);
              DesfazMovimento(posicaoOrigem, destinoTeste, pecaCapturada);
              if (!testeXeque)
              {
                return false;
              }
            }
          }
        }
      }
      return true;
    }

    public void ColocarNovaPeca(char coluna, int linha, Peca peca)
    {
      Tabuleiro.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
      _pecas.Add(peca);
    }

    private void ColocarPecas()
    {
      //BRANCAS
      ColocarNovaPeca('a', 1, new Torre(Cor.Branca, Tabuleiro));
      ColocarNovaPeca('b', 1, new Cavalo(Cor.Branca, Tabuleiro));
      ColocarNovaPeca('c', 1, new Bispo(Cor.Branca, Tabuleiro));
      ColocarNovaPeca('d', 1, new Dama(Cor.Branca, Tabuleiro));
      ColocarNovaPeca('e', 1, new Rei(Cor.Branca, Tabuleiro, this));
      ColocarNovaPeca('f', 1, new Bispo(Cor.Branca, Tabuleiro));
      ColocarNovaPeca('g', 1, new Cavalo(Cor.Branca, Tabuleiro));
      ColocarNovaPeca('h', 1, new Torre(Cor.Branca, Tabuleiro));
      ColocarNovaPeca('a', 2, new Peao(Cor.Branca, Tabuleiro, this));
      ColocarNovaPeca('b', 2, new Peao(Cor.Branca, Tabuleiro, this));
      ColocarNovaPeca('c', 2, new Peao(Cor.Branca, Tabuleiro, this));
      ColocarNovaPeca('d', 2, new Peao(Cor.Branca, Tabuleiro, this));
      ColocarNovaPeca('e', 2, new Peao(Cor.Branca, Tabuleiro, this));
      ColocarNovaPeca('f', 2, new Peao(Cor.Branca, Tabuleiro, this));
      ColocarNovaPeca('g', 2, new Peao(Cor.Branca, Tabuleiro, this));
      ColocarNovaPeca('h', 2, new Peao(Cor.Branca, Tabuleiro, this));

      //PRETAS
      ColocarNovaPeca('a', 8, new Torre(Cor.Preta, Tabuleiro));
      ColocarNovaPeca('b', 8, new Cavalo(Cor.Preta, Tabuleiro));
      ColocarNovaPeca('c', 8, new Bispo(Cor.Preta, Tabuleiro));
      ColocarNovaPeca('d', 8, new Dama(Cor.Preta, Tabuleiro));
      ColocarNovaPeca('e', 8, new Rei(Cor.Preta, Tabuleiro, this));
      ColocarNovaPeca('f', 8, new Bispo(Cor.Preta, Tabuleiro));
      ColocarNovaPeca('g', 8, new Cavalo(Cor.Preta, Tabuleiro));
      ColocarNovaPeca('h', 8, new Torre(Cor.Preta, Tabuleiro));
      ColocarNovaPeca('a', 7, new Peao(Cor.Preta, Tabuleiro, this));
      ColocarNovaPeca('b', 7, new Peao(Cor.Preta, Tabuleiro, this));
      ColocarNovaPeca('c', 7, new Peao(Cor.Preta, Tabuleiro, this));
      ColocarNovaPeca('d', 7, new Peao(Cor.Preta, Tabuleiro, this));
      ColocarNovaPeca('e', 7, new Peao(Cor.Preta, Tabuleiro, this));
      ColocarNovaPeca('f', 7, new Peao(Cor.Preta, Tabuleiro, this));
      ColocarNovaPeca('g', 7, new Peao(Cor.Preta, Tabuleiro, this));
      ColocarNovaPeca('h', 7, new Peao(Cor.Preta, Tabuleiro, this));
    }
  }
}