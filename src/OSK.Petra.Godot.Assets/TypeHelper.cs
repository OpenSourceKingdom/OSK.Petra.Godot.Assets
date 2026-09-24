using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace OSK.Petra.Godot.Assets;

public static class TypeHelper
{
    public static IEnumerable<TItem> GetStrongTypeList<TItem>()
        => AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .SelectMany(t => t.GetFields(BindingFlags.Public | BindingFlags.Static))
                .Where(p => p.FieldType == typeof(TItem))
                .Select(p => (TItem)p.GetValue(null));
}
