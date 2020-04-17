using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ShopOnline.Application.Commons
{
    public class FileHandlers : IFileHandlers
    {
        private readonly string userContentFolder;
        private const string userContentFolderName = "user-content";

        public FileHandlers(string webRootPath)
        {
            userContentFolder = Path.Combine(webRootPath, userContentFolderName);

            if (!Directory.Exists(userContentFolder))
            {
                Directory.CreateDirectory(userContentFolder);
            }
        }

        public async Task DeleteFileAsync(string fileName)
        {
            var filePath = Path.Combine(userContentFolder, fileName);

            if (File.Exists(filePath))
            {
                await Task.Run(() => File.Delete(filePath));
            }
        }

        public string GetFileUrl(string fileName) =>
            $"/{userContentFolderName}/{fileName}";

        public async Task<string> SaveFileAsync(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            var filePath = Path.Combine(userContentFolder, fileName);

            using (var mediaBinaryStream = file.OpenReadStream())
            {
                using var output = new FileStream(filePath, FileMode.Create);
                {
                    await mediaBinaryStream.CopyToAsync(output);
                }
            }

            return fileName;
        }
    }
}