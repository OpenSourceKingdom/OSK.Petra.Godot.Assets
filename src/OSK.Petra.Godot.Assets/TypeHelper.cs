using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace OSK.Petra.Godot.Assets;

public static class TypeHelper
{
    /// <summary>
    /// Returns a list of TItem types from the assembly
    /// </summary>
    /// <typeparam name="TItem">The type of item to get from the assemblies</typeparam>
    /// <returns>A collection of TItem objects that are in teh assembly</returns>
    public static IEnumerable<TItem> GetStrongTypeList<TItem>()
        => AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .SelectMany(t => t.GetFields(BindingFlags.Public | BindingFlags.Static))
                .Where(p => p.FieldType == typeof(TItem))
                .Select(p => (TItem)p.GetValue(null));
}
