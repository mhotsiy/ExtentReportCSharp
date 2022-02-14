using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using NUnit.Framework;
using System;
using System.IO;

namespace Payments.E2E.Tests.CustomReporter
{
    public class ExtentManager
    {
        private static readonly Lazy<ExtentReports> _lazy = new Lazy<ExtentReports>(() => new ExtentReports());
        public static ExtentReports Instance => _lazy.Value;
        static ExtentManager()
        {
            var testDirectoryPath = TestContext.CurrentContext.TestDirectory;
            var reportPath = Path.Combine(testDirectoryPath.Substring(0, testDirectoryPath.LastIndexOf("bin")),
                "Reports", "Index.html");
            Console.WriteLine($"Report Path : {reportPath}");
            var htmlHeporter = new ExtentHtmlReporter(reportPath);
            htmlHeporter.Config.DocumentTitle = "tests";
            htmlHeporter.Config.Theme = Theme.Standard;
            htmlHeporter.AnalysisStrategy = AnalysisStrategy.Class;

            Instance.AttachReporter(htmlHeporter);
        }
        private ExtentManager() { }
    }
}
