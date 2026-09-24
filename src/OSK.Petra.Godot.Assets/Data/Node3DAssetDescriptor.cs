using Godot;
using OSK.Petra.Assets.Models;
using OSK.Petra.Godot.Assets.Models;

namespace OSK.Petra.Godot.Assets.Data;

public abstract partial class Node3DAssetDescriptor<TNode>: NodeAssetDescriptor<TNode, Node3DTransform>
    where TNode : Node3D
{
    #region Variables

    [Export]
    private Node3DAssetReferenceResource _assetReference;

    #endregion

    #region NodeAssetDescriptor Overrides

    public override string AssetPath => _assetReference.AssetPath;

    public override IEntityAssetReference<Node3DTransform> GetAssetReference()
        => _assetReference.GetAssetReference(AssetIdentifier);

    #endregion
}
