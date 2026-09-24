using Godot;
using OSK.Petra.Assets.Models;
using OSK.Petra.Godot.Assets.Models;

namespace OSK.Petra.Godot.Assets.Data;

/// <summary>
/// A descriptor that is meant to be used with a <see cref="Node2D"/> asset
/// </summary>
/// <typeparam name="TNode">The node type that the descriptor refers to</typeparam>
public abstract partial class Node2DAssetDescriptor<TNode> : NodeAssetDescriptor<TNode, Node2DTransform>
    where TNode : Node2D
{
    #region Variables

    [Export]
    private Node2DAssetReferenceResource _assetReference;

    #endregion

    #region NodeAssetDescriptor Overrides

    /// <inheritdoc/>
    public override string AssetPath => _assetReference.AssetPath;

    /// <inheritdoc/>
    public override IEntityAssetReference<Node2DTransform> GetAssetReference()
        => _assetReference.GetAssetReference(AssetIdentifier);

    #endregion
}
