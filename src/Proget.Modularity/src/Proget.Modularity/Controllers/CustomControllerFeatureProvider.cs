namespace Proget.Modularity.Controllers;

internal sealed class CustomControllerFeatureProvider : ControllerFeatureProvider
{
    protected override bool IsController(TypeInfo typeInfo)
    {
        if (typeInfo.IsAbstract)
        {
            return false;
        }

        if (typeInfo.ContainsGenericParameters)
        {
            return false;
        }

        if (typeInfo.IsDefined(typeof(NonControllerAttribute)))
        {
            return false;
        }

        var hasSuffix = typeInfo.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase);
        var hasAttribute = typeInfo.IsDefined(typeof(ControllerAttribute));

        
        if (!hasSuffix && !hasAttribute)
        {
            return false;
        }

        return true;
    }
}