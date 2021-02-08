using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingSystem.ExternalServices
{
    public interface IFileHostingService
    {
        bool ProjectImageExists(int projectId, string filename);
 
        List<string> GetImageUrlsForProject(int projectId);
        Task<List<string>> SaveImagesForProjectAsync(int projectId, IFormFileCollection formFile);
 
        void RemoveImagesForProject(int projectId);
        void RemoveImagesForProject(int projectId, List<string> names);
    }
}
