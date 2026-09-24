using Godot;
using System.Collections.Generic;

namespace OSK.Petra.Godot.Assets.Data;

/// <summary>
/// A collection of entities that are associated together through a common collection
/// </summary>
[Tool]
[GlobalClass]
public partial class EntityAssetCollection: GameAssetCollection<EntityAssetDescriptor>
{
    #region Variables

    private EntityAssetDescriptor[] _assets;

    /// <summary>
    /// The assets within the collection
    /// </summary>
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

    /// <inheritdoc/>
    public override IEnumerable<GameAssetDescriptor> GetDescriptors()
        => _assets;

    #endregion
}
