using System;
using HedgehogRun.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HedgehogRun.Tests
{
    [TestClass]
    public class ConvertServiceTester
    {
        [TestMethod]
        public void AreMilesAccurate()
        {
            ConvertService convertService = new ConvertService();
            Assert.AreEqual(convertService.ConvertTicksToMiles(0), 0);
            Assert.AreEqual(convertService.ConvertTicksToMiles(1000), 0.5203598484848485);
        }

        [TestMethod]
        public void IsMilesPerHourAccurate()
        {
            ConvertService convertService = new ConvertService();

            Assert.AreEqual(convertService.ConvertTicksToMilesPerHour(0), 0);
            Assert.AreEqual(convertService.ConvertTicksToMilesPerHour(1000), 31.221491);
        }
    }
}
