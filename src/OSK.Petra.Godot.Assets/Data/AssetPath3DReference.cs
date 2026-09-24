using Godot;
using OSK.Petra.Assets.Models;
using OSK.Petra.Godot.Assets.Models;

namespace OSK.Petra.Godot.Assets.Data;

/// <summary>
/// Represents a path to a 3D asset
/// </summary>
[Tool]
[GlobalClass]
public partial class AssetPath3DReference : Node3DAssetReferenceResource
{
    #region Variables

    [Export(PropertyHint.File)]
    private string _assetPath;

    #endregion

    #region AssetReference Overrides

    /// <inheritdoc/>
    public override string AssetPath => _assetPath;

    /// <inheritdoc/>
    public override IEntityAssetReference<Node3DTransform> GetAssetReference(EntityAssetIdentifier identifier)
        => new AssetPathReference<Node3DTransform>(_assetPath, identifier);

    #endregion
}
