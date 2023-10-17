using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;

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

        public Result<PagedResult<ClubDto>> GetAll()
        {
            return null;
        }

        public Club GetById(long id) 
        {   
            Club club =  _dbContext.Clubs.ToList().Find(c => c.Id == id);
            if (club == null) throw new KeyNotFoundException("Not found.");
            return club;
        }
    }
}
