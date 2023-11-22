using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class ClubMessageDatabaseRepository : IClubMessageRepository
    {
        private readonly StakeholdersContext _context;

        public ClubMessageDatabaseRepository(StakeholdersContext context)
        {
            _context = context;
        }

        
    }
}
