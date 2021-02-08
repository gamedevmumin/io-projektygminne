using System;

namespace VotingSystem.Models
{
    public class Subscriber
    {
        public string Email { get; private set; }
        public static Subscriber WithEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be null");

            return new Subscriber(email);
        }

        private Subscriber()
        {
        }
        private Subscriber(string email)
        {
            Email = email;
        }
    }
}