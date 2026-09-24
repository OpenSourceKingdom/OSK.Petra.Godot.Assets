using OSK.Petra.Godot.Assets.Data;
using OSK.Petra.Godot.Assets.Models;
using OSK.Petra.Godot.Modules.Scripts;

namespace OSK.Petra.Godot.Assets.Internal.Services;

internal class DefaultModuleWorkflowLoader(ModuleAssetDescriptor descriptor, NodeModuleLoadParameters parameters) :
    NodeModuleLoader<GameModule, ModuleAssetDescriptor, NodeModuleLoadParameters>(descriptor, parameters)
{
    protected override void DisposeLoader()
    {
    }
}
