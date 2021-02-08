using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace VotingSystem.ExternalServices
{
    public class LocalFileHostingService : IFileHostingService
    {
        private readonly IWebHostEnvironment _env;
        public LocalFileHostingService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public bool ProjectImageExists(int projectId, string filename)
            => File.Exists(GetFullFilePath(projectId, filename));

        public List<string> GetImageUrlsForProject(int projectId)
        {
            var directoryPath = GetDirectoryPath(projectId);

            var urls = new List<string>();
            foreach(var path in Directory.EnumerateFiles(directoryPath))
            {
                var relativePath = Path.GetRelativePath(_env.WebRootPath, path);
                urls.Add(relativePath.Replace("\\", "/"));
            }
            return urls;
        }

        public async Task<List<string>> SaveImagesForProjectAsync(int projectId, IFormFileCollection files)
        {
            if (files.Any(f => !IsImage(f)))
                throw new ArgumentException("Cannot upload non-image files.");

            var dirPath = GetDirectoryPath(projectId);
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);

            var filePaths = new List<string>();

            foreach (var formFile in files)
            {
                var filePath = GetFullFilePath(projectId, formFile.FileName);
                var relativePath = Path.GetRelativePath(_env.WebRootPath, filePath);
                filePaths.Add(relativePath.Replace("\\", "/"));

                using var ostream = File.Create(filePath);
                await formFile.CopyToAsync(ostream);
            }

            return filePaths;
        }

        public void RemoveImagesForProject(int projectId, List<string> names)
        {
            foreach (var name in names)
            {
                var filePath = GetFullFilePath(projectId, name);
                File.Delete(filePath);
            }
        }

        public void RemoveImagesForProject(int projectId)
        {
            var dirPath = GetDirectoryPath(projectId);
            if (Directory.Exists(dirPath))
                Directory.Delete(dirPath, recursive: true);
        }


        private static bool IsImage(IFormFile file) 
        {
            if (!file.FileName.ToLowerInvariant().EndsWith(".jpg") &&
                !file.FileName.ToLowerInvariant().EndsWith(".jpeg") &&
                !file.FileName.ToLowerInvariant().EndsWith(".png"))
                return false;

            if (file.ContentType.ToLowerInvariant() != "image/jpg" &&
                file.ContentType.ToLowerInvariant() != "image/jpeg" &&
                file.ContentType.ToLowerInvariant() != "image/png")
                return false;

            return true;
        }
        private string GetDirectoryPath(int projectId)
            => _env.WebRootPath +
                Path.DirectorySeparatorChar + "img" +
                Path.DirectorySeparatorChar + projectId;

        private string GetFullFilePath(int projectId, string filename)
            => GetDirectoryPath(projectId) + Path.DirectorySeparatorChar + filename;

    }

}
