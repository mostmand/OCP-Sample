using OpenClosedPrinciple.SourceGeneration.ShapeFactoryGeneration;

namespace OpenClosedPrinciple;

internal static class Program
{
    public static void Main()
    {
        var shapePrinter = new PolymorphicShapePrinter(new GeneratedShapeFactory());
        shapePrinter.Print("RECTANGLE");
    }
}