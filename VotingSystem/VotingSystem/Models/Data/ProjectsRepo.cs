using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace VotingSystem.Models.Data
{
    public class ProjectsRepo : IProjectsRepo
    {
        private readonly VotingSystemDbContext _dbContext;

        public ProjectsRepo(VotingSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Project project)
        {
            _dbContext.Projects.Add(project);
            _dbContext.Entry(project).Reference(p => p.District).Load();
        }

        public IEnumerable<Project> FindByAcceptationStatus(bool accepted)
        {
            return _dbContext.Projects
                .Include(p => p.District)
                .Include(p => p.ConcludedEdition)
                .ToList()
                .Where(p => p.Accepted == accepted);
        }

        public Project FindById(int id)
        {
            return _dbContext.Projects
                .Include(p => p.District)
                .FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Project> FindByIds(IEnumerable<int> ids)
        {
            return _dbContext.Projects
                .Where(p => ids.Contains(p.Id))
                .Include(p => p.District)
                .ToList();
        }

        public IEnumerable<Project> GetAll()
        {
            return _dbContext.Projects
                .Include(p => p.District)
                .ToList();
        }

        public void Remove(Project project)
        {
            _dbContext.Remove(project);
        }

        public void Update(Project project)
        {
        }
    }
}