using OpenClosedPrinciple.SourceGeneration.ShapeFactoryGeneration;

namespace OpenClosedPrinciple.Shapes;

[Shape(ShapeType = "CIRCLE")]
internal class Circle : IShape
{
    public void Print()
    {
        Console.WriteLine("Circle");
    }
}