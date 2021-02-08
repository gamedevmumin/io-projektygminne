using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingSystem.Dtos;
using VotingSystem.Extensions;
using VotingSystem.Models;
using VotingSystem.Models.Data;

namespace VotingSystem.Controllers
{
    [Route("editions/concluded")]
    [ApiController]
    public class ConcludedEditionsController : ControllerBase
    {

        private readonly IVotingSystemDb _db;
        private readonly IMapper _mapper;

        public ConcludedEditionsController(IVotingSystemDb db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpPut]
        public ActionResult ConcludeActiveEdition()
        {
            var activeEdition = _db.EditionsRepo.FindActiveEdition();
            if (activeEdition == null)
                return BadRequest();

            var concludedEdition = activeEdition.Conclude();
            _db.EditionsRepo.Remove(activeEdition);
            _db.EditionsRepo.Add(concludedEdition);
            _db.SaveChanges();

            var bestScore = activeEdition.Participants.Max(v => v.Votes.Count);
            var winningProjects = activeEdition.Participants.Where(p => p.Votes.Count == bestScore);

            var rng = new Random();
            int randomIndex = rng.Next(winningProjects.Count());

            var randomizedWinner = winningProjects.ElementAt(randomIndex).Project;
            randomizedWinner.MarkAsWinnerIn(concludedEdition);

            var draftsReferencingTheWinner = _db.EditionDraftsRepo.GetAllReferencingProject(randomizedWinner.Id);
            draftsReferencingTheWinner.ForEach(d => d.RemoveProject(randomizedWinner));

            _db.SaveChanges();

            return Ok();
        }


        [HttpGet]
        public ActionResult<IEnumerable<EditionReadDto>> GetAllConcludedEditions()
        {
            var concludedEditions = _db.EditionsRepo.GetAllConcludedEditions();

            var outputDtos = _mapper.Map<IEnumerable<EditionReadDto>>(concludedEditions);

            return Ok(outputDtos);
        }
    }
}
