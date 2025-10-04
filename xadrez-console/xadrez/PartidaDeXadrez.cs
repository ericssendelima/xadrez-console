using System.Drawing;
using tabuleiro;

namespace xadrez
{
  public class PartidaDeXadrez
  {
    private HashSet<Peca> _pecas;
    private HashSet<Peca> _pecasCapturadas;


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
    }

    public void RealizaJogada(Posicao origem, Posicao destino)
    {
      Peca pecaCapturada = ExecutaMovimento(origem, destino);

      if (EstaEmXeque(JogadorAtual))
      {
        DesfazMovimento(origem, destino, pecaCapturada);
        throw new TabuleiroException("Você não pode se colocar em xeque!");
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
      if (!Tabuleiro.Peca(origem).PodeMoverPara(destino))
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
      ColocarNovaPeca('d', 1, new Rei(Cor.Branca, Tabuleiro));
      ColocarNovaPeca('c', 1, new Torre(Cor.Branca, Tabuleiro));
      ColocarNovaPeca('h', 7, new Torre(Cor.Branca, Tabuleiro));

      //PRETAS
      ColocarNovaPeca('b', 8, new Torre(Cor.Preta, Tabuleiro));
      ColocarNovaPeca('a', 8, new Rei(Cor.Preta, Tabuleiro));
    }
  }
}