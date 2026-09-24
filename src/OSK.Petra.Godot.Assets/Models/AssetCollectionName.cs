namespace OSK.Petra.Godot.Assets.Models;

/// <summary>
/// Represents a strongly typed collection name
/// </summary>
/// <param name="Name">The name of the colllection</param>
public readonly record struct AssetCollectionName(string Name)
{
    #region Operators

    /// <summary>
    /// Converts a collection name into the equivalent string
    /// </summary>
    /// <param name="name">The collection name to convert</param>
    public static implicit operator string(AssetCollectionName name)
        => name.Name;

    /// <summary>
    /// Converts a string into the equivalent collection name
    /// </summary>
    /// <param name="name">The string to convert</param>
    public static implicit operator AssetCollectionName(string name)
        => new(name);

    #endregion
}
