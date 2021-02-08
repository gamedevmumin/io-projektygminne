using System;
using System.Collections.Generic;

namespace VotingSystem.Extensions
{
    public static class IEnumerableExtensions
    { 
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> self, Action<T> action)
        {
            foreach(var elem in self)
            {
                action(elem);
            }
            return self;
        }
    }

}
