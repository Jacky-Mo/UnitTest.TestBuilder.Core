using System;

namespace UnitTest.TestBuilder.Core.Abstracts
{
    public interface IObjectBuilder
    {
        bool CanCreate(Type type);

        object Create(Type type);
    }
}
