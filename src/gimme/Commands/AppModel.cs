using gimme.Services.Files;
using gimme.Utils;
using McMaster.Extensions.CommandLineUtils;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace gimme.Commands
{
    [Command(Name = Constants.Command.APP_MODEL, Description = "Application Model")]
    public class AppModel
    {
        private readonly IFilesService filesService;

        [Required(ErrorMessage = "You must specify the group name.")]
        [Argument(0, Description = "The logical grouping that the command belongs to.")]
        public string GroupName { get; }

        [Required(ErrorMessage = "You must specify the model name.")]
        [Argument(1, Description = "Name of the model")]
        public string ModelName { get; }


        public AppModel(IFilesService filesService)
        {
            this.filesService = filesService;
        }


        public void OnExecute()
        {
            //if (!filesService.DirectoryExists(GimmeConfiguration.ApplicationProjectName))
            //    throw new DirectoryNotFoundException($"Directory `{GimmeConfiguration.ApplicationProjectName}` does not exists.");

            var modelCs = $"{ModelName}.cs";
            var appnamespace = $"{GimmeConfiguration.ApplicationProjectName.Replace("\\", ".")}.{GroupName}.Models";
            var appDirectory = Path.Combine(
                                                Environment.CurrentDirectory,
                                                GimmeConfiguration.ApplicationProjectName,
                                                GroupName,
                                                "Models"
                                               );
            var modelFile = Path.Combine(appDirectory, modelCs);

            if (!filesService.DirectoryExists(appDirectory))
                filesService.CreateDirectory(appDirectory);

            filesService.WriteAllTextToFile(modelFile,
                              ResourceUtil.GetResourceText("App_Model")
                              .Replace("{{namespace}}", appnamespace)
                              .Replace("{{name}}", ModelName)
                             );

            ConsoleUtil.SuccessMessage($"Model succesfully generated!");
        }
    }
}
