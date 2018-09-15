using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using dotnet_gimme;
using dotnetgimme.Constants;
using dotnetgimme.Utils;
using McMaster.Extensions.CommandLineUtils;

namespace dotnetgimme.Commands
{
    [Command(Name = "settings", Description = "Creates a 'gimmesettings.json' in the current directory")]
    public class GimmeSettings
    {
        [Required(ErrorMessage = "You must specify the project.")]
        [Argument(1, Description = "Name of the project (no white spaces)")]
        public string ProjectName { get; }

        public void OnExecute()
        {
            var workingDirectory = Path.Combine(Environment.CurrentDirectory);
            var templateFile = Path.Combine(GimmeConfiguration.TemplateDirectory, DefaultTemplates.GIMME_SETTINGS);
            if (!File.Exists(templateFile))
                throw new FileNotFoundException(ExceptionHelper.FileNotFoundMessage(templateFile));
            var filename = $"gimmesettings.json";
            var stringContent = File.ReadAllText(templateFile);
            var exceptionOutputFilePath = Path.Combine(workingDirectory, filename);
            File.WriteAllText(exceptionOutputFilePath,
                              stringContent.Replace("{{name}}", ProjectName)
                 );
            ConsoleUtil.SuccessMessage($"Settings succesfully generated!");
        }
    }
}
