using System.ComponentModel.DataAnnotations;

namespace VotingSystem.Dtos
{
    public class ActiveEditionSetVoteStatusDto
    {
        [Required]
        public bool GiveVote { get; set; }
        
        [Required]
        public string VoterPesel { get; set; }

    }
}
