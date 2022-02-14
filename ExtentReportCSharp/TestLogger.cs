using AventStack.ExtentReports;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace Payments.E2E.Tests
{
    public class TestLogger
    {
        private readonly ExtentTest _extentTest;
        private readonly IWebDriver _driver;

        public TestLogger(ExtentTest extentTest, IWebDriver driver)
        {
            _extentTest = extentTest;
            _driver = driver;
        }

        public void TestStep(string info, bool captureScreenshot = true)
        {
            if (captureScreenshot == false)
            {
                _extentTest.Pass(info);
            }
            else
            {
                _extentTest.Pass(info, CaptureScreenShot());
            }
        }

        public void Info(string info, bool captureScreenshot = true)
        {
            if (captureScreenshot == false)
            {
                _extentTest.Info(info);
            }
            else
            {
                _extentTest.Info(info, CaptureScreenShot());
            }
        }

        public void ToExtentReportDependsOnTestStatus()
        {
            var testStatus = TestContext.CurrentContext.Result.Outcome.Status;
            var stackTrace = TestContext.CurrentContext.Result.StackTrace;
            var errorMessage = TestContext.CurrentContext.Result.Message;
            var testCategories = TestContext.CurrentContext.Test.Properties["Category"];

            AssignCategoriesToTest(testCategories);

            switch (testStatus)
            {
                case TestStatus.Passed:
                    _extentTest.Pass("Test passed successfully", CaptureScreenShot());
                    break;
                case TestStatus.Inconclusive:
                case TestStatus.Failed:
                    _extentTest.Fail("Test failed", CaptureScreenShot());
                    _extentTest.Error($"Error message: {errorMessage}");
                    _extentTest.Error($"StackTrace: {stackTrace}");
                    break;
                case TestStatus.Skipped:
                    _extentTest.Skip($"Test is skipped. Reason: {errorMessage}");
                    break;
                case TestStatus.Warning:
                    _extentTest.Warning($"The test has status Warning. Reason: {errorMessage}");
                    break;
            }
        }
        private void AssignCategoriesToTest(IEnumerable<object> testCategories)
        {
            foreach (var testCategory in testCategories)
            {
                _extentTest.AssignCategory(testCategory.ToString());
            };
        }

        private MediaEntityModelProvider CaptureScreenShot()
        {
            ITakesScreenshot ts = (ITakesScreenshot)_driver;
            var screenshot = ts.GetScreenshot().AsBase64EncodedString;
            string screenShotName = $"Screenshot{DateTime.UtcNow:dd MMMM yyyy HH: mm:ss}";
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, screenShotName).Build();
        }
    }
}