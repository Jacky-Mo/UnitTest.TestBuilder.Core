using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using UnitTest.TestBuilder.Core.Abstracts;

namespace UnitTest.TestBuilder.Core
{
    internal class PropertyCreator
    {
        private readonly IObjectBuilder _objectBuilder;
        private readonly ParameterCreator _parameterCreator;
        public PropertyCreator(IObjectBuilder objectBuilder)
        {
            _objectBuilder = objectBuilder;
            _parameterCreator = new ParameterCreator();
        }

        public object Create(Type type)
        {
            if (type.IsClass && !type.IsInterface && type != typeof(string))
            {
                var constructorWithMostParams = GetConstructorWithMostParams(type);

                var constructorParams = new List<object>();

                foreach (var parameter in constructorWithMostParams.GetParameters())
                {
                    var parameterInstance = _objectBuilder.Create(parameter.ParameterType) ?? _parameterCreator.Create(parameter);

                    if (parameterInstance == null)
                    {
                        throw new NullReferenceException($"Can not create object for parameter Type: {parameter.ParameterType.Name} when instantiating object {type.FullName}");
                    }

                    constructorParams.Add(parameterInstance);
                }

                return constructorWithMostParams.Invoke(constructorParams.ToArray());
            }

            return null;
        }

        public ConstructorInfo GetConstructorWithMostParams(Type type)
        {
            return type.GetConstructors().OrderByDescending(a => a.GetParameters().Length).First();
        }
    }
}
