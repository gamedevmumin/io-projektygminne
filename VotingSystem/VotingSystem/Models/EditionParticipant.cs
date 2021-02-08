using System.Collections.Generic;
using System.Linq;

namespace VotingSystem.Models
{
    public abstract class EditionParticipant
    {
        public int Id { get; private set; }
        
        public int ProjectId { get; private set; }
        public Project Project { get; private set; }

        public IReadOnlyList<Vote> Votes => _votes;
        protected List<Vote> _votes = new List<Vote>();

        protected EditionParticipant(Project project)
        {
            Project = project;
            ProjectId = project.Id;
        }

        protected EditionParticipant(EditionParticipant other)
        {
            Project = other.Project;
            ProjectId = other.ProjectId;
            _votes = other._votes.Select(v => new Vote(v)).ToList();
        }

        protected EditionParticipant()
        {
        }
    }
}