using gimme.Services.Files;
using gimme.Shell.Services;
using McMaster.Extensions.CommandLineUtils;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace gimme.Commands.WebApiStarter
{
    [Command(Name = Constants.Command.WEB_API_STARTER, Description = "An ASP.NET Core WebApi Starter Kit.")]
    public partial class Command
    {
        private VariableGenerator variable;
        private readonly IShellService shellService;
        private readonly IFilesService filesService;

        [Required(ErrorMessage = "You must specify the solution name.")]
        [Argument(1, Description = "Name of the solution")]
        public string SolutionName { get; }


        public Command(IShellService shellService, IFilesService filesService)
        {
            this.shellService = shellService;
            this.filesService = filesService;
        }

        public void OnExecute()
        {
            var regexItem = new Regex("^[a-zA-Z0-9]*$");

            if (!regexItem.IsMatch(SolutionName))
                throw new ArgumentException($"Invalid characters in SolutionName");

            variable = new VariableGenerator(SolutionName);
            Step01_CreateSolution();
            Step02_CreateProjects();
            Step03_Scaffold();
            Step04_ConfigureNugetReference();
        }
    }
}
