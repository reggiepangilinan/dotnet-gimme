using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace dotnet_gimme
{
    public static class GimmeConfiguration
    {
        static IConfigurationRoot Configuration { get; set; }

        static GimmeConfiguration()
        {
            s_templateDirectory = Path.Combine(new FileInfo(Assembly.GetEntryAssembly().Location).DirectoryName, "DefaultTemplates");
            if (Exists())
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("gimmesettings.json");
                Configuration = builder.Build();
            }
        }

        public static string TemplateDirectory
        {
            get => s_templateDirectory;
            private set => s_templateDirectory = value;
        }

        public static string ProjectName => Configuration["Name"];
        public static string WebApiProjectName => Configuration["Projects:WebApi"];
        public static string ApplicationProjectName => Configuration["Projects:Application"];
        public static string ApplicationProjectUnitTestName => Configuration["Projects:ApplicationUnitTest"];

        static string s_templateDirectory;

        public static bool Exists()
        {
            var configfilePath = Path.Combine(Environment.CurrentDirectory, "gimmesettings.json");
            return File.Exists(configfilePath);
        }
    }
}
