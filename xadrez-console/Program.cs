using tabuleiro;
using xadrez;
using xadrez_console;

Tabuleiro tabuleiro = new(8, 8);

tabuleiro.ColocarPeca(new Torre(Cor.Preta, tabuleiro), new Posicao(0, 0));
tabuleiro.ColocarPeca(new Torre(Cor.Preta, tabuleiro), new Posicao(1, 3));
tabuleiro.ColocarPeca(new Rei(Cor.Preta, tabuleiro), new Posicao(2, 4));

Tela.ImprimirTabuleiro(tabuleiro);