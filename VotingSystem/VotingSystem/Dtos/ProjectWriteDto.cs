using System.ComponentModel.DataAnnotations;

namespace VotingSystem.Dtos
{
    public class ProjectWriteDto
    {
        [Required]
        [Range(0, double.MaxValue)]
        public decimal PricePln { get; set; }

        [Required]
        [MinLength(8)]
        public string Name { get; set; }

        [Required]
        [MinLength(15)]
        public string Description { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int EstimatedTimeInDays { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int DistrictId { get; set; }
    }
}
