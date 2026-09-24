using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using OSK.Petra.Godot.Assets.Models;

namespace OSK.Petra.Godot.Assets.Data;

/// <summary>
/// A collection of some game asset
/// </summary>
/// <typeparam name="TDescriptor">The descriptor type the underlying assets use</typeparam>
public abstract partial class GameAssetCollection<TDescriptor>: Resource
    where TDescriptor: GameAssetDescriptor
{
    #region Variables0

    private Guid _assetPackageId;
    private string _assetPackageName;

    private string _name;

    /// <summary>
    /// The unique name for this collection
    /// </summary>
    [Export(PropertyHint.Enum)]
    public string Name
    {
        get => _name;
        set
        {
            if (_name is not null && _name.Equals(value, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            _name = value;
            UpdateIdentifiers();
        }
    }

    #endregion

    #region Godot Overrides

    /// <inheritdoc/>
    public override void _ValidateProperty(Dictionary property)
    {
        if (property["name"].AsStringName() == nameof(Name))
        {
            var collectionNames = TypeHelper.GetStrongTypeList<AssetCollectionName>().Select(collection => collection.Name);
            string hint = string.Join(",", collectionNames);
            property["hint_string"] = hint;
        }
    }

    #endregion

    #region Helpers

    /// <summary>
    /// Updates the collection and its underlying entities to the specified package id and name
    /// </summary>
    /// <param name="packageId">The package id to set the collection to</param>
    /// <param name="packageName">The name of the package</param>
    public void UpdateIdentifiers(Guid packageId, string packageName)
    {
        _assetPackageId = packageId;
        _assetPackageName = packageName;

        UpdateIdentifiers();
    }

    /// <summary>
    /// Gets the descriptors associated with the assets
    /// </summary>
    /// <returns>The asset descriptors</returns>
    public abstract IEnumerable<GameAssetDescriptor> GetDescriptors();

    /// <summary>
    /// Informs the collection to update its identifiers
    /// </summary>
    protected void UpdateIdentifiers()
    {
        var descriptors = GetDescriptors();
        if (descriptors == null || string.IsNullOrWhiteSpace(_name))
        {
            return;
        }

        var packageId = string.IsNullOrWhiteSpace(_assetPackageName) ? "_default" : _assetPackageName;
        GD.Print($"Updating identifiers: Collection: {_name} Package: {packageId}");

        foreach (var descriptor in descriptors)
        {
            descriptor?.AssetPackageId = _assetPackageId;
            descriptor?.AssetPackageName = _assetPackageName;
            descriptor?.AssetCollectionName = _name;
        }
    }

    #endregion
}
