using System;
using dotnetgimme.Commands;
using dotnetgimme.Utils;
using McMaster.Extensions.CommandLineUtils;

namespace dotnetgimme
{
    [Command(
        Name = "dotnet gimme",
        FullName = "dotnet-gimme",
        Description = "CLI tool that generates enterprise application templates for you in C#."),
     Subcommand("app-command", typeof(ApplicationCommand)),
     Subcommand("app-query", typeof(ApplicationQuery)),
     Subcommand("app-model", typeof(ApplicationModel)),
     Subcommand("app-exception", typeof(Commands.ApplicationException)),
     Subcommand("api-mediator-controller", typeof(MediatorController)),
     Subcommand("webapi-starter", typeof(WebApiStarter))
    ]
    public class Gimme
    {
        // Return codes
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
            return ERROR;
        }
    }
}
