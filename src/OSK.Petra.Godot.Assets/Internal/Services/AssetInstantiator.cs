using Godot;
using System;
using OSK.Petra.Godot.Assets.Models;
using OSK.Petra.Assets.Models;

namespace OSK.Petra.Godot.Assets.Internal.Services;

public abstract class AssetInstantiator
{
    protected TEntity Instantiate<TEntity, TTransform>(PackedScene packedScene, TTransform transform, Action<TEntity> configurator = null)
        where TEntity: class
        where TTransform: ITransform
    {
        var entity = packedScene.Instantiate<TEntity>();
        configurator?.Invoke(entity);

        switch (transform)
        {
            case Node2DTransform node2DTransform:
                if (entity is not Node2D node2DEntity)
                {
                    throw new InvalidOperationException("An attempt was made to instantiate a node with a 2D Transform it was not compatible with");
                }

                node2DTransform.Parent?.AddChild(node2DEntity);
                node2DEntity.Position = node2DTransform.Position;
                node2DEntity.Rotation = node2DTransform.Rotation;

                return entity;
            case Node3DTransform node3DTransform:
                if (entity is not Node3D node3DEntity)
                {
                    throw new InvalidOperationException("An attempt was made to instantiate a node with a 3D Transform it was not compatible with");
                }

                node3DTransform.Parent?.AddChild(node3DEntity);
                node3DEntity.Position = node3DTransform.Position;
                node3DEntity.Rotation = node3DTransform.Rotation;

                return entity;
            default:
                throw new NotSupportedException($"Node transform of type {transform.GetType()} is not supported.");
        }
    }
}
