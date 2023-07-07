using Xunit;
using System.IO;
using FluentAssertions;
using PaintShop;
using System;

namespace PaintShop.Test.Test;

public class TestReq1
{
    [Trait("Category", "1. Crie uma classe Wall")]
    [Theory(DisplayName = "Deve ter uma classe chamada Wall criada")]
    [InlineData(typeof(Wall))]
     public void TestClassWall(System.Type t){
        var attrs = t.GetProperties();
        Assert.Contains(attrs, item => item.Name == "Width");
        Assert.Contains(attrs, item => item.Name == "Height");
        Assert.Contains(attrs, item => item.Name == "ExcludedArea");
        Assert.Contains(attrs, item => item.Name == "PaintableArea");
    }

    [Trait("Category", "1. Crie uma classe Wall")]
    [Theory(DisplayName = "Deve ter uma propriedade chamada Width com um setter implementado")]
    [InlineData(2,3,2)]
    [InlineData(0,3,1)]
    [InlineData(2.5,3,2.5)]
    [InlineData(-10.4,3,1)]
    public void TestWallWidth(double width, double height, double assertWidth)
    {
        var wallInstance = new Wall(width, height);
        Assert.Equal(wallInstance.Width, assertWidth);
    }

    [Trait("Category", "1. Crie uma classe Wall")]
    [Theory(DisplayName = "Deve ter uma propriedade chamada Height com um setter implementado")]
    [InlineData(2,3,3)]
    [InlineData(3,0,1)]
    [InlineData(2.5,3.4,3.4)]
    [InlineData(1,-10.4,1)]
    public void TestWallHeight(double width, double height, double assertHeight)
    {
        var wallInstance = new Wall(width, height);
        Assert.Equal(wallInstance.Height, assertHeight);
    }

    [Trait("Category", "1. Crie uma classe Wall")]
    [Theory(DisplayName = "Deve ter uma propriedade chamada PaintableArea com um getter implementado")]
    [InlineData(3,4,1,11)]
    [InlineData(8.2, 4, 2.2, 30.6)]
    public void TestWallPaintableArea(double width, double height, double excludedArea, double mockPaintableArea) {
        var wallInstance = new Wall(width, height);
        wallInstance.ExcludedArea = excludedArea;
        Assert.Equal(Math.Round(wallInstance.PaintableArea,1), Math.Round(mockPaintableArea,1));

    }
}

public class TestReq2
{

    [Trait("Category", "2. Crie uma classe Room")]
    [Theory(DisplayName = "Deve ter uma classe chamada Room criada")]
    [InlineData(typeof(Room))]
    public void TestClassRoom(System.Type t){
        var attrs = t.GetProperties();
        Assert.Contains(attrs, item => item.Name == "walls");
    }

    [Trait("Category", "2. Crie uma classe Room")]
    [Theory(DisplayName = "Deve ter um método chamado paintableArea com um getter implementado")]
    [InlineData(6.7, 4, 1.3, 5.5, 4.4, 2.2, 47.5)]
    [InlineData(1, 2, 1, 1, 2, 1, 2)]
    [InlineData(56.2, 58.6, 0, 7.6, 8.5, 0, 3357.92)]
    public void TestClassRoomPaintableArea(double wall1W, double wall1H, double wall1E, double wall2W, double wall2H, double wall2E, double MockTotalPaintableArea){
        var wall1 = new Wall(wall1W, wall1H);
        wall1.ExcludedArea = wall1E;
        var wall2 = new Wall(wall2W, wall2H);
        wall2.ExcludedArea = wall2E;
        Wall[] walls = { wall1, wall2 };

        var room = new Room(walls);
        Assert.Equal(room.TotalPaintableArea, MockTotalPaintableArea);
        
    }
}

public class TestReq3
{
    [Trait("Category", "3. Crie uma classe estática PaintUtilities")]
    [Theory(DisplayName = "Deve ter uma classe chamada PaintUtilities criada")]
    [InlineData(typeof(PaintUtilities))]
    public void TestClassPaintUtilities(System.Type t){
        var attrs = t.GetProperties();
        Assert.Contains(attrs, item => item.Name == "BucketSizeLiters");
        Assert.Contains(attrs, item => item.Name == "SquareMetersPerLiter");
        Assert.Contains(attrs, item => item.Name == "SquareMetersPerBucket");
    }

    [Trait("Category", "3. Crie uma classe estática PaintUtilities")]
    [Theory(DisplayName = "Deve ter um atributo SquareMetersPerBucket com um getter implementado")]
    [InlineData(200)]
    public void TestClassPaintUtilitiesSquareMetersPerBucket(int MockSquareMetersPerBucket)
    {
        Assert.Equal(PaintUtilities.SquareMetersPerBucket, MockSquareMetersPerBucket);
    }
    
