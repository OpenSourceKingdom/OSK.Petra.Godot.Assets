using OSK.Petra.Assets.Models;
using OSK.Petra.Assets.Options;
using OSK.Petra.Assets.Ports;
using System.Collections.Generic;

namespace OSK.Petra.Godot.Assets;

public static class AssetServiceExtensions
{
    public static IEnumerable<IEntityDescriptor> GetEntityCollection(this IAssetService assetService, params string[] collectionNames)
        => assetService.GetEntityDescriptors(new AssetSearchOptions().WithTagFilter(AssetTagCategories.Collection, collectionNames));

    public static IEnumerable<IModuleDescriptor> GetModuleCollection(this IAssetService assetService, params string[] collectionNames)
        => assetService.GetModuleDescriptors(new AssetSearchOptions().WithTagFilter(AssetTagCategories.Collection, collectionNames));
}
