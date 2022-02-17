using OpenQA.Selenium;

namespace Payments.E2E.Tests
{
    public class TestLoggerBase
    {
        public TestLogger Log;
        public TestLoggerBase(IWebDriver driver)
        {
            Log = new TestLogger(driver);
        }
    }
}