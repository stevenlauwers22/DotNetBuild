using System;

namespace DotNetBuild.Tests
{
    public class TestHelpers
    {
        public static Exception CatchException(Action action)
        {
            return CatchException<Exception>(action);
        }

        public static T CatchException<T>(Action action)
            where T : Exception
        {
            try
            {
                action();
            }
            catch (T exception)
            {
                return exception;
            }

            return null;
        }
    }
}