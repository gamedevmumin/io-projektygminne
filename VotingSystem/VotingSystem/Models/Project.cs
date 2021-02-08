using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingSystem.Models
{

    public class Project
    {
        public int Id { get; private set; }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public Price Price { get; private set; }

        public Duration EstimatedRealizationTime { get; private set; }

        public int DistrictId { get; private set; }
        public District District { get; private set; }

        public bool Accepted => ConcludedEditionId.HasValue;
        public int? ConcludedEditionId { get; private set; }
        public ConcludedEdition ConcludedEdition { get; private set; }

        public IReadOnlySet<EditionDraft> EditionDrafts => _editionDrafts;
        private HashSet<EditionDraft> _editionDrafts = new HashSet<EditionDraft>();

        public Project(string name, string description, Price price, Duration estimatedDuration, int districtId)
        {
            if (name?.Length < 8)
                throw new ArgumentException("Project name has to be at least 8 characters long.");

            if (description?.Length < 15)
                throw new ArgumentException("Project description has to be at least 15 characters long.");

            Name = name;
            Description = description;
            Price = price;
            EstimatedRealizationTime = estimatedDuration;
            DistrictId = districtId;
        }

        private Project()
        {
        }

        public void MarkAsWinnerIn(ConcludedEdition edition)
        {
            ConcludedEditionId = edition.Id;
        }
    }
}
