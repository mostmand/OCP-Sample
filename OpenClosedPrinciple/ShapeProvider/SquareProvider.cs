using OpenClosedPrinciple.Shapes;

namespace OpenClosedPrinciple.ShapeProvider;

public class SquareProvider : IShapeProvider
{
    public string ShapeType => "SQUARE";

    public IShape CreateShape()
    {
        return new Square();
    }
}