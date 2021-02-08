using System.Collections.Generic;
using System.Threading.Tasks;

namespace VotingSystem.ExternalServices
{
    public interface IMailingService
    {
        Task BroadcastEmailAsync(List<string> recipients, string subject, string body);
    }
}