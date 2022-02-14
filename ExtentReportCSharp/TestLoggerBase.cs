using AventStack.ExtentReports;
using OpenQA.Selenium;

namespace Payments.E2E.Tests
{
    public class TestLoggerBase
    {
        public TestLogger Log;
        public TestLoggerBase(ExtentTest extentTest, IWebDriver driver)
        {
            Log = new TestLogger(extentTest, driver);
        }
    }
}