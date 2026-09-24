using Godot;
using OSK.Petra.Assets.Models;
using System.Collections.Generic;

namespace OSK.Petra.Godot.Assets.Data;

public abstract partial class NodeAssetDescriptor<TNode, TTransform>: EntityAssetDescriptor, IEntityDescriptor<TNode, TTransform>
    where TNode : Node
    where TTransform : ITransform
{
    #region IEntityDescriptor

    public abstract EntityAssetIdentifier AssetIdentifier { get; }

    public abstract IEnumerable<AssetTag> Tags { get; }

    public abstract IEntityAssetReference<TTransform> GetAssetReference();

    #endregion
}
