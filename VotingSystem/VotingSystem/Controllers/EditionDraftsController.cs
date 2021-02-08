using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VotingSystem.Dtos;
using VotingSystem.Extensions;
using VotingSystem.Models;
using VotingSystem.Models.Data;

namespace VotingSystem.Controllers
{

    [Authorize]
    [Route("editions/drafts")]
    [ApiController]
    public class EditionDraftsController : ControllerBase
    {
        private readonly IVotingSystemDb _db;
        private readonly IMapper _mapper;
        public EditionDraftsController(IVotingSystemDb db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult<IEnumerable<EditionDraftReadDto>> GetAllEditionDrafts()
        {
            var drafts = _db.EditionDraftsRepo.GetAll();
            
            var outputDtos = _mapper.Map<IEnumerable<EditionDraftReadDto>>(drafts);
            
            return Ok(outputDtos);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public ActionResult<EditionDraftReadDto> GetEditionDraft(int id)
        {
            var existingDraft = _db.EditionDraftsRepo.FindById(id);
            if (existingDraft == null)
                return NotFound();

            var outputDto = _mapper.Map<EditionDraftReadDto>(existingDraft);

            return Ok(outputDto);
        }



        [HttpPost]
        public ActionResult<EditionDraftReadDto> CreateEditionDraft(EditionDraftWriteDto inputDto)
        {
            try
            {
                try
                {
                    if (inputDto.EndDate.HasValue)
                        inputDto.EndDate = inputDto.EndDate.Value.AddHours(1);
                    
                    var newDraft = _mapper.Map<EditionDraft>(inputDto);
                    AddInitialProjects(newDraft, inputDto.ProjectIds);

                    _db.EditionDraftsRepo.Add(newDraft);
                    _db.SaveChanges();

                    var outputDto = _mapper.Map<EditionDraftReadDto>(newDraft);
                    
                    return CreatedAtAction(
                            actionName: nameof(GetEditionDraft),
                            routeValues: new { id = outputDto.Id },
                            value: outputDto);
                }
                catch (AutoMapperMappingException mapex)
                {
                    throw mapex.InnerException;
                }
            }
            catch (ArgumentException argex)
            {
                var error = new ErrorDto { Error = argex.Message };
                return BadRequest(error);
            }
        }

        private void AddInitialProjects(EditionDraft draft, IEnumerable<int> projectsIds)
        {
            if (projectsIds == null)
                return;

            var initialProjects = _db.ProjectsRepo.FindByIds(projectsIds);

            if (initialProjects.Any(p => p.DistrictId != draft.DistrictId))
                throw new ArgumentException("Some projects have invalid district ID and coulnd't be added.");

            initialProjects.ForEach(p => draft.RegisterProject(p));
        }


        [HttpPut]
        [Route("{draftId}/projects")]
        public ActionResult SetProjectStatusInDraft(int draftId, EditionDraftSetProjectStatusDto dto)
        {
            var existingDraft = _db.EditionDraftsRepo.FindById(draftId);
            if (existingDraft == null)
            {
                var error = new ErrorDto { Error = "Draft with given ID doesn't exist." };
                return NotFound(error);
            }

            var existingProject = _db.ProjectsRepo.FindById(dto.ProjectId);
            if (existingProject == null)
            {
                var error = new ErrorDto { Error = "Project with given ID doesn't exist." }; 
                return NotFound(error);
            }

            try
            {
                RegisterOrUnregisterProjectInDraft(
                    principalDraft: existingDraft,
                    existingProject: existingProject,
                    register: dto.Registered);
            }
            catch (ArgumentException ex)
            {
                var error = new ErrorDto { Error = ex.Message };
                return BadRequest(error);
            }

            _db.EditionDraftsRepo.Update(existingDraft);
            _db.SaveChanges();

            return NoContent();
        }
        
        private void RegisterOrUnregisterProjectInDraft(EditionDraft principalDraft, Project existingProject, bool register)
        {
            if (register)
            {
                principalDraft.RegisterProject(existingProject);
            }
            else
            {
                principalDraft.RemoveProject(existingProject);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteEditionDraft(int id)
        {
            var deletedDraft = _db.EditionDraftsRepo.FindById(id);
            if (deletedDraft == null)
                return NotFound();

            _db.EditionDraftsRepo.Remove(deletedDraft);
            _db.SaveChanges();

            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateEditionDraft(int id, EditionDraftWriteDto inputDto)
        {
            try
            {
                var existingDraft = _db.EditionDraftsRepo.FindById(id);
                if (existingDraft == null)
                    return NotFound();

                try
                {
                    existingDraft = _mapper.Map(inputDto, existingDraft);
                    _db.EditionDraftsRepo.Update(existingDraft);
                }
                catch(AutoMapperMappingException ex)
                {
                    throw ex.InnerException;
                }

                _db.SaveChanges();

                return NoContent();
            }
            catch (ArgumentException rangex)
            {
                var error = new ErrorDto { Error = rangex.Message };
                return BadRequest(error);
            }
        }
    }
}
