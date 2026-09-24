using Godot;
using OSK.Petra.Godot.Primitives.Data.Nullables;

namespace OSK.Petra.Godot.Assets.Data;

/// <summary>
/// A configuration file for the an asset manager
/// </summary>
[GlobalClass]
public partial class AssetManagerConfiguration: Resource
{
    #region Variables

    /// <summary>
    /// The standard base game assets
    /// </summary>
    [Export]
    public GameAssetPackage StandardAssets { get; set; }

    /// <summary>
    /// The amount of time allowed for a cached instantiator to be idle (i.e. unused) before it is cleaned up. If null, the instantiator lives forever
    /// </summary>
    [Export]
    public NullableTimeSpan InstantiatorIdleDisposalTimeout { get; set; }

    /// <summary>
    /// Determines if path style resources should be loaded by using multi threading within godot
    /// </summary>
    [Export]
    public bool AllowMultiThreadResourceLoads { get; set; }

    #endregion
}
