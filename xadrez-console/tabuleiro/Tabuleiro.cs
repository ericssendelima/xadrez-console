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
    public Peca Peca(Posicao posicao)
    {
      return _pecas[posicao.Linha, posicao.Coluna];
    }

    public void ColocarPeca(Peca peca, Posicao posicao)
    {
      if (!ExistePeca(posicao))
      {
        _pecas[posicao.Linha, posicao.Coluna] = peca;
        peca.Posicao = posicao;
      }
      else
      {
        throw new TabuleiroException("Já existe uma peça nesta posição!");
      }
    }

    public Peca RetirarPeca(Posicao posicao)
    {
      if (!ExistePeca(posicao))
      {
        return null;
      }

      Peca aux = Peca(posicao);
      aux.Posicao = null;
      _pecas[posicao.Linha, posicao.Coluna] = null;

      return aux;
    }

    public bool ExistePeca(Posicao posicao)
    {
      ValidarPosicao(posicao);
      return Peca(posicao.Linha, posicao.Coluna) != null;
    }

    public bool PosicaoEhValida(Posicao posicao)
    {
      if (posicao.Linha < 0 || posicao.Linha >= Linhas || posicao.Coluna < 0 || posicao.Coluna >= Colunas)
      {
        return false;
      }
      return true;
    }

    public void ValidarPosicao(Posicao posicao)
    {
      if (!PosicaoEhValida(posicao))
      {
        throw new TabuleiroException("Posição inválida!");
      }
    }
  }
}