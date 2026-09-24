using Microsoft.Extensions.DependencyInjection;
using OSK.Petra.Assets;

namespace OSK.Petra.Godot.Assets;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGodotAssets(this IServiceCollection services)
    {
        services.AddAssets();

        return services;
    }
}
