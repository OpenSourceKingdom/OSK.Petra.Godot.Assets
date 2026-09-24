using Godot;
using OSK.Petra.Godot.Primitives.Data.Nullables;

namespace OSK.Petra.Godot.Assets.Data;

[GlobalClass]
public partial class AssetManagerConfiguration: Resource
{
    #region Variables

    [Export]
    public GameAssetPackage StandardAssets { get; set; }

    [Export]
    public NullableTimeSpan InstantiatorIdleDisposalTimeout { get; set; }

    [Export]
    public bool AllowMultiThreadResourceLoads { get; set; }

    #endregion
}
