using System.Collections.Generic;
using System.Linq;

namespace VotingSystem.Models.Data
{
    public interface IDistrictsRepo
    {
        IEnumerable<District> GetAll();
        District FindById(int id);

    }

    public class DistrictsRepo : IDistrictsRepo
    {
        private readonly VotingSystemDbContext _dbContext;

        public DistrictsRepo(VotingSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public District FindById(int id)
        {
            return _dbContext.Districts.Find(id);
        }

        public IEnumerable<District> GetAll()
        {
            return _dbContext.Districts.ToList();
        }
    }
}