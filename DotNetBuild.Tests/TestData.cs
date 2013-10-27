using System;

namespace DotNetBuild.Tests
{
    public class TestData
    {
        public static string GenerateString()
        {
            return Guid.NewGuid().ToString();
        }

        public static decimal GenerateDecimal()
        {
            var generator = new Random();
            var value = new decimal(generator.NextDouble() * generator.Next());
            return value;
        }
    }
}