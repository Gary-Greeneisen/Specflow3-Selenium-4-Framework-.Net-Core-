using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcceptanceTests.Config;

namespace AcceptanceTests.Features.POC
{
    class AppSettingsJson2
    {
        [Test]
        //**************************************************************************************
        //This test uses an external class file to encapulate the IConfiguration Manager methods
        //to read the Json file contents
        //**************************************************************************************
        public void Test()
        {
            //Read single values from Environment Vars Section
            //using public static class AppSettings
            var test = AppSettings.GetAppEnvironmentValue("Test");
            var url = AppSettings.GetAppEnvironmentValue("URL");
            var browserType = AppSettings.GetAppEnvironmentValue("BrowserType");
            var environment = AppSettings.GetAppEnvironmentValue("Environment");

            //Read single values from the Local/Dev/QA/Prod Environment Config Section
            //using public static class AppSettings
            var test2 = AppSettings.GetAppEnvironmentConfig("Test");
            var targetURL = AppSettings.GetAppEnvironmentConfig("TargetURL");
            var dbconnectionString = AppSettings.GetAppEnvironmentConfig("dbconnectionString");

        }

    }

}
