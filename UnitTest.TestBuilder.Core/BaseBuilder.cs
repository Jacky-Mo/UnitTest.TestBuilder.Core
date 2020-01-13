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
        private readonly IObjectBuilder _objectBuilder;
        private Dictionary<Type, PropertyInfo> _propertyDictionary = new Dictionary<Type, PropertyInfo>();
        private Dictionary<Type, object> _typeInstanceDictionary = new Dictionary<Type, object>(101);
        private readonly PropertyCreator _propertyCreator;
        private readonly ParameterCreator _parameterCreator;

        #region Ctors
        protected BaseBuilder(IObjectBuilder objectBuilder) : this(null, objectBuilder)
        {

        }

        protected BaseBuilder(IContainer container, IObjectBuilder objectBuilder)
        {
            _container = container;
            _objectBuilder = objectBuilder;
            _parameterCreator = new ParameterCreator();
            _propertyCreator = new PropertyCreator(objectBuilder);

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

            return CreateModel();
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
                //get the public properties of the Builder
                _propertyDictionary = GetType().GetProperties()
                    .Where(p => p.CanWrite && p.CanRead && (p.PropertyType.IsClass || p.PropertyType.IsInterface))
                    .ToDictionary(k => k.PropertyType, v => v);
            }

            foreach (var kvp in _propertyDictionary)
            {
                var value = kvp.Value.GetValue(this);

                if (value == null) //property is not set, create object if possible
                {
                    var propertyInstance = CreateProperty(kvp.Key);

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

        private object CreateProperty(Type type)
        {
            //if cant find it from DI container, try to create it using default object builder
            var obj = _container?.Resolve(type);

            if (obj != null)
                return obj;

            return _objectBuilder.CanCreate(type) ? _objectBuilder.Create(type) : _propertyCreator.Create(type);
        }

        private TModel CreateModel()
        {
            var constructorWithMostParams = _propertyCreator.GetConstructorWithMostParams(typeof(TModel));

            var constructorParams = new List<object>();

            foreach (var parameter in constructorWithMostParams.GetParameters())
            {
                if (_typeInstanceDictionary.ContainsKey(parameter.ParameterType))
                {
                    constructorParams.Add(_typeInstanceDictionary[parameter.ParameterType]);
                }
                else
                {
                    var parameterInstance = _objectBuilder.Create(parameter.ParameterType) ?? _parameterCreator.Create(parameter);

                    if (parameterInstance == null)
                    {
                        throw new NullReferenceException($"Object was not created for parameter Type: {parameter.ParameterType.Name} when instantiating {typeof(TModel).Name}");
                    }

                    constructorParams.Add(parameterInstance);
                }
            }

            return (TModel)constructorWithMostParams.Invoke(constructorParams.ToArray());
        }
        #endregion
    }
}
