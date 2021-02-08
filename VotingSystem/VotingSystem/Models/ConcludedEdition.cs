using System;
using System.Collections.Generic;
using System.Linq;


namespace VotingSystem.Models
{
    public class ConcludedEdition : Edition
    {
        public ConcludedEdition(ActiveEdition activeEdition)
            : base(activeEdition.StartDate, 
                  activeEdition.EndDate, 
                  activeEdition.Description, 
                  activeEdition.DistrictId)
        {
            _participants = activeEdition.Participants
                .Select(p => new ConcludedEditionParticipant(p))
                .Cast<EditionParticipant>()
                .ToList();
        }
        private ConcludedEdition()
            : base()
        {
        }
    }
}