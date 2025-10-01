using tabuleiro;

namespace xadrez
{
  public class PartidaDeXadrez
  {
    private int _turno;

    private Cor _jogadorAtual;
    public Tabuleiro Tabuleiro { get; private set; }
    public bool Terminada { get; private set; }

    public PartidaDeXadrez()
    {
      Tabuleiro = new(8, 8);
      _turno = 1;
      _jogadorAtual = Cor.Branca;
      ColocarPecas();
      Terminada = false;
    }

    public void ExecutaMovimento(Posicao origem, Posicao destino)
    {
      Peca pecaDaJogada = Tabuleiro.RetirarPeca(origem);
      // pecaDaJogada.IncrementarQteMovimentos();
      Peca pecaCapturada = Tabuleiro.RetirarPeca(destino);

      Tabuleiro.ColocarPeca(pecaDaJogada, destino);
    }

    private void ColocarPecas()
    {
      Tabuleiro.ColocarPeca(new Torre(Cor.Branca, this.Tabuleiro), new PosicaoXadrez('c', 1).ToPosicao());
      Tabuleiro.ColocarPeca(new Rei(Cor.Preta, this.Tabuleiro), new PosicaoXadrez('f', 8).ToPosicao());
    }
  }
}