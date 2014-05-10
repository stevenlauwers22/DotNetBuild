using System;

namespace DotNetBuild.Tests
{
    public class TestData
    {
        public static Guid GenerateGuid()
        {
            return Guid.NewGuid();
        }

        public static String GenerateString()
        {
            return Guid.NewGuid().ToString();
        }

        public static Decimal GenerateDecimal()
        {
            var generator = new Random();
            var value = new Decimal(generator.NextDouble() * generator.Next());
            return value;
        }

        public static Boolean GenerateBoolean()
        {
            var generator = new Random();
            var value = generator.Next();
            return value % 2 == 0;
        }
    }
}