    [Trait("Category", "3. Crie uma classe estática PaintUtilities")]
    [Theory(DisplayName = "Deve ter um método GetNeededPaintBuckets com parâmetro de entrada de área")]
    [InlineData(200, 1)]
    [InlineData(400, 2)]
    [InlineData(450, 3)]
    [InlineData(400.1, 3)]
    [InlineData(0.0001, 1)]
    [InlineData(0, 0)]
    public void TestGetNeededPaintBuckets(double area, int buckets)
    {
        Assert.Equal(PaintUtilities.GetNeededPaintBuckets(area), buckets);
    }

    [Trait("Category", "3. Crie uma classe estática PaintUtilities")]
    [Theory(DisplayName = "Deve ter um método GetNeededPaintBuckets com parâmetro de entrada de paredes")]
    [InlineData(6.7, 4, 1.3, 1)]
    [InlineData(1, 2, 1, 1)]
    [InlineData(56.2, 58.6, 450.6, 15)]
    public void TestGetNeededPaintBucketsWall(double wallWidth, double wallHeight, double wallExcludedArea, int buckets)
    {
        var wall = new Wall(wallWidth, wallHeight);
        wall.ExcludedArea = wallExcludedArea;
        Assert.Equal(PaintUtilities.GetNeededPaintBuckets(wall), buckets);
    }

    [Trait("Category", "3. Crie uma classe estática PaintUtilities")]
    [Theory(DisplayName = "Deve ter um método GetNeededPaintBuckets com parâmetro de entrada de sala")]
    [InlineData(6.7, 4, 1.3, 5.5, 4.4, 2.2, 1)]
    [InlineData(0.001, 0.001, 0, 0.001, 0.001, 0, 1)]
    [InlineData(56.2, 58.6, 450.6, 430.7, 95.6, 600.2, 218)]
    public void TestGetNeededPaintBucketsRoom(double wall1Width, double wall1Height, double wall1ExcludedArea, double wall2Width, double wall2Height, double wall2ExcludedArea, int buckets)
    {
        var wall1 = new Wall(wall1Width, wall1Height);
        wall1.ExcludedArea = wall1ExcludedArea;
        var wall2 = new Wall(wall2Width, wall2Height);
        wall2.ExcludedArea = wall2ExcludedArea;
        Wall[] walls = { wall1, wall2 };

        var room = new Room(walls);
        Assert.Equal(PaintUtilities.GetNeededPaintBuckets(room), buckets);
    }

    [Trait("Category", "3. Crie uma classe estática PaintUtilities")]
    [Theory(DisplayName = "Deve ter um método CalculateCost com parâmetro de entrada de área")]
    [InlineData(69.40, 200, 69.40)]
    [InlineData(75.20, 400, 150.40)]
    [InlineData(31.50, 450, 94.50)]
    [InlineData(126.20, 400.1, 378.60)]
    [InlineData(98.50, 0.0001, 98.50)]
    [InlineData(71.40, 0, 0)]
    public void TestCalculateCost(decimal price, double area, decimal cost)
    {
        Assert.Equal(PaintUtilities.CalculateCost(price, area), cost);
    }

    [Trait("Category", "3. Crie uma classe estática PaintUtilities")]
    [Theory(DisplayName = "Deve ter um método CalculateCost com parâmetro de entrada de paredes")]
    [InlineData(6.7, 4, 1.3, 56.99, 56.99)]
    [InlineData(0.001, 0.001, 0, 29.31, 29.31)]
    [InlineData(56.2, 58.6, 450.6, 142.78, 2141.70)]
    public void TestCalculateCostWall(double wallWidth, double wallHeight, double wallExcludedArea, decimal price, decimal cost)
    {
        var wall = new Wall(wallWidth, wallHeight);
        wall.ExcludedArea = wallExcludedArea;
        Assert.Equal(PaintUtilities.CalculateCost(price, wall), cost);
    }

    [Trait("Category", "3. Crie uma classe estática PaintUtilities")]
    [Theory(DisplayName = "Deve ter um método CalculateCost com parâmetro de entrada de sala")]
    [InlineData(6.7, 4, 1.3, 5.5, 4.4, 2.2, 56.99, 56.99)]
    [InlineData(0.001, 0.001, 0, 0.001, 0.001, 0, 29.31, 29.31)]
    [InlineData(56.2, 58.6, 450.6, 430.7, 95.6, 600.2, 142.78, 31126.04)]
    public void TestCalculateCostRoom(double wall1Width, double wall1Height, double wall1ExcludedArea, double wall2Width, double wall2Height, double wall2ExcludedArea, decimal price, decimal cost)
    {
        var wall1 = new Wall(wall1Width, wall1Height);
        wall1.ExcludedArea = wall1ExcludedArea;
        var wall2 = new Wall(wall2Width, wall2Height);
        wall2.ExcludedArea = wall2ExcludedArea;
        Wall[] walls = { wall1, wall2 };

        var room = new Room(walls);
        Assert.Equal(PaintUtilities.CalculateCost(price, room), cost);
    }
      
}
