using System;

namespace VotingSystem.Models
{
    public class Price
    {
        public decimal PLN { get; }
        public Price(decimal pln)
        {
            if (pln < 0) 
                throw new ArgumentOutOfRangeException("Parameter 'pln' cannot have negative value.");
            
            PLN = pln;
        }
    }
}
