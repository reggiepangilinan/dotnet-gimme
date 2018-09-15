using System;
using System.IO;
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
#if (DEBUG)
                var argsDebug = Console.ReadLine();
                if (!string.IsNullOrEmpty(argsDebug))
                    return CommandLineApplication.Execute<Gimme>(argsDebug.Split(' '));

#endif
                return CommandLineApplication.Execute<Gimme>(args);

            }
            catch (Exception ex)
            {
                var baseException = ex.GetBaseException();
                ConsoleUtil.ExceptionMessage(baseException.Message, baseException.StackTrace);
                return Gimme.EXCEPTION;
            }
        }
    }
}
