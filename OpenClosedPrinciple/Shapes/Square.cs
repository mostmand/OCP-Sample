using OpenClosedPrinciple.SourceGeneration.ShapeFactoryGeneration;

namespace OpenClosedPrinciple.Shapes;

[Shape(ShapeType = "SQUARE")]
internal class Square : IShape
{
    public void Print()
    {
        Console.WriteLine("Square");
    }
}