using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces
{
    public interface IApplicationReviewRepository
    {
        public ApplicationReview GetByUser(long userId);
        public ApplicationReview Update(ApplicationReview review);
        public void Create(ApplicationReview review);
    }
}
