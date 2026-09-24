using Godot;
using System.Collections.Generic;

namespace OSK.Petra.Godot.Assets.Data;

[Tool]
[GlobalClass]
public partial class ModuleAssetCollection: GameAssetCollection<ModuleAssetDescriptor>
{
    #region Variables

    private ModuleAssetDescriptor[] _assets;

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

    public override IEnumerable<GameAssetDescriptor> GetDescriptors()
        => _assets;

    #endregion
}
