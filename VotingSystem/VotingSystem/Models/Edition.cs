using System;
using System.Collections.Generic;

namespace VotingSystem.Models
{
    public abstract class Edition
    {
        public int Id { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public string Description { get; private set; }

        public IReadOnlyList<EditionParticipant> Participants => _participants;
        protected List<EditionParticipant> _participants = new List<EditionParticipant>();

        public int DistrictId { get; private set; }
        public District District { get; private set; }

        public Edition(DateTime startDate, DateTime endDate, string description, int districtId)
        {
            StartDate = startDate;
            EndDate = endDate;
            Description = description ?? "";
            DistrictId = districtId;
        }
        protected Edition() {}
    }
}