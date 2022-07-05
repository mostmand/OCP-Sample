using OpenClosedPrinciple.ShapeProvider;

namespace OpenClosedPrinciple;

internal class EnhancedShapeFactory : IShapeFactory
{
    private static readonly IReadOnlyDictionary<string, IShapeProvider> ProviderByType = FindProviders();

    private static IReadOnlyDictionary<string, IShapeProvider> FindProviders()
    {
        var shapeProviders = typeof(IAssemblyMarkerInterface).Assembly
            .DefinedTypes
            .Where(type => !type.IsAbstract &&
                           !type.IsInterface &&
                           typeof(IShapeProvider).IsAssignableFrom(type))
            .Select(Activator.CreateInstance)
            .Cast<IShapeProvider>();

        return shapeProviders.ToDictionary(shapeProvider => shapeProvider.ShapeType);
    }

    public IShape CreateShape(string shapeType)
    {
        if (!ProviderByType.TryGetValue(shapeType, out var shapeProvider))
        {
            throw new NotSupportedException();
        }

        return shapeProvider.CreateShape();
    }
}