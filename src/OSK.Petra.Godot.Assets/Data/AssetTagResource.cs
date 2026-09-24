using Godot;
using Godot.Collections;
using System.Linq;
using OSK.Petra.Godot.Assets.Models;
using OSK.Petra.Assets.Models;

namespace OSK.Petra.Godot.Assets.Data;

/// <summary>
/// A resource that can be converted into asset tags
/// </summary>
[Tool]
[GlobalClass]
public partial class AssetTagResource: Resource
{
    #region Variables

    [Export(PropertyHint.Enum)]
    private string _category;

    [Export(PropertyHint.Enum)]
    private string[] _values;

    #endregion

    #region Resource Overrides

    /// <inheritdoc/>
    public override void _ValidateProperty(Dictionary property)
    {
        if (property["name"].AsStringName() == nameof(_category))
        {
            var categories = TypeHelper.GetStrongTypeList<AssetTagCategory>().Select(category => category.Name);
            string hint = string.Join(",", categories);
            property["hint_string"] = hint;
        }

        if (property["name"].AsStringName() == nameof(_values))
        {
            var names = TypeHelper.GetStrongTypeList<AssetCollectionName>().Select(name => name.Name);
            string hint = string.Join(",", names);
            property["hint_string"] = hint;
        }
    }

    #endregion

    #region Api

    /// <summary>
    /// Converts this asset tag resource into an equivalent asset tag
    /// </summary>
    /// <returns>The asset tag derived from this resource</returns>
    public AssetTag GetTag()
        => new(_category, _values.Select(value => new AssetTagValue(value)));

    #endregion
}
