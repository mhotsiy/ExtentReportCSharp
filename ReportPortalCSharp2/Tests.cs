
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Payments.E2E.Tests;
using System;
using System.Threading;

namespace ReportPortal
{
    [Parallelizable]
    public class Tests
    {
        public TestLoggerBase Logger;
        protected IWebDriver Driver;
        protected WebDriverWait Wait;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {

        }

        [SetUp]
        public void SetUp()
        {
            ChromeOptions opt = new ChromeOptions
            {
                PageLoadStrategy = PageLoadStrategy.Eager
            };

            Driver = new ChromeDriver(opt);
            Logger = new TestLoggerBase(Driver);
        }

        [Category("Positive")]
        [Test]
        public void ShouldPass()
        {
            Logger.Log.TestStep("Open google.com");
            Driver.Navigate().GoToUrl("https://www.google.com/");

            Logger.Log.TestStep("Assert url is correct");
            Driver.Url.Should().Be("https://www.google.com/");
        }

        [Category("Negative")]
        [Test]
        public void ShouldFail()
        {
            Logger.Log.TestStep("Open reportportal.com");
            Driver.Navigate().GoToUrl("https://reportportal.io//");

            Logger.Log.TestStep("Assert url is correct");
            Driver.Url.Should().Be("https://www.yahoo.com/");
        }

        [Test]
        public void Examples()
        {
            Driver.Url.Should().Be("https://www.yahoo.com/");
            Logger.Log.Screenshot();
        }

        [Ignore("Some reason")]
        [Test]
        public void TestShouldBeIgnorred()
        {
        }

        [TearDown]
        public void TearDown()
        {
            Logger.Log.ToReportPortalBasedOnTestStatus();
            Driver.Quit();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
        }
    }
}