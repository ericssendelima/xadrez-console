using tabuleiro;
using xadrez;
using xadrez_console;

//Meu cmd está com os caracteres na cor verde e fundo preto
ConsoleColor aux = Console.ForegroundColor;
Console.ForegroundColor = ConsoleColor.White;


try
{
  PartidaDeXadrez partidaDeXadrez = new();

  while (!partidaDeXadrez.Terminada)
  {
    try
    {
      Console.Clear();
      Tela.ImprimirPartida(partidaDeXadrez);
     

      Console.WriteLine();
      Console.Write("Origem: ");
      Posicao origem = Tela.LerPosicaoXadrez().ToPosicao();
      partidaDeXadrez.ValidarPosicaoDeOrigem(origem);

      bool[,] movimentosPossiveis = partidaDeXadrez.Tabuleiro.Peca(origem).MovimentosPossiveis();

      Console.Clear();
      Tela.ImprimirTabuleiro(partidaDeXadrez.Tabuleiro, movimentosPossiveis);

      Console.WriteLine();
      Console.Write("Destino: ");
      Posicao destino = Tela.LerPosicaoXadrez().ToPosicao();
      partidaDeXadrez.ValidarPosicaoDeDestino(origem, destino);

      partidaDeXadrez.RealizaJogada(origem, destino);
    }
    catch (TabuleiroException e)
    {
      Console.WriteLine(e.Message);
      Console.ReadLine();//Aguarda o Enter pra repetir o bloco acima
    }
  }
}
catch (TabuleiroException e)
{
  Console.WriteLine(e.Message);
}
// catch (Exception e)
// {
//   Console.WriteLine(e.Message);
// }

Console.ForegroundColor = aux;
