using System;
using System.Collections.Generic;
using VotingSystem.Models;

namespace VotingSystem.Dtos
{
    public class EditionDraftReadDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime? EndDate { get; set; }
        public List<ProjectReadDto> Projects { get; set; }
        public string District { get; set; }
    }
}
