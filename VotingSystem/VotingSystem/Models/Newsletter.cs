using System.Collections.Generic;
using System.Linq;

namespace VotingSystem.Models
{


    public class Newsletter
    {
        public class EditionInfo
        {
            public string District { get; set; }
            public string Description { get; set; }
        }
        public class ProjectInfo
        {
            public string Title { get; set; }
            public string Body { get; set; }
        }
        public Newsletter(ActiveEdition advertisedEdition)
        {
            Title = "Newsletter - " + advertisedEdition.EndDate.ToString("d");
            
            EditionDescription = new EditionInfo
            {
                District = advertisedEdition.District.Name,
                Description = advertisedEdition.Description
            };
            
            ProjectDescriptions = advertisedEdition.Participants.Select(p =>
            {
                return new ProjectInfo
                {
                    Title = p.Project.Name,
                    Body = p.Project.Description
                };
            }).ToList();
        }

        public string Title { get; private set; }
        public EditionInfo EditionDescription { get; private set; }
        public IEnumerable<ProjectInfo> ProjectDescriptions { get; private set; }
    }
}
