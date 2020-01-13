namespace UnitTest.TestBuilder.Core.Tests.Implementation
{
    class TestObjectB
    {
        public ITestServiceB TestServiceB { get; set; }

        public TestObjectB()
        {

        }

        public TestObjectB(ITestServiceB testServiceB)
        {
            TestServiceB = testServiceB;
        }

    }
}
