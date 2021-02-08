using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingSystem.Models.Data
{

    public class VotingSystemDb : IVotingSystemDb
    {
        private readonly VotingSystemDbContext _dbContext;

        public IEditionsRepo EditionsRepo { get; }
        public IEditionDraftsRepo EditionDraftsRepo { get; }

        public IProjectsRepo ProjectsRepo { get; }

        public ISubscribersRepo SubscribersRepo { get; }

        public IDistrictsRepo DistrictsRepo { get; }
        public IAuthRepo AuthRepo { get; }
        public VotingSystemDb(VotingSystemDbContext dbContext)
        {
            _dbContext = dbContext;
            EditionDraftsRepo = new EditionDraftsRepo(dbContext);
            EditionsRepo = new EditionsRepo(dbContext);
            ProjectsRepo = new ProjectsRepo(dbContext);
            SubscribersRepo = new SubscribersRepo(dbContext);
            DistrictsRepo = new DistrictsRepo(dbContext);
            AuthRepo = new AuthRepo(dbContext);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
