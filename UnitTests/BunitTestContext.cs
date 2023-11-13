using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Bunit;
using NUnit.Framework;

namespace UnitTests
{
    public abstract class BunitTestContext : TestContextWrapper
    {
        [SetUp]
        public void Setup() => TestContext = new Bunit.TestContext();

        [TearDown]
        public void TearDown() => TestContext?.Dispose();
    }
}
