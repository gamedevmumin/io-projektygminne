using System.Collections.Generic;

namespace VotingSystem.Models.Data
{
    public interface IProjectsRepo
    {
        Project FindById(int id);
        IEnumerable<Project> FindByIds(IEnumerable<int> ids);
        IEnumerable<Project> FindByAcceptationStatus(bool accepted);
        IEnumerable<Project> GetAll();
        void Add(Project project);
        void Remove(Project project);
        void Update(Project project);

    }
}