using System;
using System.Collections.Generic;
using System.Linq;
using VotingSystem.Extensions;

namespace VotingSystem.Models
{
    public class ActiveEdition : Edition
    {
        
        public ActiveEdition(EditionDraft draft) 
            : base(DateTime.Now, draft.EndDate.Value, draft.Description, draft.DistrictId)
        {
            //if (DateTime.UtcNow.AddYears(1) > draft.EndDate)
            //throw new ArgumentException("End-date must be at least 1 year away from the start-date.");

            if (draft.EndDate.Value.ToUniversalTime() < DateTime.UtcNow)
                throw new ArgumentException("End-date cannot be set in the past.");

            var participants = new List<ActiveEditionParticipant>();
            foreach (var proj in draft.Projects)
                participants.Add(new ActiveEditionParticipant(proj));
            
            _participants = participants
                .Cast<EditionParticipant>()
                .ToList();
        }
        private ActiveEdition()
            : base()
        {
        }

        public ConcludedEdition Conclude()
        {
            return new ConcludedEdition(activeEdition: this);
        }

        public void CastVote(int projectId, string voterPesel)
        {
            var peselAlreadyUsed = Participants
                .Any(p => p.Votes.Any(v => v.VoterPesel == voterPesel));

            if (peselAlreadyUsed) 
                throw new InvalidOperationException("User with that PESEL has casted a vote already.");

            Participants
                .OfType<ActiveEditionParticipant>()
                .FirstOrDefault(p => p.Project.Id == projectId)
                .PlaceVote(voterPesel);
        }

        internal void RevokeVote(int projectId, string voterPesel)
        {
            var voteDoesNotExist = !Participants
                .OfType<ActiveEditionParticipant>()
                .FirstOrDefault(p => p.Project.Id == projectId)
                .Votes
                .Any(v => v.VoterPesel == voterPesel);

            if (voteDoesNotExist) throw new InvalidOperationException("User with that PESEL hasn't casted a vote yet");

            Participants
                .OfType<ActiveEditionParticipant>()
                .FirstOrDefault(p => p.Project.Id == projectId)
                .RemoveVote(voterPesel);
        }
    }
}