namespace UnitTest.TestBuilder.Core.Tests.Implementation
{
    class TestObjectC
    {
        public ITestServiceC TestServiceC { get; set; }

        public TestObjectC()
        {

        }

        public TestObjectC(ITestServiceC testServiceC)
        {
            TestServiceC = testServiceC;
        }

    }
}
