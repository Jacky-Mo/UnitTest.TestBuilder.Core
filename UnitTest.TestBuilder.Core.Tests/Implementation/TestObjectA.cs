using System;
using UnitTest.TestBuilder.Core.Abstracts;

namespace UnitTest.TestBuilder.Core.Tests.Implementation
{
    class TestObjectA
    {
        public ITestServiceA TestServiceA { get; set; }
        public TestObjectB TestObjectB { get; set; }
        public DateTime Date { get; set; }
        public int Number { get; set; }

        public TestObjectA(ITestServiceA testServiceA, TestObjectB object2, DateTime date, int number)
        {
            TestServiceA = testServiceA;
            TestObjectB = object2;
            Date = date;
            Number = number;
        }
    }
}
