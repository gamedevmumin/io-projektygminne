﻿namespace VotingSystem.Dtos
{
    public class UserLoginSuccessDto
    {
        public string AccessToken { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }

    }
}