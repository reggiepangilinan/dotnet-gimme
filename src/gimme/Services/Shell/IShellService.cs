namespace gimme.Shell.Services
{
    public interface IShellService
    {
        /// <summary>
        /// Execute command in terminal
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        string Exec(string command);
    }
}
