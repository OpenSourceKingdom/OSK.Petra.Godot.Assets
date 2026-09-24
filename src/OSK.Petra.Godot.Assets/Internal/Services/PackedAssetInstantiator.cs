using Godot;
using OSK.Petra.Assets.Ports;
using System;
using OSK.Petra.Assets.Models;

namespace OSK.Petra.Godot.Assets.Internal.Services;

internal class PackedAssetInstantiator<TEntity, TTransform>(PackedScene packedScene, bool cleanupOnDisposal = false): AssetInstantiator, IAssetInstantiator<TEntity, TTransform>
    where TEntity: class
    where TTransform: ITransform
{
    #region IAssetInstantiator

    public TEntity Instantiate(TTransform transform, Action<TEntity>? configurator = null)
        => Instantiate<TEntity, TTransform>(packedScene, transform, configurator);

    public void Dispose()
    {
        if (cleanupOnDisposal)
        {
            packedScene.Dispose();
        }
    }

    #endregion
}
