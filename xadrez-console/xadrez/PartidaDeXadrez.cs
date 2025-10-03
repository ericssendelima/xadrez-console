using tabuleiro;

namespace xadrez
{
  public class PartidaDeXadrez
  {
    public int Turno { get; private set; }
    public Cor JogadorAtual { get; private set; }
    public Tabuleiro Tabuleiro { get; private set; }
    public bool Terminada { get; private set; }

    public PartidaDeXadrez()
    {
      Tabuleiro = new(8, 8);
      Turno = 1;
      JogadorAtual = Cor.Branca;
      ColocarPecas();
      Terminada = false;
    }

    private void ExecutaMovimento(Posicao origem, Posicao destino)
    {
      Peca pecaDaJogada = Tabuleiro.RetirarPeca(origem);
      pecaDaJogada.IncrementarQteMovimentos();
      Peca pecaCapturada = Tabuleiro.RetirarPeca(destino);

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

    private void ColocarPecas()
    {
      Tabuleiro.ColocarPeca(new Torre(Cor.Branca, Tabuleiro), new PosicaoXadrez('a', 1).ToPosicao());
      Tabuleiro.ColocarPeca(new Torre(Cor.Branca, Tabuleiro), new PosicaoXadrez('a', 2).ToPosicao());
      Tabuleiro.ColocarPeca(new Torre(Cor.Branca, Tabuleiro), new PosicaoXadrez('b', 1).ToPosicao());
      Tabuleiro.ColocarPeca(new Torre(Cor.Branca, Tabuleiro), new PosicaoXadrez('c', 3).ToPosicao());
      Tabuleiro.ColocarPeca(new Rei(Cor.Preta, Tabuleiro), new PosicaoXadrez('f', 5).ToPosicao());
    }
  }
}