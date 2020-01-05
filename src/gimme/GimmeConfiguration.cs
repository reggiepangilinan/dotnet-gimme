using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace gimme.Utils
{
    public static class GimmeConfiguration
    {
        static IConfigurationRoot Configuration { get; set; }

        static GimmeConfiguration()
        {

            if (Exists())
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("gimmesettings.json");
                Configuration = builder.Build();
            }
            else
            {
                ConsoleUtil.HiglightedMessage("gimmesettings.json does not exists!");
            }
        }


        public static string ProjectName => Configuration["Name"];
        public static string WebApiProjectName => Configuration["Projects:WebApi"];
        public static string ApplicationProjectName => Configuration["Projects:Application"];
        public static string ApplicationProjectUnitTestName => Configuration["Projects:ApplicationUnitTest"];
        public static string ServicesUnitTestName => Configuration["Projects:ServicesUnitTest"];

        public static bool Exists()
        {
            var configfilePath = Path.Combine(Environment.CurrentDirectory, "gimmesettings.json");
            return File.Exists(configfilePath);
        }
    }
}
