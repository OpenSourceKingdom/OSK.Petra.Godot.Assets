using Godot;
using OSK.Operations.Workflows.Events;
using OSK.Operations.Workflows.Models;
using OSK.Petra.Assets.Attributes;
using OSK.Petra.Godot.Operations.Workflows.Tasks.Resources;
using OSK.Petra.Modules;
using OSK.Petra.Modules.Services;
using System.Collections.Generic;
using System.Reflection;
using OSK.Petra.Godot.Assets.Models;
using OSK.Petra.Assets.Models;
using OSK.Petra.Godot.Modules.Services;

namespace OSK.Petra.Godot.Assets;

public abstract class NodeModuleLoader<TModule, TModuleDescriptor, TLoadParameters>(TModuleDescriptor descriptor, TLoadParameters loadParameters)
    : GameModuleWorkflowLoader<TModule, TModuleDescriptor, TLoadParameters>(descriptor, loadParameters)
    where TModule: Node, IModule
    where TModuleDescriptor : IModuleDescriptor
    where TLoadParameters : NodeModuleLoadParameters
{
    #region GameModuleWorkflowLoader Overrides

    protected override void LoadModule()
    {
        if (Parameters.LoadBehavior is ModuleLoadBehavior.Replace)
        {
            foreach (var child in Parameters.SceneRoot.GetChildren())
            {
                if (child.GetType().GetCustomAttribute<PersistentAssetAttribute>() is null)
                {
                    Parameters.SceneRoot.RemoveChild(child);
                    child.QueueFree();
                }
            }
        }

        Parameters.SceneRoot.AddChild(ModuleNode);
    }

    protected override IEnumerable<WorkflowStep> GetWorkflowSteps()
    {
        yield return new(new LoadResourceOperation(Descriptor.AssetPath, LoadResourceOptions.Default))
        {
            Description = "Loading Module"
        };

        foreach (var step in GetModuleWorkflowSteps())
        {
            yield return step;
        }
    }

    protected override void OnOperationFinished(WorkflowStepOperationFinishedEvent finishedEvent)
    {
        if (ModuleNode is null && finishedEvent.Operation is LoadResourceOperation loadResourceOperation && loadResourceOperation.Result is PackedScene packedScene)
        {
            ModuleNode = packedScene.Instantiate<TModule>();
            packedScene.Dispose();
            GameModuleBootstrapper.Initialize(ModuleNode, o => { o.WithParent(Parameters.ParentModule as IServiceModule); });
        }
    }

    #endregion

    #region Helpers

    protected virtual IEnumerable<WorkflowStep> GetModuleWorkflowSteps()
        => [];

    #endregion
}
