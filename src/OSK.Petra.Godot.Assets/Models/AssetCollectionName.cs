namespace OSK.Petra.Godot.Assets.Models;

public readonly record struct AssetCollectionName(string Name)
{
    #region Operators

    public static implicit operator string(AssetCollectionName name)
        => name.Name;

    public static implicit operator AssetCollectionName(string name)
        => new(name);

    #endregion
}
