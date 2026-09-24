using Godot;
using OSK.Petra.Assets.Models;
using OSK.Petra.Godot.Assets.Models;

namespace OSK.Petra.Godot.Assets.Data;

[Tool]
[GlobalClass]
public partial class AssetPath2DReference : Node2DAssetReferenceResource
{
    #region Variables

    [Export(PropertyHint.File)]
    private string _assetPath;

    #endregion

    #region AssetReference Overrides

    public override string AssetPath => _assetPath;

    public override IEntityAssetReference<Node2DTransform> GetAssetReference(EntityAssetIdentifier identifier)
        => new AssetPathReference<Node2DTransform>(_assetPath, identifier);

    #endregion
}
