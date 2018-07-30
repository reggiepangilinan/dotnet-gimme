using System;
using System.IO;
using System.Reflection;

namespace dotnet_gimme
{
    public static class GimmeConfiguration
    {
        static GimmeConfiguration() => s_templateDirectory = Path.Combine(new FileInfo(Assembly.GetEntryAssembly().Location).DirectoryName,"DefaultTemplates");
        public static string TemplateDirectory { get => s_templateDirectory; private set => s_templateDirectory = value; }
        public const string WebApiProjectName = "";
        public const string ApplicationProjectName = "";
        static string s_templateDirectory;

        public static bool Exists()
        {
            var configfilePath = Path.Combine(Environment.CurrentDirectory,"gimmesettings.json");
            return File.Exists(configfilePath);
        }
    }
}
