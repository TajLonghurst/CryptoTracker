using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace UITest
{
    [TestFixture(Platform.Android)]
    //[TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void CryproTracker_DisplaysData_ResultShouldBeFound()
        {
            AppResult[] results = app.WaitForElement(c => c.Marked("ETH"));
            AppResult[] results1 = app.WaitForElement(c => c.Marked("BTC"));
            AppResult[] results2 = app.WaitForElement(c => c.Marked("DOGE"));
            AppResult[] results3 = app.WaitForElement(c => c.Marked("LTC"));
            AppResult[] results4 = app.WaitForElement(c => c.Marked("XMR"));

            app.Screenshot("MainPageDisplay");

            Assert.IsTrue(results.Any());
            Assert.IsTrue(results1.Any());
            Assert.IsTrue(results2.Any());
            Assert.IsTrue(results3.Any());
            Assert.IsTrue(results4.Any());
        }
    }
}
