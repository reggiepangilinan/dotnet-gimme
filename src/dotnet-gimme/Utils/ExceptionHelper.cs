using System;
using System.Runtime.Serialization;

namespace dotnetgimme.Utils
{
    public static class ExceptionHelper
    {
        public static string FileNotFoundMessage(string filename)
        {
            return $"Cannot find file {filename}";
        }
    }
}
