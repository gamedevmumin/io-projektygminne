namespace VotingSystem.Models
{
    public class Vote
    {
        public string VoterPesel { get; private set; }
        public int ProjectId { get; private set; }
        public Vote(string voterPesel, int projectId)
        {
            VoterPesel = voterPesel;
            ProjectId = projectId;
        }
        public Vote(Vote other)
        {
            VoterPesel = other.VoterPesel;
            ProjectId = other.ProjectId;
        }
    }
}