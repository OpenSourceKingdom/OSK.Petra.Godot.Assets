using Godot;
using OSK.Petra.Assets.Models;
using OSK.Petra.Godot.Assets.Models;

namespace OSK.Petra.Godot.Assets.Data;

/// <summary>
/// Represents an asset template reference that uses a 3D scene
/// </summary>
[Tool]
[GlobalClass]
public partial class AssetScene3DReference: Node3DAssetReferenceResource
{
    #region Variables

    [Export]
    private PackedScene _packedScene;

    #endregion

    #region AssetReference Overrides

    /// <inheritdoc/>
    public override string AssetPath => _packedScene.ResourcePath;

    /// <inheritdoc/>
    public override IEntityAssetReference<Node3DTransform> GetAssetReference(EntityAssetIdentifier identifier)
        => new AssetTemplateReference<PackedScene, Node3DTransform>(_packedScene, identifier);

    #endregion
}
