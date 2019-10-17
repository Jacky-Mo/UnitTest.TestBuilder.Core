using System;
using System.Collections.Generic;
using UnitTest.TestBuilder.Core.Abstracts;

namespace UnitTest.TestBuilder.Core
{
    public class SimpleContainer : IContainer
    {
        private readonly Dictionary<Type, object> _objectDictionary;
        public SimpleContainer()
        {
            _objectDictionary = new Dictionary<Type, object>(101);
        }

        public void Add<T>(T obj)
        {
            var type = typeof(T);

            if(_objectDictionary.ContainsKey(type))
            {
                _objectDictionary.Remove(type);
            }

            _objectDictionary.Add(type, obj);
        }

        public T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }

        public object Resolve(Type type)
        {
            return _objectDictionary.TryGetValue(type, out var obj)
                ? obj
                : null;
        }
    }
}
