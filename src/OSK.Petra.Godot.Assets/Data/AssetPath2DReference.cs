using Godot;
using OSK.Petra.Assets.Models;
using OSK.Petra.Godot.Assets.Models;

namespace OSK.Petra.Godot.Assets.Data;

/// <summary>
/// Describes a path to a 2D asset
/// </summary>
[Tool]
[GlobalClass]
public partial class AssetPath2DReference : Node2DAssetReferenceResource
{
    #region Variables

    [Export(PropertyHint.File)]
    private string _assetPath;

    #endregion

    #region AssetReference Overrides

    /// <inheritdoc/>
    public override string AssetPath => _assetPath;

    /// <inheritdoc/>
    public override IEntityAssetReference<Node2DTransform> GetAssetReference(EntityAssetIdentifier identifier)
        => new AssetPathReference<Node2DTransform>(_assetPath, identifier);

    #endregion
}
