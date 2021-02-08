using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace VotingSystem.Models.Data
{
    public class EditionsRepo : IEditionsRepo
    {
        private readonly VotingSystemDbContext _dbContext;

        public EditionsRepo(VotingSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(ActiveEdition edition)
        {
            _dbContext.ActiveEditions.Add(edition);
            _dbContext.Entry(edition).Reference(ed => ed.District).Load();
        }

        public void Add(ConcludedEdition concludedEdition)
        {
            _dbContext.ConcludedEditions.Add(concludedEdition);
        }

        public ActiveEdition FindActiveEdition()
        {
            return _dbContext.ActiveEditions
                .Include(ae => ae.Participants)
                .ThenInclude(p => p.Project)
                .ThenInclude(p => p.District)
                .Include(ae => ae.Participants)
                .ThenInclude(p => p.Votes)
                .FirstOrDefault();
        }

        public IEnumerable<ConcludedEdition> GetAllConcludedEditions()
        {
            return _dbContext.ConcludedEditions
                .Include(ce => ce.District)
                .Include(ce => ce.Participants)
                .ThenInclude(p => p.Project)
                .ThenInclude(ce => ce.District)
                .Include(ae => ae.Participants)
                .ThenInclude(p => p.Votes)
                .ToList();
        }

        public IEnumerable<Edition> GetAllReferencingProject(int projectId)
        {
            return _dbContext.Editions
                .Include(e => e.Participants)
                .Where(e => e.Participants.Any(p => p.ProjectId == projectId))
                .ToList();
        }

        public void Remove(ActiveEdition activeEdition)
        {
            _dbContext.ActiveEditions.Remove(activeEdition);
        }

        public void Update(ActiveEdition activeEdition)
        {
        }
    }
}
