using System;
using System.IO;
using System.Reflection;

namespace gimme.Utils
{
    public static class ConsoleUtil
    {
        /// <summary>
        /// Writes a Success Message
        /// </summary>
        /// <param name="messages">Messages.</param>
        public static void SuccessMessage(params string[] messages)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            foreach (var message in messages)
                Console.WriteLine(message);
            Console.ResetColor();
        }

        /// <summary>
        /// Writes an Exception Message
        /// </summary>
        /// 
        /// <param name="message">Message.</param>
        /// <param name="stackTrace">Stack trace.</param>
        public static void ExceptionMessage(string message, string stackTrace)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine("Unexpected error: " + message);
#if (DEBUG)
            Console.Error.WriteLine("StackTrace " + stackTrace);
#endif
            Console.ResetColor();
        }

        /// <summary>
        /// Writes a Highlighted Message
        /// </summary>
        /// <param name="messages">Messages.</param>
        public static void HiglightedMessage(params string[] messages)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            foreach (var message in messages)
                Console.WriteLine(message);
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine();
        }
    }

    public static class ResourceUtil
    {
        public static string GetResourceText(string resourcename)
        {
            var resource = Path.Combine(new FileInfo(Assembly.GetEntryAssembly().Location).DirectoryName, "Resources", $"{resourcename}.txt");
            return File.ReadAllText(resource);
        }
    }
}
