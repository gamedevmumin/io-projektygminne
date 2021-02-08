using System;
using System.Collections.Generic;
using System.Linq;
using VotingSystem.Extensions;

namespace VotingSystem.Models
{
    public class EditionDraft
    {
        public int Id { get; private set; }


        public string Description { get; set; }

        public DateTime? EndDate
        {
            set
            {
                if (value.HasValue)
                {
                    if (value.Value.ToUniversalTime() < DateTime.UtcNow)
                        throw new ArgumentException("End-date cannot be set in the past.");

                    _endDate = value.Value.RoundToSeconds();
                }
            }
            get
            {
                return _endDate;
            }
        }
        private DateTime? _endDate;

        
        public District District { get; private set; }

        private int _districtId;
        public int DistrictId
        {
            get
            {
                return _districtId;
            }
            set
            {
                if (Projects.Any(p => p.DistrictId != value))
                    throw new ArgumentException("Cannot set district to one incompatible with projects' district.");

                _districtId = value;
            }
        }

        
        public IReadOnlySet<Project> Projects => _projects;
        private HashSet<Project> _projects = new HashSet<Project>();

        
        public void RegisterProject(Project existingProject)
        {
            if (existingProject.Accepted) 
                throw new ArgumentException("Already accepted project can't be added to edition draft.");

            if (DistrictId != existingProject.DistrictId)
                throw new ArgumentException("Cannot register a project with different district assigned.");

            _projects.Add(existingProject);
        }
        public void RemoveProject(Project registeredProject)
        {
            _projects.Remove(registeredProject);
        }
        public ActiveEdition Implement()
        {
            if (Projects.Count < 2) 
                throw new InvalidOperationException("Edition draft must have at least 2 projects to be active");

            if (!EndDate.HasValue)
                throw new InvalidOperationException("Cannot start edition from draft with end-date not set.");

            if (EndDate.Value.ToUniversalTime() < DateTime.UtcNow)
                throw new InvalidOperationException("End-date cannot be set in the past.");

            return new ActiveEdition(draft: this);
        }

    }
}
