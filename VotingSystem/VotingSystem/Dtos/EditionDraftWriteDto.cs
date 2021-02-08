using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VotingSystem.Models;

namespace VotingSystem.Dtos
{
    public class EditionDraftWriteDto
    {
        public string Description { get; set; }
        
        public DateTime? EndDate { get; set; }
        
        public int DistrictId { get; set; }
        public List<int> ProjectIds { get; set; }
    }
}
