using System.Collections.Generic;

namespace VotingSystem.Models.Data
{

    public interface IEditionsRepo
    {
        IEnumerable<ConcludedEdition> GetAllConcludedEditions();
        IEnumerable<Edition> GetAllReferencingProject(int projectId);
        ActiveEdition FindActiveEdition();
        void Add(ActiveEdition edition);
        void Update(ActiveEdition activeEdition);
        void Remove(ActiveEdition activeEdition);
        void Add(ConcludedEdition concludedEdition);
    }
}
