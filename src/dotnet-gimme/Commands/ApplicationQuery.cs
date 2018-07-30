using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Reflection;
using dotnet_gimme;
using dotnetgimme.Utils;
using McMaster.Extensions.CommandLineUtils;

namespace dotnetgimme.Commands
{
    [Command(Name = "app-query", Description = "Application Query (CQRS)")]
    public class ApplicationQuery
    {
        [Required(ErrorMessage = "You must specify the group name.")]
        [Argument(0, Description = "The logical grouping that the query belongs to.")]
        public string GroupName { get; }

        [Required(ErrorMessage = "You must specify the query name.")]
        [Argument(1, Description = "Name of the query")]
        public string QueryName { get; }

        public void OnExecute()
        {
            if (!Directory.Exists(GimmeConfiguration.ApplicationProjectName))
                throw new DirectoryNotFoundException($"Could not find Application Project Folder '{GimmeConfiguration.ApplicationProjectName}'");
            string currentDirectory = Environment.CurrentDirectory;
            var workingDirectory = Path.Combine(
                                                currentDirectory,
                                                GimmeConfiguration.ApplicationProjectName,
                                                GroupName,
                                                "Queries",
                                                QueryName
                                               );


            if (!Directory.Exists(workingDirectory))
                Directory.CreateDirectory(workingDirectory);


            var queryTemplateFile = Path.Combine(GimmeConfiguration.TemplateDirectory, "ApplicationQueryTemplate.txt");

            if (!File.Exists(queryTemplateFile))
                throw new FileNotFoundException($"Cannot find file {queryTemplateFile}");


            var queryFilename = "Query.cs";

            var nameSpace = $"{GimmeConfiguration.ApplicationProjectName}.{GroupName}.Queries.{QueryName}";

            var queryStringContent = File.ReadAllText(queryTemplateFile);

            var queryOutputPath = Path.Combine(workingDirectory, queryFilename);

            File.WriteAllText(queryOutputPath,
                              queryStringContent
                              .Replace("{{namespace}}", nameSpace)
                              .Replace("{{name}}", QueryName)
                             );

            ConsoleUtil.SuccessMessage($"Query succesfully generated!");
        }
    }
}
