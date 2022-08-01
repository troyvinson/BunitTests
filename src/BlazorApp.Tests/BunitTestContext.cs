using Bunit;
using NUnit.Framework;
using Bunit.TestDoubles;


namespace BlazorApp.Tests
{
    public abstract class BunitTestContext : TestContextWrapper
    {
        [SetUp]
        public void Setup() => TestContext = new Bunit.TestContext();

        [TearDown]
        public void TearDown() => TestContext?.Dispose();
    }
}