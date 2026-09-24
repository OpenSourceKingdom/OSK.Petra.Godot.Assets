using OSK.Petra.Assets.Models;

namespace OSK.Petra.Godot.Assets.Data;

public abstract partial class NodeAssetReferenceResource<TTransform> : GameAssetReferenceResource
    where TTransform : ITransform
{
    public abstract string AssetPath { get; }

    public abstract IEntityAssetReference<TTransform> GetAssetReference(EntityAssetIdentifier identifier);
}
