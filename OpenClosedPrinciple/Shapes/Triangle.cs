using OpenClosedPrinciple.SourceGeneration.ShapeFactoryGeneration;

namespace OpenClosedPrinciple.Shapes;

[Shape(ShapeType = "TRIANGLE")]
public class Triangle : IShape
{
    public void Print()
    {
        Console.WriteLine("Triangle");
    }
}