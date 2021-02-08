using System.Collections.Generic;
using System.Linq;

namespace VotingSystem.Models
{
    public class ConcludedEditionParticipant : EditionParticipant
    {

        public ConcludedEditionParticipant(EditionParticipant participant)
            : base(participant)
        {
        }

        private ConcludedEditionParticipant() 
            : base() 
        {
        }
    }
}