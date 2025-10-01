using tabuleiro;
using xadrez;
using xadrez_console;

try
{
  Tabuleiro tabuleiro = new(8, 8);

  tabuleiro.ColocarPeca(new Torre(Cor.Preta, tabuleiro), new Posicao(0, 0));
  tabuleiro.ColocarPeca(new Torre(Cor.Preta, tabuleiro), new Posicao(1, 3));
  tabuleiro.ColocarPeca(new Rei(Cor.Preta, tabuleiro), new Posicao(1, 3));

  Tela.ImprimirTabuleiro(tabuleiro);
}
catch (TabuleiroException e)
{
  Console.WriteLine(e.Message);
}
