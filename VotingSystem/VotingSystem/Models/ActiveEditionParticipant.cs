using System;
using System.Collections.Generic;
using System.Linq;

namespace VotingSystem.Models
{
    public class ActiveEditionParticipant : EditionParticipant
    {

        public ActiveEditionParticipant(Project project)
            : base(project)
        {
        }
        private ActiveEditionParticipant() 
        {
        }

        public bool WasVotedBy(string voterPesel)
        {
            return _votes.Any(v => v.VoterPesel == voterPesel);
        }
        public void PlaceVote(string voterPesel)
        {
            _votes.Add(new Vote(voterPesel, ProjectId));
        }
        public void RemoveVote(string voterPesel)
        {
            var voteWithThisPesel = _votes.Find(v => v.VoterPesel == voterPesel);
            _votes.Remove(voteWithThisPesel);
        }
    }
}