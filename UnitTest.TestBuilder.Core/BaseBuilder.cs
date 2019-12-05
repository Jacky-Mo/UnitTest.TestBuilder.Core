using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnitTest.TestBuilder.Core.Abstracts;

namespace UnitTest.TestBuilder.Core
{
    public abstract class BaseBuilder<TModel> : IBuilder<TModel> 
    {
        protected IContainer _container;
        private IObjectBuilder _objectBuilder;
        private Dictionary<Type, PropertyInfo> _propertyDictionary = new Dictionary<Type, PropertyInfo>();
        private Dictionary<Type, object> _typeInstanceDictionary = new Dictionary<Type, object>(101);

        #region Ctors
        protected BaseBuilder(IObjectBuilder objectBuilder) : this(null, objectBuilder)
        {

        }

        protected BaseBuilder(IContainer container, IObjectBuilder objectBuilder)
        {
            _container = container;
            _objectBuilder = objectBuilder;

            PopulateProperties();
        }

        #endregion

        #region Methods
        public TModel Build(params object[] args)
        {
            var instance = CreateObject(args);

            if (instance != null)
                return instance;

            //re-populate the properties because user can instaniate properties in constructor
            PopulateProperties();

            var constructorWithMostParams = typeof(TModel).GetConstructors().OrderByDescending(a => a.GetParameters().Length).First();

            var constructorParams = new List<object>();

            foreach (var parameter in constructorWithMostParams.GetParameters())
            {
                if(_typeInstanceDictionary.ContainsKey(parameter.ParameterType))
                {
                    constructorParams.Add(_typeInstanceDictionary[parameter.ParameterType]);
                }
                else
                {
                    var parameterInstance = CreateObject(parameter.ParameterType);

                    if(parameterInstance == null)
                    {
                        throw new NullReferenceException($"no object created for Type: {parameter.ParameterType}");
                    }

                    constructorParams.Add(parameterInstance);
                }
            }

            instance = (TModel)constructorWithMostParams.Invoke(constructorParams.ToArray());

            return instance;
        }

        protected virtual TModel CreateObject(params object[] args)
        {
            return default;
        }
        #endregion

        #region Private Methods
        private void PopulateProperties()
        {
            if (_propertyDictionary.Count == 0)
            {
                //get the public properties of the TestBuilder
                _propertyDictionary = GetType().GetProperties()
                    .Where(p => p.CanWrite && p.CanRead && (p.PropertyType.IsClass || p.PropertyType.IsInterface))
                    .ToDictionary(k => k.PropertyType, v => v);
            }

            foreach (var kvp in _propertyDictionary)
            {
                var value = kvp.Value.GetValue(this);

                if (value == null) //property is not set, create object if possible
                {
                    var propertyInstance = CreateObject(kvp.Key);

                    if (propertyInstance != null)
                    {
                        _propertyDictionary[kvp.Key].SetValue(this, propertyInstance);
                        _typeInstanceDictionary.Add(kvp.Key, propertyInstance);
                    }
                }
                else //property is set, add it to the instance dictioinary so it can be used for constructor parameter
                {
                    if (_typeInstanceDictionary.ContainsKey(kvp.Key))
                    {
                        _typeInstanceDictionary.Remove(kvp.Key);
                    }

                    _typeInstanceDictionary.Add(kvp.Key, value);
                }
            }
        }

        private object CreateObject(Type type)
        {
            var obj = _container?.Resolve(type);

            if (obj == null)
            {
                if(type.IsValueType)
                {
                    obj = Activator.CreateInstance(type);
                }
                else if(_objectBuilder.CanCreate(type))
                {
                    obj = _objectBuilder.Create(type);
                }
            }

            return obj;
        }
        #endregion
    }
}
