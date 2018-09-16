using System.Diagnostics;
using System.Text;

namespace gimme.Shell.Services
{
    /// <summary>
    /// Shell Service For Windows
    /// </summary>
    public class WindowsShellService : IShellService
    {
        public string Exec(string command)
        {
            try
            {
                var escapedArgs = command.Replace("\"", "\\\"");
                var process = new Process()
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "cmd.exe",
                        Arguments = $"/C \"{escapedArgs}\"",
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                    }
                };
                process.Start();
                string result = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                return result;
            }
            catch (System.Exception err)
            {
                var errBuilder = new StringBuilder();
                errBuilder.AppendLine();
                errBuilder.AppendLine();
                errBuilder.AppendLine($"Error executing command `{command}`");
                errBuilder.AppendLine(err.GetBaseException().Message);
                errBuilder.AppendLine();
                errBuilder.AppendLine();
                return errBuilder.ToString();

            }
        }
    }
}
