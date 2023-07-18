namespace PaintShop;

// 2 - Crie uma classe Room
// A classe Room não deve ser estática
// A classe Room deve possuir uma propriedade pública Walls que seja um array de objetos do tipo Wall que criamos no requisito 2 (ou seja, um Wall[]). Essa propriedade não deve possuir um set.
// Ao não atribuirmos um setter a essa propriedade, ela poderá ser atribuída apenas na inicialização, o que faz sentido, pois não deve ser possível alterar a quantidade de paredes de um cômodo sem que ele vire outro cômodo diferente.
// A classe Room deve possuir um construtor que receba um Wall[] e o atribua à propriedade Walls
// A classe Room deve possuir uma propriedade pública TotalPaintableArea do tipo double com um get explícito e sem um set que represente toda a superfície a ser pintada
// O método get da propriedade TotalPaintableArea deve calcular a soma de todas as áreas a serem pintadas de cada parede(Wall) contida no quarto(Room)
public class Room
{
  public Wall[] walls { get; }
  public double TotalPaintableArea
  {
    get
    {
      double totalArea = 0;
      foreach (var wall in walls)
      {
        totalArea += wall.PaintableArea;
      }
      return totalArea;
    }
  }

  public Room(params Wall[] walls)
  {
    this.walls = walls;
  }
}