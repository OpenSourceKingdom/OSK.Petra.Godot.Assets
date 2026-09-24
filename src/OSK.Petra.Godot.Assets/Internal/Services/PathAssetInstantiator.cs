using Godot;
using OSK.Operations.Outputs;
using OSK.Operations.Outputs.Models;
using OSK.Operations.Workflows.Models;
using OSK.Operations.Workflows.Tasks;
using OSK.Operations.Workflows.Tasks.Sequential;
using OSK.Petra.Assets.Ports;
using OSK.Petra.Godot.Operations.Workflows.Tasks.Resources;
using System;
using OSK.Petra.Assets.Models;

namespace OSK.Petra.Godot.Assets.Internal.Services;

internal class PathAssetInstantiator<TEntity, TTransform>(string path, bool allowMultiThreadedLoads): AssetInstantiator, IAssetInstantiator<TEntity, TTransform>
    where TEntity: class
    where TTransform: ITransform
{
    #region Variables

    private PackedScene _packedScene;

    #endregion

    #region IAssetInstantiator

    public void Dispose()
    {
        _packedScene.Dispose();
    }

    public TEntity Instantiate(TTransform transform, Action<TEntity>? configurator = null)
        => Instantiate(_packedScene, transform, configurator);

    #endregion

    #region Api

    internal Output<ITaskOperation> GetLoadOperation()
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            return Out.InvalidRequest<ITaskOperation>("The asset path can not be empty.");
        }
        if (!FileAccess.FileExists(path))
        {
            return Out.DataNotFound<ITaskOperation>($"Unable to load the asset because path, '{path}', did not exist.");
        }

        return Out.Success<ITaskOperation>(new SequentialOperation(new LoadResourceOperation(path, new()
            {
                AllowMultiThreadedLoad = allowMultiThreadedLoads
            }),
            [
                new(operation => {
                    if (operation.IsSuccessful && operation is LoadResourceOperation loadResourceOperation)
                    {
                        _packedScene = loadResourceOperation.TryGetScene();
                        return _packedScene is null
                            ? new FinishedOperation(OperationState.Failed, "The loaded asset was not of the expected PackedScene type")
                            : FinishedOperation.Success;
                    }

                    return new FinishedOperation(OperationState.Failed, $"There was an issue loading the asset: {operation.Status.Exception?.Message ?? operation.Status.Message}");
                })
            ]));
    }

    #endregion
}
