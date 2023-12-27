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
    public class FavouriteItemRepository : IFavouriteItemRepository
    {
        private readonly ToursContext _dbContext;
        public FavouriteItemRepository(ToursContext dbContext)
        {
            _dbContext = dbContext;
        }

       

        public IEnumerable<FavouriteItem> GetFavouriteItemsByWishlist(int wishlistId)
        {
            return _dbContext.FavouriteItems.Where(o => o.WishlistId == wishlistId).ToList();
        }

        public void RemoveAllItemsByWishlistId(int wishlistId)
        {
            var allItems = GetFavouriteItemsByWishlist(wishlistId);
            foreach (FavouriteItem item in allItems)
            {
                _dbContext.FavouriteItems.Remove(item);
                _dbContext.SaveChanges();
            }

        }
    }
}
