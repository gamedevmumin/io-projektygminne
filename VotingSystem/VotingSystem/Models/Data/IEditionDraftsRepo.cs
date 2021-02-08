using System.Collections.Generic;

namespace VotingSystem.Models.Data
{
    public interface IEditionDraftsRepo
    {
        IEnumerable<EditionDraft> GetAll();
        EditionDraft FindById(int id);
        void Add(EditionDraft draft);
        void Remove(EditionDraft draft);
        void Update(EditionDraft draft);
        IEnumerable<EditionDraft> GetAllReferencingProject(int projectId);
    }
}
