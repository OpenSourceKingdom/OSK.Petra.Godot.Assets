using Godot;
using OSK.Operations.Outputs;
using OSK.Operations.Outputs.Models;
using OSK.Operations.Workflows.Managers;
using OSK.Operations.Workflows.Models;
using OSK.Operations.Workflows.Ports;
using OSK.Petra.Assets.Ports;
using OSK.Petra.DependencyInjection.Attributes;
using System;
using System.Threading;
using System.Threading.Tasks;
using OSK.Petra.Godot.Assets.Data;
using OSK.Petra.Godot.Assets.Internal.Services;
using OSK.Petra.Assets.Models;

namespace OSK.Petra.Godot.Assets.Scripts;

[GlobalClass]
public partial class AssetManager: Node, IAssetManager
{
    #region Variables

    [Export]
    private AssetManagerConfiguration _configuration;

    [Inject]
    private IAssetService _assetService;

    [Inject]
    private ITaskOperationManager _taskManager;

    #endregion

    #region Godot Overrides

    public override void _EnterTree()
    {
        _taskManager.Configure(settings =>
        {
            settings.Defaults.MaxConcurrentOperations = 1;
        });

        _taskManager.AddTask(() => _assetService.InitializeAsync(new()
        {
            InstantiatorIdleDisposalTimeout = _configuration.InstantiatorIdleDisposalTimeout
        }));
    }

    public override void _Process(double delta)
    {
        var deltaTimespan = TimeSpan.FromSeconds(delta);
        _taskManager?.Update(deltaTimespan);
        _assetService?.Update(deltaTimespan);
    }

    #endregion

    #region IAssetManager

    public async Task<Output<IAssetInstantiator<TEntity, TTransform>>> GetInstantiatorAsync<TEntity, TTransform>(IEntityAssetReference<TTransform> assetReference, CancellationToken cancellationToken = default) 
        where TTransform : ITransform
        where TEntity: class
    {
        switch (assetReference)
        {
            case AssetTemplateReference<PackedScene, TTransform> templateReference:
                return Out.Success<IAssetInstantiator<TEntity, TTransform>>(new PackedAssetInstantiator<TEntity, TTransform>(templateReference.Template, templateReference.CleanupOnDispose));
            case AssetPathReference<TTransform> pathReference:
                var instantiator = new PathAssetInstantiator<TEntity, TTransform>(pathReference.Path, _configuration.AllowMultiThreadResourceLoads);
                var getInstantiationOperation = instantiator.GetLoadOperation();
                if (!getInstantiationOperation.IsSuccessful)
                {
                    return getInstantiationOperation.As<IAssetInstantiator<TEntity, TTransform>>();
                }

                var managedOperation = _taskManager.AddOperation(getInstantiationOperation.Data);

                await managedOperation.WaitAsync(cancellationToken);

                return getInstantiationOperation.Data.Status.State == OperationState.Complete
                    ? Out.Success<IAssetInstantiator<TEntity, TTransform>>(instantiator)
                    : getInstantiationOperation.Data.Status.Exception is not null
                        ? Out.Fault<IAssetInstantiator<TEntity, TTransform>>(getInstantiationOperation.Data.Status.Exception) 
                        : Out.Error<IAssetInstantiator<TEntity, TTransform>>(
                            getInstantiationOperation.Data.Status.State == OperationState.Aborted ? OutputStatus.Timeout : OutputStatus.InternalError, 
                        getInstantiationOperation.Data.Status.Message);
            default:
                return Out.InvalidRequest<IAssetInstantiator<TEntity, TTransform>>("No asset instantiator can support the reference.");
        }
    }

    public async Task<Output> InitializeDatabaseAsync(IAssetInitializationContext context, CancellationToken cancellationToken = default)
    {
        var discoveredPackagesOutput = await DiscoverAssetPackagesAsync(context, cancellationToken);
        if (!discoveredPackagesOutput.IsSuccessful)
        {
            return discoveredPackagesOutput;
        }

        var standardDescriptorsd = _configuration.StandardAssets.GetDescriptors();
        context.AddAssets(standardDescriptorsd);
        context.UpdateProgress(1, "All Assets Loaded");

        context.Succeed();
        return Out.Success();
    }

    #endregion

    #region Helpers

    protected virtual Task<Output> DiscoverAssetPackagesAsync(IAssetInitializationContext context, CancellationToken cancellationToken)
        => Task.FromResult(Out.Success());

    #endregion
}
