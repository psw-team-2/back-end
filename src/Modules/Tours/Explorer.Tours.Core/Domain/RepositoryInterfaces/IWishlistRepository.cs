using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface IWishlistRepository
    {
         Wishlist GetById(int wishlistId);
         void Update(Wishlist wishlist);
         Wishlist GetWishlistByUserId(int userId);
    }
}
