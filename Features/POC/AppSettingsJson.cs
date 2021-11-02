using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace AcceptanceTests.Features.POC
{
    class AppSettingsJson
    {

        [SetUp]
        public void Initialize()
        {
            
        }

        [Test]
        public void Test()
        {
            //*****************************************************************************
            //Example of using GetSection Method GetSection("Section Name")["Value Name"]
            //to return a Section.Value
            //*****************************************************************************
            IConfiguration config = new ConfigurationBuilder()             //Have to use this syntax
                    .AddJsonFile("appSettings.json").Build();   //Have to use this syntax

            //Return a single value element
            var URL = config.GetSection("URL").Value;

            var username = config.GetSection("credentials")["username"];
            var password = config.GetSection("credentials")["password"];

            //********************************************************************************************
            //Example of using GetSection Method to return the entire Section GetSection("Section Name")
            //Then return the specific ["Value Name"]
            //********************************************************************************************
            var credentials = config.GetSection("credentials");
            var username2 = credentials["username"];
            var password2 = credentials["password"];

            //*****************************************************************************
            //There's another method on the `IConfiguration` service, `.GetValue<T>()`,
            //that allows us to convert our configuration value to another type:
            //*****************************************************************************
            var times = config.GetSection("greetingTimes");

            //Test reading the values
            var morning = times["morning"];
            var afternoon = times["afternoon"];
            var night = times["night"];

            TimeSpan morningTime = times.GetValue<TimeSpan>("morning");
            TimeSpan afternoonTime = times.GetValue<TimeSpan>("afternoon");
            TimeSpan nightTime = times.GetValue<TimeSpan>("night");
        }

        [TearDown]
        public void EndTest()
        {
            
        }


    }
}
