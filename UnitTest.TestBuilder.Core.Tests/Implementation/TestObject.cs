using System;
using UnitTest.TestBuilder.Core.Abstracts;

namespace UnitTest.TestBuilder.Core.Tests.Implementation
{
    class TestObject
    {
        public ITestService TestService { get; set; }
        public TestObject2 TestObject2 { get; set; }
        public DateTime Date { get; set; }
        public int Number { get; set; }

        public TestObject(ITestService testService, TestObject2 object2, DateTime date, int number)
        {
            TestService = testService;
            TestObject2 = object2;
            Date = date;
            Number = number;
        }
    }
}
