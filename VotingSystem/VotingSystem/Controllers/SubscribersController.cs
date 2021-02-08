using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingSystem.Dtos;
using VotingSystem.Models;
using VotingSystem.Models.Data;

namespace VotingSystem.Controllers
{
    [Route("subscribers")]
    [ApiController]
    public class SubscribersController : ControllerBase
    {
        private readonly IVotingSystemDb _db;

        private readonly IMapper _mapper;

        public SubscribersController(IVotingSystemDb db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpPut]
        public ActionResult<SubscriberReadDto> AddSubscriber(SubscriberRegisterDto inputDto)
        {
            var existingSubscriber = _db.SubscribersRepo.FindByEmail(inputDto.Email);
            if (existingSubscriber != null)
                return NoContent();

            var newSubscriber = _mapper.Map<Subscriber>(inputDto);
            try
            {
                _db.SubscribersRepo.Add(newSubscriber);
                _db.SaveChanges();

                var outputDto = _mapper.Map<SubscriberReadDto>(newSubscriber);
                return Ok(outputDto);
            }
            catch(ArgumentException ex)
            {
                var error = new ErrorDto { Error = ex.Message };
                return BadRequest(error);
            }
        }

        [HttpDelete]
        public ActionResult RemoveSubscriber(SubscriberRemoveDto inputDto)
        {
            var existingSubscriber = _db.SubscribersRepo.FindByEmail(inputDto.Email);
            if (existingSubscriber == null)
                return NotFound();

            _db.SubscribersRepo.Remove(existingSubscriber);
            _db.SaveChanges();

            return NoContent();
        }
    }
}
