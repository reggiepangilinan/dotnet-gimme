using gimme.Utils;
using McMaster.Extensions.CommandLineUtils;
using System;

namespace gimme
{
    [Command(
        Name = "gimme",
        FullName = "gimme",
        Description = "CLI tool that generates enterprise application templates for you in C#."),
        Subcommand(typeof(Commands.WebApiStarter.Command)),
        Subcommand(typeof(Commands.AppCommand)),
        Subcommand(typeof(Commands.AppQuery)),
        Subcommand(typeof(Commands.AppModel)),
        Subcommand(typeof(Commands.AppException)),
        Subcommand(typeof(Commands.ApiController)),
        Subcommand(typeof(Commands.GimmeSettings)),
    ]
    public class Gimme
    {
        public const int EXCEPTION = 2;
        public const int ERROR = 1;
        public const int OK = 0;


        public int OnExecute(CommandLineApplication app, IConsole console)
        {
            if (console == null)
                throw new ArgumentNullException(nameof(console));

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(
                string.Join(
                    Environment.NewLine,
                    string.Empty,

                @" ==============================================================  ",
                @" |                  _       _              _                  | ",
                @" |               __| | ___ | |_ _ __   ___| |_                | ",
                @" |              / _` |/ _ \| __| '_ \ / _ \ __|               | ",
                @" |             | (_| | (_) | |_| | | |  __/ |_                | ",
                @" |              \__,_|\___/ \__|_| |_|\___|\__|               | ",
                @" |                                                            | ",
                @" |                    _                            _          | ",
                @" |               __ _(_)_ __ ___  _ __ ___   ___  / \         | ",
                @" |              / _` | | '_ ` _ \| '_ ` _ \ / _ \/  /         | ",
                @" |             | (_| | | | | | | | | | | | |  __/\_/          | ",
                @" |              \__, |_|_| |_| |_|_| |_| |_|\___\/            | ",
                @" |              |___/                                         | ",
                @" ============================================================== ",
                    string.Empty));
            Console.ResetColor();
            ConsoleUtil.HiglightedMessage("Let's get started dude! Pick a command and then execute it.",
                                          "If you don't know what you're doing just type --help");

            app.ShowHelp();
            if (!GimmeConfiguration.Exists())
            {
                ConsoleUtil.HiglightedMessage("There is no 'gimmesettings.json' file in the current directory.",
                                              "Run command 'gimme settings' to create one.");
            }
            else
            {
                ConsoleUtil.HiglightedMessage("Here are your current settings",
                                              $" - Name: {GimmeConfiguration.ProjectName}",
                                              $" - WebApi Project: {GimmeConfiguration.WebApiProjectName}",
                                              $" - Application Project: {GimmeConfiguration.ApplicationProjectName}",
                                              $" - Application UnitTest Project: {GimmeConfiguration.ApplicationProjectUnitTestName}"
                                             );

            }
            return OK;
        }

    }
}
