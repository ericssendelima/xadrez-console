namespace tabuleiro
{
  public class Tabuleiro(int linhas, int colunas)
  {
    private Peca[,] _pecas = new Peca[linhas, colunas];
    public int Linhas { get; set; } = linhas;
    public int Colunas { get; set; } = colunas;

    public Peca Peca(int linha, int coluna)
    { 
      return _pecas[linha, coluna];
    }

    public void ColocarPeca(Peca peca, Posicao posicao)
    {
      _pecas[posicao.Linha, posicao.Coluna] = peca;
      peca.Posicao = posicao;
    }
  }
}