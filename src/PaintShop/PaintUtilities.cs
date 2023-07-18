namespace PaintShop;

// 3 - Crie uma classe estática PaintUtilities
// A classe PaintUtilities deve possuir uma propriedade pública BucketSizeLiters do tipo int com valor 20. Essa propriedade armazena quantos litros de tinta possui um balde.
// Lembre-se que todo membro da classe PaintUtilities deve ser estático. A atribuição do valor pode ser feita de forma direta ou por um construtor estático.
// A classe PaintUtilities deve possuir uma propriedade pública SquareMetersPerLiter do tipo int com valor 10. Essa propriedade armazena quantos metros quadrados podem ser pintados por litro
// Lembre-se que todo mebro da classe PaintUtilities deve ser estático. A atribuição do valor pode ser feita de forma direta ou por um construtor estático.
// A classe PaintUtilities deve possuir uma propriedade pública SquareMetersPerBucket do tipo int com um get explícito e sem um set
// O método get da propriedade SquareMetersPerBucket deve calcular a quantidade de metros quadrados que cada balde de tinta irá render utilizando os valores das propriedades BucketSizeLiters e SquareMetersPerLiter.
// A classe PaintUtilities deve possuir um método público GetNeededPaintBuckets() com três overloads: um que receba a área em m² com o tipo double, um que receba uma parede do tipo Wall e um que receba um cômodo do tipo Room e retorne a quantidade de tinta a ser usada em baldes como int.
// O método GetNeededPaintBuckets() é o principal método da nossa classe de utilidades. Por isso, ele deve conseguir trabalhar tanto com o valor da área em si quanto com paredes e cômodos.
// O valor deve ser retornado em baldes de tinta, não em litros.Para isso, considere aceitável que ao final da pintura haja alguma sobra, contanto que essa sobra seja menor que um balde inteiro.
// Dica pencil2: um overload acontece quando declaramos o mesmo método mais de uma vez, mas com parâmetros diferentes em cada uma das declarações.Assim, podemos escolher que tipo de argumento queremos passar (ou se não queremos passar nenhum).
// Como a tipagem do C# é estática, o compilador irá saber qual das versões do método usar a partir da forma como o invocamos.
// Dica 2 :peincil2::O método Math.Ceiling() do C# retorna o menor valor inteiro maior ou igual a um número com casas decimais. Documentação
// A classe PaintUtilities deve possuir um método público CalculateCost com dois parâmetros.O preimeiro é o preço do balde de tinta, em formato decimal. O segundo será dividido em três overloads iguais ao do GetNeededPaintBuckets() : área(double), parede(Wall) ou cômodo(Room). Retorne o custo da tinta como decimal.
// O método PaintUtilities deverá utilizar o método GetNeededPaintBuckets() para conseguir a quantidade de baldes a ser comprada e, com essa informação somada ao preço do balde passado no parâmetro, retornar o custo total da obra.

public static class PaintUtilities
{
  public static int BucketSizeLiters { get; set; } = 20;
  public static int SquareMetersPerLiter { get; set; } = 10;

  public static int SquareMetersPerBucket
  {
    get
    {
      return SquareMetersPerLiter * BucketSizeLiters;
    }
  }

  public static int GetNeededPaintBuckets(double area)
  {
    return Convert.ToInt32(Math.Ceiling(area / SquareMetersPerBucket));
  }

  public static int GetNeededPaintBuckets(Wall wall)
  {
    return Convert.ToInt32(Math.Ceiling(wall.PaintableArea / SquareMetersPerBucket));
  }

  public static int GetNeededPaintBuckets(Room room)
  {
    double totalArea = 0;
    foreach (var wall in room.walls)
    {
      totalArea += wall.PaintableArea;
    }
    return Convert.ToInt32(Math.Ceiling(totalArea / SquareMetersPerBucket));
  }

  public static decimal CalculateCost(decimal price, double area)
  {
    return price * GetNeededPaintBuckets(area);
  }

  public static decimal CalculateCost(decimal price, Wall wall)
  {
    return price * GetNeededPaintBuckets(wall);
  }

  public static decimal CalculateCost(decimal price, Room room)
  {
    return price * GetNeededPaintBuckets(room);
  }
}