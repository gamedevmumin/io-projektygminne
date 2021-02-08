using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingSystem.Dtos;
using VotingSystem.ExternalServices;
using VotingSystem.Models;
using VotingSystem.Models.Data;

namespace VotingSystem.Controllers
{
    [Route("districts")]
    [ApiController]
    public class DistrictsController : ControllerBase
    {
        private readonly IVotingSystemDb _db;
        private readonly IMapper _mapper;

        public DistrictsController(
            IVotingSystemDb db,
            IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<DistrictReadDto>> GetAllDistricts()
        {
            var districts = _db.DistrictsRepo.GetAll();

            var outputDtos = _mapper.Map<IEnumerable<DistrictReadDto>>(districts);

            return Ok(outputDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<DistrictReadDto> GetDistrict(int id)
        {
            var chosenDistrict = _db.DistrictsRepo.FindById(id);
            if (chosenDistrict == null)
                return NotFound();

            var outputDto = _mapper.Map<DistrictReadDto>(chosenDistrict);

            return Ok(outputDto);
        }
    }
}
