using Explorer.Payments.Core.Domain;
using Explorer.Payments.Infrastructure.Database;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class WishlistRepository : IWishlistRepository
    {
        private readonly ToursContext _dbContext;
        public WishlistRepository(ToursContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Wishlist GetById(int wishlistId)
        {
            return _dbContext.Wishlists.FirstOrDefault(w => w.Id == wishlistId);
        }

        public Wishlist GetWishlistByUserId(int userId)
        {
            return _dbContext.Wishlists.FirstOrDefault(sc => sc.UserId == userId);
        }

       

        public void Update(Wishlist wishlist)
        {
            Wishlist oldWishlist = _dbContext.Wishlists.FirstOrDefault(sc => sc.Id == wishlist.Id);
            _dbContext.Entry(oldWishlist).CurrentValues.SetValues(wishlist);
            _dbContext.SaveChanges();
        }
    }
}
