using gimme.Services.Files;
using gimme.Shell.Services;
using gimme.Utils;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace gimme
{
    class Program
    {
        public static int Main(string[] args)
        {
            try
            {
                ServiceCollection services = ServicesRegistration();
                var app = new CommandLineApplication<Gimme>();
                app.Conventions
                    .UseDefaultConventions()
                    .UseConstructorInjection(services.BuildServiceProvider());

#if (DEBUG)
                var argsDebug = Console.ReadLine();
                if (!string.IsNullOrEmpty(argsDebug))
                {
                    var result1 = app.Execute(argsDebug.Split(' '));
                    return CommandResult(result1);
                }
#endif
                var result2 = app.Execute(args);
                return CommandResult(result2);
            }
            catch (Exception ex)
            {
                var baseException = ex.GetBaseException();
                ConsoleUtil.HiglightedMessage();
                ConsoleUtil.HiglightedMessage($"Ooppps! Something wen't wrong.", "(T__T)");
                ConsoleUtil.HiglightedMessage("Please make sure you're in the correct directory",
                    "FYI -  You can only execute commands on the root directory of the solution where you have your `gimmesettings.json`");
                ConsoleUtil.ExceptionMessage(baseException.Message, baseException.StackTrace);
                return CommandResult(Gimme.EXCEPTION);
            }
        }

        private static int CommandResult(int result)
        {

#if (DEBUG)
            ConsoleUtil.SuccessMessage(@"Press any key to continue...");
            Console.ReadKey();
#endif
            return result;
        }

        private static ServiceCollection ServicesRegistration()
        {
            var services = new ServiceCollection();
            var isWindows = System.Runtime.InteropServices.RuntimeInformation.OSDescription.Contains("Windows");
            if (isWindows)
            {
                services.AddSingleton<IShellService, WindowsShellService>();
            }
            else
            {
                services.AddSingleton<IShellService, UnixShellService>();
            }
            services.AddSingleton<IFilesService, FilesService>();
            return services;
        }
    }
}
