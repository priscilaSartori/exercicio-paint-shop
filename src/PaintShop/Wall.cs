namespace PaintShop;

// 1 - Crie uma classe Wall
// A classe Wall não deve ser estática
// A classe Wall deve possuir uma propriedade pública Width do tipo double com um get implícito e um set explícito
// O método set da propriedade Width deve armazenar o valor recebido caso o mesmo seja maior do que 0. Se o valor for menor ou igual a 0, o valor armazenado deverá ser 1
// A classe Wall deve possuir uma propriedade pública Height do tipo double com um get implícito e um set explícito
// A classe Wall deve possuir uma propriedade pública ExcludedArea do tipo double com um get e set implícitos
// A classe Wall deve possuir uma propriedade pública PaintableArea do tipo double sem um set e com um get explícito
// A classe Wall deve possuir um construtor que receba um width como primeiro parâmetro e um height como segundo parâmetro
public class Wall
{
  double _width;
  double _height;

  public double Width
  {
    get { return _width; }
    set
    {
      if (value <= 0)
      {
        _width = 1;
      }
      else
      {
        _width = value;
      }
    }
  }
  public double Height
  {
    get { return _height; }
    set
    {
      if (value <= 0)
      {
        _height = 1;
      }
      else
      {
        _height = value;
      }
    }
  }
  public double ExcludedArea { get; set; }
  public double PaintableArea
  {
    get { return (Width * Height) - ExcludedArea; }
  }

  public Wall(double width, double height)
  {
    Width = width;
    Height = height;
  }
}