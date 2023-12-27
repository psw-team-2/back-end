using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface IFavouriteItemRepository
    {
       
        public IEnumerable<FavouriteItem> GetFavouriteItemsByWishlist(int wishlistId);
        public void RemoveAllItemsByWishlistId(int wishlistId);
    }
}
