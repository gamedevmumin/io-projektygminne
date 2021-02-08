using System.ComponentModel.DataAnnotations;

namespace VotingSystem.Dtos
{
    public class EditionDraftSetProjectStatusDto
    {
        [Required]
        public bool Registered { get; set; }
        
        [Required]
        public int ProjectId { get; set; }
    }
}
