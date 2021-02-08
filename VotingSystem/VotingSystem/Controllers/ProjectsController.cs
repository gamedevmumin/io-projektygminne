using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VotingSystem.Dtos;
using VotingSystem.ExternalServices;
using VotingSystem.Models;
using VotingSystem.Models.Data;

namespace VotingSystem.Controllers
{
    [Authorize]
    [Route("projects")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IVotingSystemDb _db;
        private readonly IMapper _mapper;
        private readonly IFileHostingService _fileService;

        public ProjectsController(
            IVotingSystemDb db,
            IMapper mapper,
            IFileHostingService fileService)
        {
            _mapper = mapper;
            _fileService = fileService;
            _db = db;
        }


        [AllowAnonymous]
        [HttpGet]
        public ActionResult<IEnumerable<ProjectReadDto>> GetAllProjects()
        {
            var projects = _db.ProjectsRepo.GetAll();

            var outputDtos = _mapper.Map<IEnumerable<ProjectReadDto>>(projects);

            return Ok(outputDtos);
        }

        [AllowAnonymous]
        [Route("pending")]
        [HttpGet]
        public ActionResult<IEnumerable<ProjectReadDto>> GetPendingProjects()
        {
            return GetProjectsByAcceptationStatus(accepted: false);
        }

        [AllowAnonymous]
        [Route("accepted")]
        [HttpGet]
        public ActionResult<IEnumerable<ProjectReadDto>> GetAcceptedProjects()
        {
            return GetProjectsByAcceptationStatus(accepted: true);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public ActionResult<ProjectReadDto> GetProject(int id)
        {
            var existingProject = _db.ProjectsRepo.FindById(id);
            
            if (existingProject == null)
                return NotFound();

            var outputDto = _mapper.Map<ProjectReadDto>(existingProject);

            return Ok(outputDto);
        }

        [HttpPost]
        public ActionResult<ProjectReadDto> SubmitProject(ProjectWriteDto inputDto)
        {
            try
            {
                try
                {
                    var newProject = _mapper.Map<Project>(inputDto);
                    _db.ProjectsRepo.Add(newProject);
                    _db.SaveChanges();

                    var outputDto = _mapper.Map<ProjectReadDto>(newProject);

                    return CreatedAtAction
                        (
                            actionName: nameof(GetProject),
                            routeValues: new { id = outputDto.Id },
                            value: outputDto
                        );
                }
                catch (AutoMapperMappingException mapex)
                {
                    throw mapex.InnerException;
                }
            }
            catch (ArgumentException argex)
            {
                var error = new ErrorDto { Error = argex.Message };
                return BadRequest(argex);
            }
        }

        [HttpPut("{id}")]
        public ActionResult UpdateUnreferencedProject(int id, ProjectWriteDto inputDto)
        {
            var existingProject = _db.ProjectsRepo.FindById(id);
            if (existingProject == null)
                return NotFound();

            if (IsReferencedInEdition(existingProject))
            {
                var error = new ErrorDto { Error = "Cannot update Project that was already referenced in an Edition." }; 
                return BadRequest(error);
            }
            
            try
            {
                try
                {
                    _mapper.Map(inputDto, existingProject);
                }
                catch (AutoMapperMappingException mapex)
                {
                    throw mapex.InnerException;
                }

                _db.ProjectsRepo.Update(existingProject);
                _db.SaveChanges();

                return NoContent();
            }
            catch (ArgumentException argex)
            {
                var error = new ErrorDto { Error = argex.Message };
                return BadRequest(error);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult RemoveUnreferencedProject(int id)
        {
            var existingProject = _db.ProjectsRepo.FindById(id);
            if (existingProject == null)
                return NotFound();

            if (IsReferencedInEdition(existingProject))
            {
                var error = new ErrorDto { Error = "Cannot delete Project that was already referenced in an Edition." };
                return BadRequest(error);
            }

            _db.ProjectsRepo.Remove(existingProject);
            _db.SaveChanges();
            _fileService.RemoveImagesForProject(existingProject.Id);

            return NoContent();
        }

        [Route("{id}/images")]
        [HttpPut]
        public async Task<ActionResult<UrlListDto>> PostImages(int id, IFormFileCollection files)
        {

            var existingProject = _db.ProjectsRepo.FindById(id);
            if (existingProject == null)
                return NotFound();

            if (IsReferencedInEdition(existingProject))
                return BadRequest(ProjectReferencedError());

            try
            {
                var links = await _fileService.SaveImagesForProjectAsync(id, files);
                var outputDto = new UrlListDto { Links = links };

                return CreatedAtAction
                    (
                        actionName: nameof(GetImageUrls),
                        routeValues: new { id = id },
                        value: outputDto
                    );
            }
            catch(ArgumentException ex)
            {
                var error = new ErrorDto { Error = ex.Message };
                return BadRequest(error);
            }
        }   

        [Route("{id}/images")]
        [HttpDelete]
        public ActionResult RemoveImages(int id, RemoveImagesDto inputDto)
        {
            var existingProject = _db.ProjectsRepo.FindById(id);
            if (existingProject == null)
                return NotFound();

            if (IsReferencedInEdition(existingProject))
                return BadRequest(ProjectReferencedError());

            if (inputDto.Names.Any(name => !_fileService.ProjectImageExists(id, name)))
                return NotFound();

            _fileService.RemoveImagesForProject(id, inputDto.Names);
            return NoContent();
        }

        [Route("{id}/images")]
        [HttpGet]
        [AllowAnonymous]
        public ActionResult<UrlListDto> GetImageUrls(int id)
        {
            var outputDto = new UrlListDto
            {
                Links = _fileService.GetImageUrlsForProject(id)
            };
            return Ok(outputDto);
        }
    
        private bool IsReferencedInEdition(Project p)
        {
            return _db.EditionsRepo.GetAllReferencingProject(p.Id).Any();
        }

        private ActionResult<IEnumerable<ProjectReadDto>> GetProjectsByAcceptationStatus(bool accepted)
        {
            var models = _db.ProjectsRepo.FindByAcceptationStatus(accepted);
            var dtos = _mapper.Map<IEnumerable<ProjectReadDto>>(models);
            return Ok(dtos);
        }
        private ErrorDto ProjectReferencedError()
        {
            return new ErrorDto 
            { 
                Error = "This Project referenced in one or more Editions " +
                        "and media related to it cannot be modified."
            };
        }
    }
}
