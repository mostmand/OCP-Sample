namespace OpenClosedPrinciple;

internal interface IShapeFactory
{
    IShape CreateShape(string shapeType);
}