using System.Reflection;

namespace ApiTemplate.Config.IoC;

public interface IEndpoint
{
    void Register(WebApplication app);
}

public static class EnpointExtensions
{
    public static void MapEndpoints(this WebApplication app)
    {
        var types = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(x => x.GetInterfaces().Any(x => x == typeof(IEndpoint)) && x.IsClass);
        foreach(var type in types)
        {
            var endpoint = (IEndpoint?)Activator.CreateInstance(type);
            endpoint?.Register(app);
        }
    }
}