using OpenClosedPrinciple.Shapes;

namespace OpenClosedPrinciple;

internal class OriginalShapeFactory : IShapeFactory
{
    public IShape CreateShape(string shapeType)
    {
        return shapeType switch
        {
            "CIRCLE" => new Circle(),
            "RECTANGLE" => new Rectangle(),
            "SQUARE" => new Square(),
            _ => throw new NotSupportedException()
        };
    }
}