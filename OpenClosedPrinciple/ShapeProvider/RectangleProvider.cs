using OpenClosedPrinciple.Shapes;

namespace OpenClosedPrinciple.ShapeProvider;

internal class RectangleProvider : IShapeProvider
{
    public string ShapeType => "RECTANGLE";

    public IShape CreateShape()
    {
        return new Rectangle();
    }
}