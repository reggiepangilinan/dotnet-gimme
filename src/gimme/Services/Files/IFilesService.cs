namespace gimme.Services.Files
{
    public interface IFilesService
    {
        void DeleteFile(string fullpath);

        bool DirectoryExists(string path);

        void WriteAllTextToFile(string fullpath, string contents);

        void CreateDirectory(string path);
    }
}