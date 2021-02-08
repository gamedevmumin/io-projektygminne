using System;

namespace VotingSystem.Models
{
    public class Duration
    {
        public TimeSpan Value { get; }
        public Duration(TimeSpan value)
        {
            if (value < TimeSpan.Zero)
                throw new ArgumentOutOfRangeException("Value of duration cannot be negative.");

            Value = value;
        }
    }
}
