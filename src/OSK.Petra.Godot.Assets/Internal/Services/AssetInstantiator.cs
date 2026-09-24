using Godot;
using System;
using OSK.Petra.Godot.Assets.Models;
using OSK.Petra.Assets.Models;

namespace OSK.Petra.Godot.Assets.Internal.Services;

/// <summary>
/// A generic instantiator to help share base instantiation logic among different instantiators
/// </summary>
public abstract class AssetInstantiator
{
    /// <summary>
    /// Instantiates a packed scene into a usable entity within the asset system
    /// </summary>
    /// <typeparam name="TEntity">The type of entity being instantiated</typeparam>
    /// <typeparam name="TTransform">The type of transform the entity utilizes</typeparam>
    /// <param name="packedScene">The packed scene that is able to instantiate the entity</param>
    /// <param name="transform">The transform that describes the spatial information for the asset being instantiated</param>
    /// <param name="configurator">An action that can further configure an entity prior to its addition to a game's module</param>
    /// <returns>The entity that was insntantiated</returns>
    /// <exception cref="InvalidOperationException">If the transform and node expectations do not match</exception>
    /// <exception cref="NotSupportedException">If the node is not a known type to this instantiator</exception>
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
