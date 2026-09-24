using Godot;
using OSK.Petra.Assets.Models;
using OSK.Petra.Godot.Primitives.Data;

namespace OSK.Petra.Godot.Assets.Data;

public abstract partial class EntityAssetDescriptor: GameAssetDescriptor
{
    #region Variables

    [Export]
    private GuidIdentifier _id = new();

    #endregion

    #region GameAssetDescriptor 

    public EntityAssetIdentifier AssetIdentifier => new(AssetPackageId, _id);

    #endregion
}
