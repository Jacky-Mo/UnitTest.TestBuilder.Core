namespace UnitTest.TestBuilder.Core.Abstracts
{
    public interface ITestBuilder<out T>
    {
        T Build(params object[] args);
    }
}
