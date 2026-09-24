using Godot;
using System.Collections.Generic;

namespace OSK.Petra.Godot.Assets.Data;

/// <summary>
/// A collection of modules that can be loaded within the asset system
/// </summary>
[Tool]
[GlobalClass]
public partial class ModuleAssetCollection: GameAssetCollection<ModuleAssetDescriptor>
{
    #region Variables

    private ModuleAssetDescriptor[] _assets;

    /// <summary>
    /// The collection of module assets
    /// </summary>
    [Export]
    public ModuleAssetDescriptor[] Assets
    {
        get => _assets;
        set
        {
            _assets = value;
            UpdateIdentifiers();
        }
    }

    #endregion

    #region GameAssetCollection Overrides

    /// <inheritdoc/>
    public override IEnumerable<GameAssetDescriptor> GetDescriptors()
        => _assets;

    #endregion
}
