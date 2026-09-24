using Godot;
using OSK.Petra.Assets.Models;
using System.Collections.Generic;

namespace OSK.Petra.Godot.Assets.Data;

/// <summary>
/// An entity asset that uses a Godot <see cref="Node"/>
/// </summary>
/// <typeparam name="TNode">The type of node the asset uses</typeparam>
/// <typeparam name="TTransform">The type of transform expected to be used with the asset</typeparam>
public abstract partial class NodeAssetDescriptor<TNode, TTransform>: EntityAssetDescriptor, IEntityDescriptor<TNode, TTransform>
    where TNode : Node
    where TTransform : ITransform
{
    #region IEntityDescriptor

    /// <inheritdoc/>
    public abstract IEntityAssetReference<TTransform> GetAssetReference();

    #endregion
}
