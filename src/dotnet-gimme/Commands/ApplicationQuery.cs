using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using dotnet_gimme;
using dotnetgimme.Constants;
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
                throw new DirectoryNotFoundException(ApplicationProject.Message.CannotFindApplicationFolder);

            var workingDirectory = Path.Combine(
                                                Environment.CurrentDirectory,
                                                GimmeConfiguration.ApplicationProjectName,
                                                GroupName,
                                                "Queries",
                                                QueryName
                                               );
            var queryTemplateFile = Path.Combine(GimmeConfiguration.TemplateDirectory, DefaultTemplates.APP_QUERY);
            var queryFilename = "Query.cs";
            var nameSpace = $"{GimmeConfiguration.ApplicationProjectName}.{GroupName}.Queries.{QueryName}";
            var queryOutputPath = Path.Combine(workingDirectory, queryFilename);

            if (!Directory.Exists(workingDirectory))
                Directory.CreateDirectory(workingDirectory);
            
            if (!File.Exists(queryTemplateFile))
                throw new FileNotFoundException(ExceptionHelper.FileNotFoundMessage(queryTemplateFile));

            var queryStringContent = File.ReadAllText(queryTemplateFile);

            File.WriteAllText(queryOutputPath,
                              queryStringContent
                              .Replace("{{namespace}}", nameSpace)
                              .Replace("{{name}}", QueryName)
                             );
            
            ConsoleUtil.SuccessMessage($"Query succesfully generated!");
        }
    }
}
