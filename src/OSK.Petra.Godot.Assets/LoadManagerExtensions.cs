using Godot;
using OSK.Petra.Assets.Ports;
using OSK.Petra.Modules;
using OSK.Petra.Godot.Assets.Models;
using OSK.Petra.Assets.Models;

namespace OSK.Petra.Godot.Assets;

public static class LoadManagerExtensions
{
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
