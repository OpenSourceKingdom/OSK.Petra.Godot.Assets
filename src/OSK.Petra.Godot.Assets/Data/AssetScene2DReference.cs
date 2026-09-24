using Godot;
using OSK.Petra.Assets.Models;
using OSK.Petra.Godot.Assets.Models;

namespace OSK.Petra.Godot.Assets.Data;

/// <summary>
/// Represents an asset template reference that uses a 2D scene
/// </summary>
[Tool]
[GlobalClass]
public partial class AssetScene2DReference : Node2DAssetReferenceResource
{
    #region Variables

    [Export]
    private PackedScene _packedScene;

    #endregion

    #region AssetReference Overrides

    /// <inheritdoc/>
    public override string AssetPath => _packedScene.ResourcePath;

    /// <inheritdoc/>
    public override IEntityAssetReference<Node2DTransform> GetAssetReference(EntityAssetIdentifier identifier)
        => new AssetTemplateReference<PackedScene, Node2DTransform>(_packedScene, identifier);

    #endregion
}
