using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace VotingSystem.Models.Data
{
    public class EditionDraftsRepo : IEditionDraftsRepo
    {
        private VotingSystemDbContext _dbContext;

        public EditionDraftsRepo(VotingSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(EditionDraft draft)
        {
            _dbContext.EditionDrafts.Add(draft);
            _dbContext.Entry(draft).Reference(d => d.District).Load();
            _dbContext.Entry(draft).Collection(d => d.Projects).Load();
        }
        public EditionDraft FindById(int id)
        {
            return _dbContext.EditionDrafts
                    .Include(ed => ed.District)
                    .Include(ed => ed.Projects)
                    .ThenInclude(p => p.District)
                    .FirstOrDefault(ed => ed.Id == id);
        }

        public IEnumerable<EditionDraft> GetAll()
        {
            return _dbContext.EditionDrafts
                    .Include(ed => ed.District)
                    .Include(ed => ed.Projects)
                    .ThenInclude(p => p.District)
                    .ToList();
        }
        
        public void Update(EditionDraft draft)
        {
        }

        public void Remove(EditionDraft draft)
        {
            _dbContext.EditionDrafts.Remove(draft);
        }

        public IEnumerable<EditionDraft> GetAllReferencingProject(int projectId)
        {
            return _dbContext.EditionDrafts
                .Include(ed => ed.Projects)
                .Where(ed => ed.Projects.Any(p => p.Id == projectId))
                .ToList();
        }
    }
}
