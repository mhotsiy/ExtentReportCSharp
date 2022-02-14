using AventStack.ExtentReports;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Payments.E2E.Tests;
using Payments.E2E.Tests.CustomReporter;

namespace ExtentReportCSharp
{
    public class Tests
    {
        public ExtentTest ExtentTest;
        public TestLoggerBase Logger;
        protected IWebDriver Driver;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            ExtentTestManager.CreateTestSuit(GetType().FullName);
        }

        [SetUp]
        public void SetUp()
        {
            Driver = new ChromeDriver();
            ExtentTest = ExtentTestManager.CreateTest(TestContext.CurrentContext.Test.Name);
            Logger = new TestLoggerBase(ExtentTest, Driver);
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
            Logger.Log.TestStep("Open google.com");
            Driver.Navigate().GoToUrl("https://www.google.com/");

            Logger.Log.TestStep("Assert url is correct");
            Driver.Url.Should().Be("https://www.1234.com");
        }

        [Test]
        public void JustToShow()
        {
            Logger.Log.Info("Info with screenshot");
            Logger.Log.Info("Info without screenshot", captureScreenshot: false);
            Logger.Log.TestStep("Test step with screenshot");
            Logger.Log.TestStep("Test step without screenshot", captureScreenshot: false);
        }

        [TearDown]
        public void TearDown()
        {
            Logger.Log.ToExtentReportDependsOnTestStatus();
            Driver.Quit();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            ExtentManager.Instance.Flush();
        }
    }
}