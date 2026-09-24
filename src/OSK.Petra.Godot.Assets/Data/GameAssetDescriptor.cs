using Godot;
using System.Collections.Generic;
using System.Linq;
using OSK.Petra.Assets.Models;
using System.IO;
using FileAccess = Godot.FileAccess;
using System;

namespace OSK.Petra.Godot.Assets.Data;

public abstract partial class GameAssetDescriptor : Resource, IAssetDescriptor
{
    #region Variables

    [Export]
    private string _name;

    [Export]
    private string _description;

    [Export]
    private Texture2D _icon;

    [Export]
    private AssetTagResource[] _tags;

    private long? _size;

    #endregion

    #region IAssetDescriptor

    public string Name => _name;

    public string Description => _description;

    public abstract string AssetPath { get; }

    public string IconPath => _icon.ResourcePath;

    public IEnumerable<AssetTag> Tags => GetComputedAssetTags().Concat(_tags?.Select(tag => tag.GetTag()) ?? []).DistinctBy(tag => tag.Category);

    #endregion

    #region Public

    public Guid AssetPackageId { get; internal set; }

    public string AssetPackageName { get; internal set; }

    public string AssetCollectionName { get; internal set; }

    public Texture2D RawIcon => _icon;

    public long? Size
    {
        get
        {
            if (_size.HasValue)
            {
                return _size;
            }

            _size = !string.IsNullOrWhiteSpace(AssetPath) && FileAccess.FileExists(AssetPath) ? new FileInfo(AssetPath).Length : 0;

            return _size;
        }
    }

    #endregion

    #region Helpers

    private IEnumerable<AssetTag> GetComputedAssetTags()
        =>
        [
            new("AssetPackage", AssetPackageName),
            new(AssetTagCategories.Collection, AssetCollectionName)
        ];

    #endregion
}
