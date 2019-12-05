namespace UnitTest.TestBuilder.Core.Abstracts
{
    public interface IBuilder<out T>
    {
        T Build(params object[] args);
    }
}
