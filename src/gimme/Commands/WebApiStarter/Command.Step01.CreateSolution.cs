using gimme.Utils;
using System;

namespace gimme.Commands.WebApiStarter
{

    public partial class Command
    {
        private void Step01_CreateSolution()
        {
            ConsoleUtil.HiglightedMessage("Creating Solution File");
            Console.WriteLine(shellService.Exec($"mkdir {variable.SolutionName}"));
            Console.WriteLine(shellService.Exec($"dotnet new sln -n {SolutionName} -o {SolutionName}"));
        }
    }
}
