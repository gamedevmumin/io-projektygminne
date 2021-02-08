namespace VotingSystem.Dtos
{
    public class ProjectReadDto
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        public string Description { get; set; }
        
        public decimal PricePln { get; set; }
        public int EstimatedTimeInDays { get; set; }

        public string District { get; set; }

        public bool Accepted { get; set; }

        public int? TheEditionThisProjectWonId { get; set; }
        public string TheEditionThisProjectWonDescription { get; set; }
    }
}
