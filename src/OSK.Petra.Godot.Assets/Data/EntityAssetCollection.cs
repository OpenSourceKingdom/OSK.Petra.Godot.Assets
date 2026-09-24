using Godot;
using System.Collections.Generic;

namespace OSK.Petra.Godot.Assets.Data;

[Tool]
[GlobalClass]
public partial class EntityAssetCollection: GameAssetCollection<EntityAssetDescriptor>
{
    #region Variables

    private EntityAssetDescriptor[] _assets;

    [Export]
    public EntityAssetDescriptor[] Assets
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
