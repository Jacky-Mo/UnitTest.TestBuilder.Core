using System;

namespace UnitTest.TestBuilder.Core.Abstracts
{
    public interface IContainer
    {
        T Resolve<T>();

        object Resolve(Type type);
    }
}
