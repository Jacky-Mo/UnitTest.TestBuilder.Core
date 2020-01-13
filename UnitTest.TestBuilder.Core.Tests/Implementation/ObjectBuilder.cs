using System;
using UnitTest.TestBuilder.Core.Abstracts;

namespace UnitTest.TestBuilder.Core.Tests.Implementation
{
    class ObjectBuilder : IObjectBuilder
    {
        public bool CanCreate(Type type)
        {
            return type.IsInterface && type != typeof(ITestServiceC);
        }

        public object Create(Type type)
        {
            if (!CanCreate(type)) return null;

            if(type == typeof(ITestServiceA))
            {
                return new TestServiceA();
            }

            if (type == typeof(ITestServiceB))
            {
                return new TestServiceB();
            }

            return null;
        }
    }
}
