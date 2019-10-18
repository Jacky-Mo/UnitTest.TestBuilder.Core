using System;
using UnitTest.TestBuilder.Core.Abstracts;

namespace UnitTest.TestBuilder.Core.Tests.Implementation
{
    class ObjectBuilder : IObjectBuilder
    {
        public bool CanCreate(Type type)
        {
            return type != typeof(ITestService2);
        }

        public object Create(Type type)
        {
            if(type == typeof(ITestService))
            {
                return new TestService();
            }

            return Activator.CreateInstance(type);
        }
    }
}
