using System;
using System.Collections.Generic;

namespace VotingSystem.Dtos
{

    public class EditionReadDto
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string District { get; set; }
        public List<EditionParticipantReadDto> Participants { get; set; }
    }
}
