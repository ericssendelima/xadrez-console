using tabuleiro;
using xadrez;
using xadrez_console;

Tabuleiro tabuleiro = new(8, 8);

tabuleiro.ColocarPeca(new Torre(Cor.Amarela, tabuleiro), new Posicao(1, 4));
tabuleiro.ColocarPeca(new Rei(Cor.Branca, tabuleiro), new Posicao(6, 2));

Tela.ImprimirTabuleiro(tabuleiro);