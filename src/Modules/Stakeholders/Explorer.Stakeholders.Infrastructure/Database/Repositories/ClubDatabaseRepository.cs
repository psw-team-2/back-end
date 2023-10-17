using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class ClubDatabaseRepository : IClubRepository
    {
        private readonly StakeholdersContext _dbContext;

        public ClubDatabaseRepository(StakeholdersContext dbContext)
        {
            _dbContext = dbContext;
        }

        public long GetHighestClubId()
        {
            long id = _dbContext.Clubs
                .OrderByDescending(club => club.Id)
                .Select(club => club.Id)
                .FirstOrDefault();

            return id;
        }

        public Club Create(Club club)
        {
            club.Id = GetHighestClubId()+1;
            _dbContext.Clubs.Add(club);
            _dbContext.SaveChanges();
            return club;
        }

        public List<Club> GetAll()
        {
            return _dbContext.Clubs.ToList();
        }

        public Club GetById(long id) 
        {   
            Club club =  _dbContext.Clubs.ToList().Find(c => c.Id == id);
            if (club == null) throw new KeyNotFoundException("Not found.");
            return club;
        }
    }
}
