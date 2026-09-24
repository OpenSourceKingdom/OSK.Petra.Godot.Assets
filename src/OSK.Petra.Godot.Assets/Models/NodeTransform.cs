using Godot;
using OSK.Petra.Assets;

namespace OSK.Petra.Godot.Assets.Models;

public abstract class NodeTransform<TPosition, TRotation>: Transform<TPosition, TRotation>
    where TPosition: struct
    where TRotation: struct
{
    public Node Parent { get; set; }
}
