using System;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("UnitTest.TestBuilder.Core.Tests")]
namespace UnitTest.TestBuilder.Core
{ 
    internal class ParameterCreator
    {
        public object Create(ParameterInfo parameter)
        {
            var type = parameter.ParameterType;

            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }

            if (type == typeof(string))
            {
                return $"{parameter.Name} - Test";
            }

            return null;
        }
    }
}
