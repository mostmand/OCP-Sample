namespace OpenClosedPrinciple.ShapeProvider;

internal interface IShapeProvider
{
    string ShapeType { get; }
    IShape CreateShape();
}