namespace tabuleiro
{
  public class Tabuleiro(int linhas, int colunas)
  {
    private Peca[,] _pecas;
    public int Linhas { get; set; } = linhas;
    public int Colunas { get; set; } = colunas;
  }
}