using Godot;
using OSK.Petra.Assets.Ports;
using OSK.Petra.Modules;
using OSK.Petra.Godot.Assets.Models;
using OSK.Petra.Assets.Models;

namespace OSK.Petra.Godot.Assets;

public static class LoadManagerExtensions
{
    /// <summary>
    /// Performs a quick module replacement load for a basic node module
    /// </summary>
    /// <param name="service">The asset service to use</param>
    /// <param name="moduleName">The module to load</param>
    /// <param name="sceneRoot">The scene to add the module to</param>
    /// <returns>The load context for monitoring the load sequence</returns>
    public static IModuleLoadContext ReplaceModule(this IAssetService service, ModuleName moduleName, Node sceneRoot)
    {
        return service.LoadModule(new NodeModuleLoadParameters(moduleName)
        {
            FinalizationMode = ModuleFinalizationMode.Immediate,
            SceneRoot = sceneRoot,
            LoadBehavior = ModuleLoadBehavior.Replace
        });
    }
}
