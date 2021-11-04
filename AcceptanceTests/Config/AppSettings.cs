using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace AcceptanceTests.Config
{
    //**************************************************************************************
    //This is an external class file to encapulate the IConfiguration Manager methods
    //to read the Json file contents
    //**************************************************************************************
    public static class AppSettings
    {

        private static IConfiguration config;

        /// <summary>
        /// Create a connection to the IConfiguration Manager
        /// </summary>
        public static IConfiguration Configuration
        {
            get
            {
                var builder = new ConfigurationBuilder()
                    .AddJsonFile("AppSettings.json");
                config = builder.Build();
                return config;
            }

        }


        /// <summary>
        /// Get single Key.value from the Json Environment Vars settings
        /// </summary>
        /// <param name="key">The key name</param>
        /// <returns></returns>
        public static string GetAppEnvironmentValue(string key)
        {
            //Return a single value element Note the .Value propertery
            var result = AppSettings.Configuration.GetSection(key).Value;

            return result;
 
        }

        /// <summary>
        /// Get the section value corresponding to the key from 
        /// Local/Dev/QA/Prod Environment Config Section
        /// </summary>
        /// <param name="key">The Environment key name</param>
        /// <returns></returns>
        public static string GetAppEnvironmentConfig(string key)
        {

            //First get the current environment from the Environment Vars Section
            var environment = AppSettings.Configuration.GetSection("Environment").Value;

            //Next get the Local/Dev/QA/Prod Environment Config Section value based on the "Environment"
            //var value = environment[key];
            var value = config.GetSection(environment)[key];

            return value;


        }

    }

}
