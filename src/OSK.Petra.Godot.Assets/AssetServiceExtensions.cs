using OSK.Petra.Assets.Models;
using OSK.Petra.Assets.Options;
using OSK.Petra.Assets.Ports;
using System.Collections.Generic;

namespace OSK.Petra.Godot.Assets;

public static class AssetServiceExtensions
{
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
}
