using gimme.Services.Files;
using gimme.Utils;
using McMaster.Extensions.CommandLineUtils;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace gimme.Commands
{
    [Command(Name = Constants.Command.APP_QUERY, Description = "Application Query (CQRS)")]
    public class AppQuery
    {
        private readonly IFilesService filesService;

        [Required(ErrorMessage = "You must specify the group name.")]
        [Argument(0, Description = "The logical grouping that the query belongs to.")]
        public string GroupName { get; }

        [Required(ErrorMessage = "You must specify the query name.")]
        [Argument(1, Description = "Name of the query")]
        public string QueryName { get; }


        public AppQuery(IFilesService filesService)
        {
            this.filesService = filesService;
        }

        public void OnExecute()
        {
            if (!filesService.DirectoryExists(GimmeConfiguration.ApplicationProjectName))
                throw new DirectoryNotFoundException($"Directory `{GimmeConfiguration.ApplicationProjectName}` does not exists.");

            var appDirectory = Path.Combine(
                                                Environment.CurrentDirectory,
                                                GimmeConfiguration.ApplicationProjectName,
                                                GroupName,
                                                "Queries",
                                                QueryName
                                               );
            var queryCs = "Query.cs";
            var appnamespace = $"{GimmeConfiguration.ApplicationProjectName}.{GroupName}.Queries.{QueryName}";
            var queryFile = Path.Combine(appDirectory, queryCs);

            if (!filesService.DirectoryExists(appDirectory))
                filesService.CreateDirectory(appDirectory);

            filesService.WriteAllTextToFile(queryFile,
                              ResourceUtil.GetResourceText("App_Query")
                              .Replace("{{namespace}}", appnamespace)
                              .Replace("{{name}}", QueryName)
                             );
            ConsoleUtil.SuccessMessage($"Query succesfully generated!");
        }
    }
}
