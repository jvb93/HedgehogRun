using System;
using System.Collections.Generic;
using System.Text;
using HedgehogRun.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HedgehogRun.Tests
{
    [TestClass]
    public class SolarCalculatorTester
    {
        [TestMethod]
        public void IsSunsetInAfternoon()
        {
            SolarCalculatorService calculatorService = new SolarCalculatorService();
            var sunset = calculatorService.GetSunsetTimeForLatLong(DateTime.UtcNow, TimeZoneInfo.Utc, 0, 0);
            Assert.IsTrue(sunset.Hour > 12);
        }

        [TestMethod]
        public void IsSunriseInMorning()
        {
            SolarCalculatorService calculatorService = new SolarCalculatorService();
            var sunset = calculatorService.GetSunriseTimeForLatLong(DateTime.UtcNow, TimeZoneInfo.Utc, 0, 0);
            Assert.IsTrue(sunset.Hour < 12);
        }
    }
}
