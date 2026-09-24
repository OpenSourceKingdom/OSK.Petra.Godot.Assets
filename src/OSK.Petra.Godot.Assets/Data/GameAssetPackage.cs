using Godot;
using OSK.Petra.Assets;
using OSK.Petra.Assets.Models;
using OSK.Petra.Godot.Primitives.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OSK.Petra.Godot.Assets.Data;

/// <summary>
/// A unique set of assets and modules that can be utilized within an asset manager
/// </summary>
[GlobalClass]
public partial class GameAssetPackage : Resource
{
    #region Variables

    private Guid _identifier = AssetIdentifiers.DefaultPackageId;
    private string _packageName = "_default";

    [Export]
    private GuidIdentifier _packageId
    {
        get => new(_identifier);
        set
        {
            _identifier = value;
            UpdateCollections();
        }
    }

    /// <summary>
    /// The name of the package
    /// </summary>
    [Export]
    public string PackageName 
    {
        get => _packageName; 
        set
        {
            _packageName = value;
            UpdateCollections();
        }
    }

    /// <summary>
    /// A collection of game entities
    /// </summary>
    [Export]
    public EntityAssetCollection[] EntityCollections { get; set; }

    /// <summary>
    /// A collection of game modules
    /// </summary>
    [Export]
    public ModuleAssetCollection[] ModuleCollections { get; set; }

    #endregion

    #region Api

    /// <summary>
    /// Gets all the asset descriptors contianed within this package
    /// </summary>
    /// <returns>The entire descriptor collection within the package</returns>
    public IEnumerable<IAssetDescriptor> GetDescriptors()
        => EntityCollections.SelectMany(collection => collection.GetDescriptors()).Concat(ModuleCollections.SelectMany(collection => collection.GetDescriptors()));

    #endregion

    #region Helpers

    private void UpdateCollections()
    {
        _packageName = string.IsNullOrWhiteSpace(_packageName)
            ? "_default"
            : _packageName;

        GD.Print("Updating package ids. New: " + _packageId);

        UpdateCollection(_packageName, EntityCollections);
        UpdateCollection(_packageName, ModuleCollections);
    }

    private void UpdateCollection<TAsset>(string packageName, IEnumerable<GameAssetCollection<TAsset>> collections)
        where TAsset: GameAssetDescriptor
    {
        if (collections is null)
        {
            return;
        }
        foreach (var collection in collections)
        {
            if (collection is null)
            {
                continue;
            }

            collection.UpdateIdentifiers(_packageId, _packageName);
        }
    }

    #endregion
}
