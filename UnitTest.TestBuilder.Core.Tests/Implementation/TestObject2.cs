namespace UnitTest.TestBuilder.Core.Tests.Implementation
{
    class TestObject2
    {
        public ITestService2 TestService2 { get; set; }

        public TestObject2()
        {

        }

        public TestObject2(ITestService2 testService2)
        {
            TestService2 = testService2;
        }

    }
}
