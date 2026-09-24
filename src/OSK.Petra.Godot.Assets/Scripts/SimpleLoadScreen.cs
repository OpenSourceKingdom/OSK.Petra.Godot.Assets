using Godot;
using OSK.Petra.Assets.Attributes;
using OSK.Petra.Assets.Events;
using OSK.Petra.Assets.Ports;
using System.Reflection;
using OSK.Petra.Godot.Assets.Models;
using OSK.Petra.Assets.Models;

namespace OSK.Petra.Godot.Assets.Scripts;

/// <summary>
/// Provides a basic load screen that utilizes a background
/// image to 'hide' the scenes. Description text and progress
/// bar allow for setting visual information to a user
/// </summary>
public partial class SimpleLoadScreen : Node, ILoadScreen
{
    #region Variables

    [Export]
    private Label _titleLabel;

    [Export]
    private TextureRect _backgroundImage;

    [Export]
    private ProgressBar _progressBar;

    [Export]
    private Label _descriptionText;

    [Export]
    private Label _progressText;

    private IModuleLoadContext _loadContext;
    private bool _moduleReady;

    #endregion

    #region Godot Overrides

    /// <inheritdoc/>
    public override void _Process(double delta)
    {
        if (_loadContext is not null || _loadContext.LoadProgress.State is not ProgressState.Complete)
        {
            return;
        }

        if (Input.IsAnythingPressed())
        {
            _loadContext.FinalizeLoad();
            _loadContext = null;
        }
    }

    #endregion

    #region ILoadScreen

    /// <inheritdoc/>
    public void Initialize(ModuleLoadParameters loadParameters, IModuleLoadContext context)
    {
        _backgroundImage.Visible = true;

        _loadContext = loadParameters.FinalizationMode is ModuleFinalizationMode.AnyUserInput ? context : null;
        _progressBar.Value = 0;
        if (loadParameters is NodeModuleLoadParameters nodeParameters)
        {
            foreach (var child in nodeParameters.SceneRoot.GetChildren())
            {
                if (child.GetType().GetCustomAttribute<PersistentAssetAttribute>() is null)
                {
                    nodeParameters.SceneRoot.RemoveChild(child);
                    child.QueueFree();
                }
            }
        }

        UpdateDescriptionText();
        UpdateProgress(new LoadProgress(0, "Initializing"));

        context.LoadEvent += HandleLoadEvent; 
    }

    #endregion

    #region Helpers

    private void UpdateDescriptionText()
    {
        if (_descriptionText is not null)
        {
            _descriptionText.Text = GetDescriptionText();
        }
    }

    private void UpdateProgress(LoadProgress progress)
    {
        _progressBar.Value = progress.Percentage;
        if (_progressText is not null)
        {
            _progressText.Text = GetProgressText(progress);
        }
    }

    /// <summary>
    /// Gets the description text to display on the load screen
    /// e.g. this could be a mission story or background context
    /// </summary>
    /// <returns>The description to display</returns>
    protected virtual string GetDescriptionText()
        => string.Empty;

    /// <summary>
    /// Gets the progress text to display to a user
    /// e.g. Loading Map - 55%
    /// </summary>
    /// <param name="progress">The percentage progress, 0-1</param>
    /// <returns>The string to display for progress text</returns>
    protected virtual string GetProgressText(LoadProgress progress)
        => $"{progress.Percentage}% ({progress.Messaage})";

    private void HandleLoadEvent(ModuleLoadEvent loadEvent)
    {
        switch (loadEvent)
        {
            case ModuleLoadProgressEvent progressEvent:
                UpdateProgress(progressEvent.LoadContext.LoadProgress);
                break;
            case ModuleLoadFailedEvent:
            case ModuleLoadFinalizedEvent:
                loadEvent.LoadContext.LoadEvent -= HandleLoadEvent;
                break;
        }
    }

    #endregion
}
