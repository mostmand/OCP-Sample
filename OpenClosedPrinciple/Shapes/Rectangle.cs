using OpenClosedPrinciple.SourceGeneration.ShapeFactoryGeneration;

namespace OpenClosedPrinciple.Shapes;

[Shape(ShapeType = "RECTANGLE")]
internal class Rectangle : IShape
{
    public void Print()
    {
        Console.WriteLine("Rectangle");
    }
}