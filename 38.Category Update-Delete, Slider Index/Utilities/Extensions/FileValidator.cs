using FrontToBack.Models;
using FrontToBack.Utilities.Enums;
using System.IO;

namespace FrontToBack.Utilities.Extensions
{
    public static class FileValidator
    {
        public static bool ValidatorType(this IFormFile file, IFormFile photo, string type)
        {
            if(file.ContentType.Contains(type))
            {
                return true;
            }

            return false;

        }

        public static bool ValidatorSize(this IFormFile file,FileSize fileSize , long size)
        {
            switch (fileSize)
            {
                case FileSize.KB:
                    return file.Length <= size * 1024;
                case FileSize.MB:
                    return file.Length <= size * 1024 * 1024;
                case FileSize.GB:
                    return file.Length <= (long)size * 1024 * 1024 * 1024;
            }

            return false;

        }

        public static async Task<string> CreateFileAsync(this IFormFile file, params string[] roots)
        {
            string fileName = string.Concat(Guid.NewGuid().ToString(), file.FileName);

            string path = string.Empty;

            for (int i = 0; i < roots.Length; i++)
            {
                path = Path.Combine(path, roots[i]);
            }

            path = Path.Combine(path, fileName);

            using (FileStream filestream = new(path, FileMode.Create))
            {
                await file.CopyToAsync(filestream);
            }
            return fileName;
        }
    }
}
