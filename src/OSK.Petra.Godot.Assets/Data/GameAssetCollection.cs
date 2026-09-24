using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using OSK.Petra.Godot.Assets.Models;

namespace OSK.Petra.Godot.Assets.Data;

public abstract partial class GameAssetCollection<TDescriptor>: Resource
    where TDescriptor: GameAssetDescriptor
{
    #region Variables0

    private Guid _assetPackageId;
    private string _assetPackageName;

    private string _name;

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

    public void UpdateIdentifiers(Guid packageId, string packageName)
    {
        _assetPackageId = packageId;
        _assetPackageName = packageName;

        UpdateIdentifiers();
    }

    public abstract IEnumerable<GameAssetDescriptor> GetDescriptors();

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
