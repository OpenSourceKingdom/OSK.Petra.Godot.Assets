using Godot;
using OSK.Petra.Assets.Models;
using OSK.Petra.Godot.Primitives.Data;

namespace OSK.Petra.Godot.Assets.Data;

/// <summary>
/// A descriptor that is for game entities
/// </summary>
public abstract partial class EntityAssetDescriptor: GameAssetDescriptor
{
    #region Variables

    [Export]
    private GuidIdentifier _id = new();

    #endregion

    #region GameAssetDescriptor 

    /// <summary>
    /// The identifier for the asset
    /// </summary>
    public EntityAssetIdentifier AssetIdentifier => new(AssetPackageId, _id);

    #endregion
}
