using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using OSK.Petra.Modules;
using OSK.Petra.Godot.Assets.Internal.Services;
using OSK.Petra.Assets.Models;

namespace OSK.Petra.Godot.Assets.Data;

[Tool]
[GlobalClass]
public partial class ModuleAssetDescriptor : GameAssetDescriptor, IModuleDescriptor
{
	#region Variables

	private static readonly Type ExpectedLoaderType = typeof(NodeModuleLoader<,,>);

    private string _loaderType;

	[Export(PropertyHint.Enum)]
	private string _moduleName;

	[Export(PropertyHint.File)]
	private string _assetPath;

	[Export(PropertyHint.Enum)]
	internal string ModuleLoaderType
	{
		get => _loaderType;
		set { _loaderType = value; }
	}

	#endregion

	#region Godot Overrides

	public override void _ValidateProperty(Dictionary property)
    {
        GD.Print("Validating" + property["name"]);
        if (property["name"].AsStringName() == nameof(_moduleName))
		{
            var moduleNames = TypeHelper.GetStrongTypeList<ModuleName>().Select(moduleName => moduleName.Name);
            string hint = string.Join(",", moduleNames);
            property["hint_string"] = hint;
        }

		if (property["name"].AsStringName() == nameof(ModuleLoaderType))
		{
			var types = GetModuleLoaderTypeNames();
			string hint = string.Join(",", types);
			property["hint_string"] = hint;
		}
	}

    #endregion

    #region GameAssetDescriptor Overrides

    public override string AssetPath => _assetPath;

	public ModuleAssetIdentifier AssetIdentifier => new(AssetPackageId, _moduleName);

    public Type GetModuleLoaderType()
	{
		return string.IsNullOrWhiteSpace(ModuleLoaderType)
			? typeof(DefaultModuleWorkflowLoader)
			: Type.GetType(ModuleLoaderType);
	}

	#endregion

	#region Helpers

	private IEnumerable<string> GetModuleLoaderTypeNames()
	{
		return AppDomain.CurrentDomain.GetAssemblies()
			.SelectMany(s => s.GetTypes())
            .Where(p => !p.IsInterface && !p.IsAbstract && p.BaseType is { IsGenericType: true } baseType && baseType.GetGenericTypeDefinition() == ExpectedLoaderType)
            .Select(t => t.FullName);
	}

	#endregion
}
