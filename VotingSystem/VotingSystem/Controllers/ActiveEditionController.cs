using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using VotingSystem.Dtos;
using VotingSystem.Models;
using VotingSystem.ExternalServices;
using System.Threading.Tasks;
using VotingSystem.Dtos.Helpers;
using Microsoft.AspNetCore.Authorization;
using VotingSystem.Models.Data;

namespace VotingSystem.Controllers
{
    [Authorize]
    [Route("editions/current")]
    [ApiController]
    public class ActiveEditionController : ControllerBase
    {
        private readonly IVotingSystemDb _db;
        private readonly IPeselService _peselService;
        private readonly IMailingService _mailingService;
        private readonly IMapper _mapper;
        public ActiveEditionController(
            IVotingSystemDb db,
            IMapper mapper,
            IMailingService mailingService,
            IPeselService peselService)
        {
            _db = db;
            _mapper = mapper;
            _mailingService = mailingService;
            _peselService = peselService;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult<EditionReadDto> GetActiveEdition()
        {
            var activeEdition = _db.EditionsRepo.FindActiveEdition();
            if (activeEdition == null)
                return NotFound();

            var outputDto = _mapper.Map<EditionReadDto>(activeEdition);

            return Ok(outputDto);
        }

        [HttpPut]
        public async Task<ActionResult<EditionReadDto>> StartFromDraft(ActiveEditionStartDto inputDto)
        {
            var activeEdition = _db.EditionsRepo.FindActiveEdition();
            if (activeEdition != null)
                return BadRequest();

            var existingDraft = _db.EditionDraftsRepo.FindById(inputDto.DraftId);
            if (existingDraft == null)
                return BadRequest();

            try
            {
                var newActiveEdition = existingDraft.Implement();
                _db.EditionDraftsRepo.Remove(existingDraft);
                _db.EditionsRepo.Add(newActiveEdition);
                _db.SaveChanges();

                await BroadcastNewsletterAsync(advertisedEdition: newActiveEdition);

                return NoContent();
            }
            catch(ArgumentException ex)
            {
                var error = new ErrorDto { Error = ex.Message };
                return BadRequest(error);
            }
            catch(InvalidOperationException ex)
            {
                var error = new ErrorDto { Error = ex.Message };
                return BadRequest(error);
            }
        }

        [AllowAnonymous]
        [Route("projects/{projectId}/votes")]
        [HttpPut]
        public ActionResult SetVoteStatus(int projectId, ActiveEditionSetVoteStatusDto dto)
        {
            var activeEdition = _db.EditionsRepo.FindActiveEdition();
            if (activeEdition == null)
                return NotFound();

            if (!_peselService.IsCitizenPesel(dto.VoterPesel))
            {
                var error = new ErrorDto { Error = "Provided PESEL is not valid as citizen's PESEL." };
                return BadRequest(error);
            }


            try
            {
                PlaceOrRevokeVote(
                    edition: activeEdition, 
                    projectId: projectId,
                    voterPesel: dto.VoterPesel,
                    placeVote: dto.GiveVote);
            }
            catch (InvalidOperationException ex)
            {
                var error = new ErrorDto { Error = ex.Message };
                return BadRequest(error);
            }

            _db.EditionsRepo.Update(activeEdition);
            _db.SaveChanges();

            return Ok(new { message = "B)" });
        }
 
        private async Task BroadcastNewsletterAsync(ActiveEdition advertisedEdition)
        {
            var subscribers = _db.SubscribersRepo
                    .GetAll()
                    .Select(s => s.Email)
                    .ToList();

            var newsletter = new Newsletter(advertisedEdition: advertisedEdition);
            var newsletterHtml = new NewsletterHtmlPrinter().Print(newsletter);

            await _mailingService.BroadcastEmailAsync(
                recipients: subscribers,
                subject: newsletter.Title,
                body: newsletterHtml);
        }

        private void PlaceOrRevokeVote(ActiveEdition edition, int projectId, string voterPesel, bool placeVote)
        {
            if (placeVote)
            {
                edition.CastVote(projectId, voterPesel);
            }
            else
            {
                edition.RevokeVote(projectId, voterPesel);
            }
        }
    
    }
    
}
