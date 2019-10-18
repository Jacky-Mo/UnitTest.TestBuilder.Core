namespace UnitTest.TestBuilder.Core.Tests.Implementation
{
    interface ITestService
    {
        void DoNothing();
    }

    class TestService : ITestService
    {
        public void DoNothing()
        {

        }
    }
}
