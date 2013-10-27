namespace DotNetBuild.Tests
{
    public abstract class TestSpecification<T>
    {
        protected T Sut;

        protected TestSpecification()
        {
            Arrange();
            Sut = CreateSubjectUnderTest();
            Act();
        }

        protected virtual void Arrange()
        {
        }

        protected abstract T CreateSubjectUnderTest();

        protected abstract void Act();
    }
}