using System.Collections.Generic;

namespace VotingSystem.Models.Data
{
    public interface ISubscribersRepo
    {
        IEnumerable<Subscriber> GetAll();
        Subscriber FindByEmail(string email);
        void Add(Subscriber subscriber);
        void Remove(Subscriber subscriber);
    }
}
