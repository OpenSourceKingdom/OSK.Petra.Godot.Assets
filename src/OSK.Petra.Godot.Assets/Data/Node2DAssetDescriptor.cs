using Godot;
using OSK.Petra.Assets.Models;
using OSK.Petra.Godot.Assets.Models;

namespace OSK.Petra.Godot.Assets.Data;

public abstract partial class Node2DAssetDescriptor<TNode> : NodeAssetDescriptor<TNode, Node2DTransform>
    where TNode : Node2D
{
    #region Variables

    [Export]
    private Node2DAssetReferenceResource _assetReference;

    #endregion

    #region NodeAssetDescriptor Overrides

    public override string AssetPath => _assetReference.AssetPath;

    public override IEntityAssetReference<Node2DTransform> GetAssetReference()
        => _assetReference.GetAssetReference(AssetIdentifier);

    #endregion
}
