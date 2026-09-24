using Godot;
using OSK.Petra.Assets;

namespace OSK.Petra.Godot.Assets.Models;

/// <summary>
/// Describes a transform data structure within the godot game engine for the asset system
/// </summary>
/// <typeparam name="TPosition">The transform's postion type</typeparam>
/// <typeparam name="TRotation">The transform's rotation type</typeparam>
public abstract class NodeTransform<TPosition, TRotation>: Transform<TPosition, TRotation>
    where TPosition: struct
    where TRotation: struct
{
    /// <summary>
    /// Describes an optional parent to the node being instantiated, if not set then the asset will be added to the <see cref="NodeModuleLoadParameters.SceneRoot"/>"/>
    /// </summary>
    public Node Parent { get; set; }
}
