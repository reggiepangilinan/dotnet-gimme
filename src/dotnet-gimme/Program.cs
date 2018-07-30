using System;
using dotnetgimme;
using dotnetgimme.Utils;
using McMaster.Extensions.CommandLineUtils;

namespace dotnet_gimme
{
    class Program
    {
        public static int Main(string[] args)
        {
            try
            {
                var argsDebug = Console.ReadLine();

                if (string.IsNullOrEmpty(argsDebug))
                    return CommandLineApplication.Execute<Gimme>(args);
                return CommandLineApplication.Execute<Gimme>(argsDebug.Split(' '));

            }
            catch (Exception ex)
            {
                ConsoleUtil.ExceptionMessage(ex.Message, ex.StackTrace);
                return Gimme.EXCEPTION;
            }
        }
    }
}
