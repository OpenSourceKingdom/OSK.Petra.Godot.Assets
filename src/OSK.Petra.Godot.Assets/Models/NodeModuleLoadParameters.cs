using Godot;
using OSK.Petra.Assets.Models;
using OSK.Petra.Modules;
using System;

namespace OSK.Petra.Godot.Assets.Models;

public class NodeModuleLoadParameters(ModuleName moduleName, Guid? assetPackageId = null)
    : ModuleLoadParameters(moduleName, assetPackageId)
{
    public Node SceneRoot { get; init; }
}
