using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VotingSystem.Dtos
{
    public class SubscriberRegisterDto
    {
        [Required]
        public string Email { get; set; }
    }
}
