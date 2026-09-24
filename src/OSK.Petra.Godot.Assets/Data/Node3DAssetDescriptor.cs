using Godot;
using OSK.Petra.Assets.Models;
using OSK.Petra.Godot.Assets.Models;

namespace OSK.Petra.Godot.Assets.Data;

/// <summary>
/// A descriptor that is meant to be used with a <see cref="Node3D"/> asset
/// </summary>
/// <typeparam name="TNode">The type of node the descriptor refers to</typeparam>
public abstract partial class Node3DAssetDescriptor<TNode>: NodeAssetDescriptor<TNode, Node3DTransform>
    where TNode : Node3D
{
    #region Variables

    [Export]
    private Node3DAssetReferenceResource _assetReference;

    #endregion

    #region NodeAssetDescriptor Overrides

    /// <inheritdoc/>
    public override string AssetPath => _assetReference.AssetPath;

    /// <inheritdoc/>
    public override IEntityAssetReference<Node3DTransform> GetAssetReference()
        => _assetReference.GetAssetReference(AssetIdentifier);

    #endregion
}
