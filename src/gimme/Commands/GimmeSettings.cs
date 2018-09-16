using gimme.Services.Files;
using gimme.Utils;
using McMaster.Extensions.CommandLineUtils;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace gimme.Commands
{
    [Command(Name = Constants.Command.SETTINGS, Description = "Creates a 'gimmesettings.json' in the current directory")]
    public class GimmeSettings
    {
        private readonly IFilesService filesService;

        [Required(ErrorMessage = "You must specify the project.")]
        [Argument(1, Description = "Name of the project (no white spaces)")]
        public string ProjectName { get; }

        public GimmeSettings(IFilesService filesService)
        {
            this.filesService = filesService;
        }

        public void OnExecute()
        {
            var appDirectory = Path.Combine(Environment.CurrentDirectory);

            var filename = Path.Combine(appDirectory, $"gimmesettings.json");
            filesService.WriteAllTextToFile(filename,
                              ResourceUtil.GetResourceText("GimmeSettings").Replace("{{name}}", ProjectName)
                 );
            ConsoleUtil.SuccessMessage($"Settings succesfully generated!");
        }
    }
}
