using OpenClosedPrinciple.Shapes;

namespace OpenClosedPrinciple.ShapeProvider;

public class TriangleProvider : IShapeProvider
{
    public string ShapeType => "TRIANGLE";
    public IShape CreateShape()
    {
        return new Triangle();
    }
}