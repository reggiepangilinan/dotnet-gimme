using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace gimme.Services.Files
{
    public class FilesService : IFilesService
    {
        public void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }

        public bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        public void DeleteFile(string fullpath)
        {
            File.Delete(fullpath);
        }

        public void WriteAllTextToFile(string fullpath, string contents)
        {
            File.WriteAllText(fullpath, contents);
        }

        public bool FileExists(string fullpath)
        {
            return File.Exists(fullpath);
        }
    }
}
