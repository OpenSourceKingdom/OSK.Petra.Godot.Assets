using Microsoft.Extensions.DependencyInjection;
using OSK.Petra.Assets;

namespace OSK.Petra.Godot.Assets;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the asset system to the DI container for use within Godot
    /// </summary>
    /// <param name="services">The services to add the dependencies to</param>
    /// <returns>The services for chaining</returns>
    public static IServiceCollection AddGodotAssets(this IServiceCollection services)
    {
        services.AddAssets();

        return services;
    }
}
