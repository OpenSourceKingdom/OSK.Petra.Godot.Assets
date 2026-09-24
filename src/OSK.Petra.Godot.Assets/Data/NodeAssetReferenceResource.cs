using OSK.Petra.Assets.Models;

namespace OSK.Petra.Godot.Assets.Data;

/// <summary>
/// Represents a reference to a node resource
/// </summary>
/// <typeparam name="TTransform">The transform type the node uses</typeparam>
public abstract partial class NodeAssetReferenceResource<TTransform> : GameAssetReferenceResource
    where TTransform : ITransform
{
    /// <summary>
    /// The path to the asset
    /// </summary>
    public abstract string AssetPath { get; }

    /// <summary>
    /// Gets an asset reference that can be used within the asset system to instantiate the node
    /// </summary>
    /// <param name="identifier">The identifier for the entity</param>
    /// <returns>The reference to be used within the asset system</returns>
    public abstract IEntityAssetReference<TTransform> GetAssetReference(EntityAssetIdentifier identifier);
}
