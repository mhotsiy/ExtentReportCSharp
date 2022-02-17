using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using ReportPortal.Shared;
using System;
using System.Collections.Generic;
using System.IO;

namespace Payments.E2E.Tests
{
    public class TestLogger
    {
        private readonly IWebDriver _driver;
        private string _screenshotPath;

        public TestLogger(IWebDriver driver)
        {
            _driver = driver;
        }

        public void Error(string info, bool captureScreenshot = true)
        {
            if (captureScreenshot == false)
            {
                Context.Current.Log.Error(info);
            }
            else
            {
                CaptureScreenShot();
                Context.Current.Log.Error($"{info} {{rp#file#{_screenshotPath}}}");
            }
        }

        public void Info(string info, bool captureScreenshot = true)
        {
            if (captureScreenshot == false)
            {
                Context.Current.Log.Info(info);
            }
            else
            {
                CaptureScreenShot();
                Context.Current.Log.Info($"{info} {{rp#file#{_screenshotPath}}}");
            }
        }

        public void TestStep(string info, bool captureScreenshot = true)
        {
            Info($"Test step: {info}", captureScreenshot);
        }

        public void Screenshot()
        {
            Info("Screenshot: ", captureScreenshot : true);
        }

        public void ToReportPortalBasedOnTestStatus()
        {
            var testStatus = TestContext.CurrentContext.Result.Outcome.Status;

            switch (testStatus)
            {
                case TestStatus.Passed:
                    Info("Test passed");
                    break;
                case TestStatus.Inconclusive:
                case TestStatus.Failed:
                    Error("Test failed");
                    break;
                case TestStatus.Skipped:
                    break;
                case TestStatus.Warning:
                    break;
            }
        }

        private void CaptureScreenShot()
        {
            var screenshot = ((ITakesScreenshot)_driver).GetScreenshot();
            var screenshotName = "screenshot.png";
            var folderWithScreenshotPath = Path.Combine(
                TestContext.CurrentContext.TestDirectory.Substring(0, TestContext.CurrentContext.TestDirectory.LastIndexOf("bin")),"Screenshots");

            _screenshotPath = Path.Combine(folderWithScreenshotPath, screenshotName);
            screenshot.SaveAsFile(_screenshotPath);
        }
    }
}