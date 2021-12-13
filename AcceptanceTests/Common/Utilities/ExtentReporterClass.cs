using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcceptanceTests.Common.Library;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium;


namespace AcceptanceTests.Common.Utilities
{
    public class ExtentReporterClass
    {

        /// <summary>
        /// Define global class vars
        /// </summary>
        //Define a reporter var
        //private static ExtentHtmlReporter htmlReporter;
        // create ExtentReport var and attach reporter(s)
        private ExtentReports _extentReport = null;
        public string reportDir = null;
        public string reportFile = null;
        public string runDir = null;

        //Define a get method to return ExtentReports _report;
        /// <summary>
        /// Since this is a static class, you must do (1) of the following
        /// 1. Put the code logic in the get method
        /// 2. Or create a seperate non-static class and place the code logic in this class
        ///     and call the new operator on the non-static class as was done in the 
        ///     class = TestRunnerInterface.cs file
        ///     example
        ///     private static TestRunnerClass _testrunner = new TestRunnerClass();
        ///     
        ///     Solution
        ///     Put the code logic in the get method
        ///     now for debugging use can place a break point on the get method 
        ///     Similar to the YouTube Video
        ///     Extent Reports in C#|Selenium tutorial using C# and Nunit|Selenium Webdriver C# Nunit tutorial-3
        ///         https://www.youtube.com/watch?v=8-Q3ayH0Zo8 
        /// 
        /// </summary>
        public ExtentReports CreateExtentReport()
        {
            //Update global class vars
            //Get the project dir
            runDir = Library.Library.GetProjectDir();
            //Create the Report file path
            reportDir = runDir + @"\AcceptanceTests\Reports";
            reportFile = runDir + @"\AcceptanceTests\Reports\TestResultsReport.html";

            //Create the Reporter class var from the report dir
            var htmlReporter = new ExtentHtmlReporter(reportFile);

            //Path to Extent-Config.xml
            //C:\Test1\Specflow3-Selenium-4-Framework-.Net-Core\AcceptanceTests\Common\Utilities\Extent-Config.xml
            var extentConfigPath = runDir + @"\AcceptanceTests\Common\Utilities\Extent-Config.xml";
            htmlReporter.LoadConfig(extentConfigPath);

            //With Extent Reports version 4 no xml file is installed
            //All configuration are done in code
            //htmlReporter.config().setEncoding("utf-8");
            //htmlReporter.config().setDocumentTitle("Automation Report");
            //htmlReporter.config().setReportName("Automation Test Results");
            //htmlReporter.config().setTheme(Theme.STANDARD);

            htmlReporter.Config.Encoding = "utf-8";
            htmlReporter.Config.DocumentTitle = "Automation Report";
            htmlReporter.Config.ReportName = "Automation Test Results";
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Standard;


            //Create ExtentReport class var and attach reporter(s)
            _extentReport = new ExtentReports();
            _extentReport.AttachReporter(htmlReporter);

            //AddS ystem Info
            _extentReport.AddSystemInfo("OS", "Windows");
            _extentReport.AddSystemInfo("Host Name", "LocalHost");
            _extentReport.AddSystemInfo("Environment", "QA");
            _extentReport.AddSystemInfo("UserName", "TestUser");

         

            return _extentReport;
        }


        public void CaptureScreenShot(ExtentTest test,IWebDriver browser, string imageName)
        {
            //*********************************************************
            //Pro vs Community Version
            //https://www.extentreports.com/docs/versions/3/java/index.html
            //
            // All the YouTube Videos and Google searchs, not one mention of these
            //methods not available ...great...
            //Based on above web page the following are disabled
            //Add Base64 screenshots to tests
            //Add Base64 screenshots to logs

            //MediaEntityBuilder.CreateScreenCaptureFromPath("screenshot.png").Build());
            //MediaEntityBuilder.CreateScreenCaptureFromPath(reportDir,imgTitle).Build();

            //This code adds an image link to the test report
            //but the imge is blank
            //var base64 = ((ITakesScreenshot)browser).GetScreenshot().AsBase64EncodedString;
            //var screen64 = MediaEntityBuilder.CreateScreenCaptureFromBase64String(base64, title).Build();
            //test.AddScreenCaptureFromBase64String(screen64.ToString(), title);

            //This code DID NOT add an image link to the test report
            //var screenshot = ((ITakesScreenshot)browser).GetScreenshot();
            //var screen = MediaEntityBuilder.CreateScreenCaptureFromPath(screenshot.ToString(), title);
            //test.AddScreenCaptureFromPath(screen.ToString(), title);

            //So we going to have to do the following
            //1. use Selenium to capture the screenshot
            //2. Save the scrrenshot image to a target location
            //3. then call test.AddScreenCaptureFromPath(path to screenshot, title)

            //*************************************************************************
            // Usage
            // the method test.AddScreenCaptureFromPath(imageFileName, title)
            // insert the image target dir path into the web page
            // So we need seperate image.jpeg filenames for differtn screen shots
            //*************************************************************************
            //Capture screen shot
            Screenshot image = ((ITakesScreenshot)browser).GetScreenshot();

            //Save the screenshot to project Temp dir
            //The image filenames must be unique
            //The previous image filename will be over written
            string imageDir = runDir + @"\AcceptanceTests\Temp\";           //Save to Temp dir
            string imageFileName = imageDir + imageName + ".jpeg";          //File extention = .jpeg
            image.SaveAsFile(imageFileName, ScreenshotImageFormat.Jpeg);    //Set teh file format

            //Write to Test Report with the saved image filename = "image"
            //Using the passed in title name
            //Save to Report
            test.AddScreenCaptureFromPath(imageFileName, imageName);

        }


    }// endpublic class ExtentReporterClass


}//End AcceptanceTests.Common.Utilities
