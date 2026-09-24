using OSK.Operations.Outputs;
using OSK.Operations.Workflows.Events;
using OSK.Operations.Workflows.Models;
using OSK.Operations.Workflows.Runners;
using OSK.Petra.Assets;
using OSK.Petra.Assets.Models;
using OSK.Petra.Modules;
using System;
using System.Collections.Generic;

namespace OSK.Petra.Godot.Assets;

public abstract class GameModuleWorkflowLoader<TModule, TModuleDescriptor, TModuleParameters>
    : ModuleLoader<TModuleParameters>
    where TModule: IModule
    where TModuleDescriptor: IModuleDescriptor
    where TModuleParameters : ModuleLoadParameters
{
    #region Variables

    private WorkflowRunner _runner;

    protected TModule ModuleNode { get; set; }

    #endregion

    #region Constructors

    public GameModuleWorkflowLoader(TModuleDescriptor descriptor, TModuleParameters parameters)
        : base(descriptor, parameters)
    {
        var workflowSteps = GetWorkflowSteps() ?? [];
        _runner = new WorkflowRunner(workflowSteps);

        _runner.OnWorkflowEvent += workflowEvent =>
        {
            if (workflowEvent is WorkflowStepFinishedEvent stepFinishedEvent)
            {
                OnStepFinished(stepFinishedEvent);
            }
        };
        _runner.OnOperationFinished += OnOperationFinished;

        var a = LoadProgress;
    }

    #endregion

    #region ModuleLoader

    public IModule GetLoadedModule()
        => ModuleNode;

    #endregion

    #region WorkflowOperation Overrides

    protected override LoadProgress UpdateProgress(TimeSpan deltaTime)
    {
        if (_runner is null)
        {
            return LoadProgress.Complete;
        }

        _runner.Iterate(deltaTime);
        var status = _runner.Status;
        if (_runner.IsFinished)
        {
            if (ModuleNode is null)
            {
                return new(Out.Fault(new InvalidOperationException($"The load workflow '{GetType().FullName}' completed without containing a module root, unable to complete loading.")));
            }

            OnWorkflowFinished();
            _runner = null;
            return LoadProgress.Complete;
        }

        return new(_runner.Status.Progress, _runner.Status.Message);
    }

    #endregion

    #region Helpers

    protected virtual void OnWorkflowFinished()
    {
    }

    protected virtual void OnStepFinished(WorkflowStepFinishedEvent stepFinishedEvent)
    {
    }

    protected virtual void OnOperationFinished(WorkflowStepOperationFinishedEvent finishedEvent)
    {
    }

    protected abstract IEnumerable<WorkflowStep> GetWorkflowSteps();

    #endregion
}
