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

    public PartidaDeXadrez()
    {
      Tabuleiro = new(8, 8);
      Turno = 1;
      JogadorAtual = Cor.Branca;
      _pecas = [];
      _pecasCapturadas = [];
      Terminada = false;
      ColocarPecas();
    }

    private void ExecutaMovimento(Posicao origem, Posicao destino)
    {
      Peca pecaDaJogada = Tabuleiro.RetirarPeca(origem);
      pecaDaJogada.IncrementarQteMovimentos();
      Peca pecaCapturada = Tabuleiro.RetirarPeca(destino);
      if (pecaCapturada != null)
      {
        _pecasCapturadas.Add(pecaCapturada);
      }
      Tabuleiro.ColocarPeca(pecaDaJogada, destino);

    }

    public void RealizaJogada(Posicao origem, Posicao destino)
    {
      ExecutaMovimento(origem, destino);
      Turno++;
      MudaJogador();
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

    public void ColocarNovaPeca(char coluna, int linha, Peca peca)
    {
      Tabuleiro.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
      _pecas.Add(peca);
    }

    private void ColocarPecas()
    {
      ColocarNovaPeca('a', 1, new Torre(Cor.Branca, Tabuleiro));
      ColocarNovaPeca('a', 2, new Torre(Cor.Branca, Tabuleiro));
      ColocarNovaPeca('b', 1, new Torre(Cor.Branca, Tabuleiro));
      ColocarNovaPeca('c', 3, new Torre(Cor.Branca, Tabuleiro));
      ColocarNovaPeca('f', 5, new Rei(Cor.Preta, Tabuleiro));
    }
  }
}