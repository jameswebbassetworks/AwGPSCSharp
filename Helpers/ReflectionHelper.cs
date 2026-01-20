using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CSharpInterviewMessageProcessor.Helpers;

public static class ReflectionHelper
{
    public static IEnumerable<T> GetObjectsByBaseClass<T>() where T : class
    {
        var baseClassType = typeof(T);

        var implementingTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(type => baseClassType.IsAssignableFrom(type) && type is { IsInterface: false, IsAbstract: false });

        return implementingTypes
            .Select(Activator.CreateInstance)
            .OfType<T>()
            .ToList();
    }
}
