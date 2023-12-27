using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface IAuthorReviewRepository
    {
        AuthorReview Create(AuthorReview entity);
        AuthorReview Get(long id);
        AuthorReview Update(AuthorReview entity);
        List<AuthorReview> GetAuthorReviews(int authorId);
        AuthorReview DisapproveAuthorReview(long reviewId);
    }

}
