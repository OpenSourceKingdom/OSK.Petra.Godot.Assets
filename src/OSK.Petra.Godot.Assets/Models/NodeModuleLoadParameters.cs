using Godot;
using OSK.Petra.Assets.Models;
using OSK.Petra.Assets.Ports;
using OSK.Petra.Modules;
using System;

namespace OSK.Petra.Godot.Assets.Models;

/// <summary>
/// A base class for expected load parameters for node module style <see cref="IModuleLoader"/>
/// </summary>
/// <param name="moduleName">The module name</param>
/// <param name="assetPackageId">The asset package the module belongs to</param>
public class NodeModuleLoadParameters(ModuleName moduleName, Guid? assetPackageId = null)
    : ModuleLoadParameters(moduleName, assetPackageId)
{
    /// <summary>
    /// The scene root in the godot scene that will have the module added to
    /// </summary>
    public Node SceneRoot { get; init; }
}
