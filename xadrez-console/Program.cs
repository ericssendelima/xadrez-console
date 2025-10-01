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
    Console.Clear();
    Tela.ImprimirTabuleiro(partidaDeXadrez.Tabuleiro);

    Console.WriteLine();
    Console.Write("Origem: ");
    Posicao origem = Tela.LerPosicaoXadrez().ToPosicao();
    Console.Write("Destino: ");
    Posicao destino = Tela.LerPosicaoXadrez().ToPosicao();

    partidaDeXadrez.ExecutaMovimento(origem, destino);
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
