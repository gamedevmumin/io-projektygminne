using System;

namespace VotingSystem.Models.Data
{
    public interface IVotingSystemDb : IDisposable
    {
        IEditionDraftsRepo EditionDraftsRepo { get; }
        IProjectsRepo ProjectsRepo { get; }
        IEditionsRepo EditionsRepo { get; }
        ISubscribersRepo SubscribersRepo { get; }
        IDistrictsRepo DistrictsRepo { get; }
        IAuthRepo AuthRepo { get; }
        void SaveChanges();
    }
}
