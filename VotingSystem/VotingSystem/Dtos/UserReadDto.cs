using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingSystem.Dtos
{
    public class UserReadDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string RoleName { get; set; }
    }
}