using OpenClosedPrinciple.Shapes;

namespace OpenClosedPrinciple.ShapeProvider;

internal class CircleProvider : IShapeProvider
{
    public string ShapeType => "CIRCLE";

    public IShape CreateShape()
    {
        return new Circle();
    }
}