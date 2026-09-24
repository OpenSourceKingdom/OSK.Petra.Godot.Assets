using Godot;
using OSK.Petra.Assets.Models;
using OSK.Petra.Assets.Options;
using OSK.Petra.Assets.Ports;
using OSK.Petra.Godot.Assets.Models;
using OSK.Petra.Modules;
using System.Collections.Generic;

namespace OSK.Petra.Godot.Assets;

public static class AssetServiceExtensions
{
    #region Collection

    /// <summary>
    /// Gets an enumeration of descriptors that are within the specified <see cref="AssetTagCategories.Collection"/>
    /// </summary>
    /// <param name="assetService">The asset service to call</param>
    /// <param name="tagValues">An optional filter for specific values within the collection</param>
    /// <returns>The enumeration of descriptors that are in the specified collection and meet the filter</returns>
    public static IEnumerable<IEntityDescriptor> GetEntityCollection(this IAssetService assetService, params string[] tagValues)
        => assetService.GetEntityDescriptors(new AssetSearchOptions().WithTagFilter(AssetTagCategories.Collection, tagValues));

    /// <summary>
    /// Gets an enumeration of module descriptors that are within the specified <see cref="AssetTagCategories.Collection"/>
    /// </summary>
    /// <param name="assetService">The asset service to call</param>
    /// <param name="tagValues">An optional filter for specific values within the collection</param>
    /// <returns>The enumeration of module descriptors that are in the specified collection and meet the filter</returns>
    public static IEnumerable<IModuleDescriptor> GetModuleCollection(this IAssetService assetService, params string[] tagValues)
        => assetService.GetModuleDescriptors(new AssetSearchOptions().WithTagFilter(AssetTagCategories.Collection, tagValues));

    #endregion

    #region Replace

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

    #endregion
}
