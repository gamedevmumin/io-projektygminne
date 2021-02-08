using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingSystem.Models
{
    public class District
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        public District(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public static District FromId(int id)
        {
            switch (id)
            {
                case 1:
                    return new District(1, "Repty");
                case 2:
                    return new District(2, "Lasowice");
                case 3:
                    return new District(3, "Bobrowniki Śląskie");
                case 4:
                    return new District(4, "Opatowice");
                case 5:
                    return new District(5, "Rybna");
                case 6:
                    return new District(6, "Pniowiec");
                case 7:
                    return new District(7, "Sowice");
                case 8:
                    return new District(8, "Puferki");
                case 9:
                    return new District(9, "Repecko");
                case 10:
                    return new District(10, "Siwcowe");
                case 11:
                    return new District(11, "Tłuczykąt");
                default:
                    throw new NotSupportedException("Id out of range.");
            }
        }
    }
}
