using System;
using System.Threading.Tasks;

namespace VotingSystem.Extensions
{

    public static class DateTimeExtensions
    {
        public static DateTime RoundToSeconds(this DateTime self)
        {
            return new DateTime(
                    self.Year, self.Month, self.Day,
                    self.Hour, self.Minute, self.Second);
        }
    }

}
