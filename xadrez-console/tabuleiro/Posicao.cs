namespace tabuleiro
{
  public class Posicao(int linha, int coluna)
  {
    public int Linha { get; set; } = linha;
    public int Coluna { get; set; } = coluna;


    public void DefinirValores(int novaLinha, int novaColuna)
    {
      Linha = novaLinha;
      Coluna = novaColuna;
    }
    public override string ToString()
    {
      return $"{Linha}, {Coluna}";
    }
  }
}