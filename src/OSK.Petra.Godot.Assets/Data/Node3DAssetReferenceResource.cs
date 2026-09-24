using Godot;
using OSK.Petra.Godot.Assets.Models;

namespace OSK.Petra.Godot.Assets.Data;

/// <summary>
/// Represents an asset reference that uses a <see cref="Node3D"/>
/// </summary>
[GlobalClass]
public abstract partial class Node3DAssetReferenceResource: NodeAssetReferenceResource<Node3DTransform>
{
}
