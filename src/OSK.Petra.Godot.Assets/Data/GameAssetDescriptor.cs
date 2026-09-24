using Godot;
using System.Collections.Generic;
using System.Linq;
using OSK.Petra.Assets.Models;
using System.IO;
using FileAccess = Godot.FileAccess;
using System;

namespace OSK.Petra.Godot.Assets.Data;

/// <summary>
/// A descriptor for a general game asset within godot
/// </summary>
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

    /// <inheritdoc/>
    public string Name => _name;

    /// <inheritdoc/>
    public string Description => _description;

    /// <inheritdoc/>
    public abstract string AssetPath { get; }

    /// <inheritdoc/>
    public string IconPath => _icon.ResourcePath;

    /// <inheritdoc/>
    public IEnumerable<AssetTag> Tags => GetComputedAssetTags().Concat(_tags?.Select(tag => tag.GetTag()) ?? []).DistinctBy(tag => tag.Category);

    /// <inheritdoc/>
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

    #region Public

    /// <summary>
    /// The package id this asset belongs to
    /// </summary>
    public Guid AssetPackageId { get; internal set; }

    /// <summary>
    /// The package name that this asset belongs to
    /// </summary>
    public string AssetPackageName { get; internal set; }

    /// <summary>
    /// The package collection this asset belongs to
    /// </summary>
    public string AssetCollectionName { get; internal set; }

    /// <summary>
    /// The icon data that the asset uses
    /// </summary>
    public Texture2D RawIcon => _icon;

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
