using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HedgehogRun.Tests
{
    [TestClass]
    public class TimeZoneTester
    {
        [TestMethod]
        public void PacificTimeOffsetIsNegative()
        {
            Assert.IsTrue(TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time").BaseUtcOffset.Hours < 0);
        }
    }
}
