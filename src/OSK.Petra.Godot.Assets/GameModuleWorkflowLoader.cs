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

/// <summary>
/// Represents a module loader for godot that loads utilizing a workflow
/// </summary>
/// <typeparam name="TModule">The type of node module to load</typeparam>
/// <typeparam name="TModuleDescriptor">The type of descriptor for the module</typeparam>
/// <typeparam name="TModuleParameters">The type of parameters required to load the module</typeparam>
public abstract class GameModuleWorkflowLoader<TModule, TModuleDescriptor, TModuleParameters>
    : ModuleLoader<TModuleParameters>
    where TModule: IModule
    where TModuleDescriptor: IModuleDescriptor
    where TModuleParameters : ModuleLoadParameters
{
    #region Variables

    private WorkflowRunner _runner;

    /// <summary>
    /// The module node, once it is loaded
    /// </summary>
    protected TModule ModuleNode { get; set; }

    #endregion

    #region Constructors

    /// <summary>
    /// Creates a workflow module loader with the provided dsecriptor and load parameters
    /// </summary>
    /// <param name="descriptor">The descriptor for the module being loaded</param>
    /// <param name="parameters">The parameters needed to load the module</param>
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

    /// <summary>
    /// Gets the loaded module
    /// </summary>
    /// <returns>The module, if finished loading</returns>
    public IModule GetLoadedModule()
        => ModuleNode;

    #endregion

    #region WorkflowOperation Overrides

    /// <inheritdoc/>
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

    /// <summary>
    /// Triggered when the workflow finished
    /// </summary>
    protected virtual void OnWorkflowFinished()
    {
    }

    /// <summary>
    /// Triggered when a workflow step compeltes
    /// </summary>
    /// <param name="stepFinishedEvent">The triggered finished step event</param>
    protected virtual void OnStepFinished(WorkflowStepFinishedEvent stepFinishedEvent)
    {
    }

    /// <summary>
    /// Triggererd when an individual operation within a step failed
    /// </summary>
    /// <param name="finishedEvent">The triggered finished event</param>
    protected virtual void OnOperationFinished(WorkflowStepOperationFinishedEvent finishedEvent)
    {
    }

    /// <summary>
    /// Gets the list of extended workflow steps required to fully load and initialize the module, once the module has been loaded
    /// </summary>
    /// <returns></returns>
    protected abstract IEnumerable<WorkflowStep> GetWorkflowSteps();

    #endregion
}
