using System.Collections.Generic;
using System.Linq;

namespace VotingSystem.Models.Data
{
    public class SubscribersRepo : ISubscribersRepo
    {
        private readonly VotingSystemDbContext _dbContext;

        public SubscribersRepo(VotingSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Subscriber subscriber)
        {
            _dbContext.Subscribers.Add(subscriber);
        }

        public Subscriber FindByEmail(string email)
        {
            return _dbContext.Subscribers.FirstOrDefault(s => s.Email == email);
        }

        public IEnumerable<Subscriber> GetAll()
        {
            return _dbContext.Subscribers.ToList();
        }

        public void Remove(Subscriber subscriber)
        {
            throw new System.NotImplementedException();
        }
    }
}
