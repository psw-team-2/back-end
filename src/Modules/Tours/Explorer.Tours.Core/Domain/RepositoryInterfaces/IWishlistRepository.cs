using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface IWishlistRepository
    {
        public Wishlist GetById(int wishlistId);
        public void Update(Wishlist wishlist);
        public Wishlist GetWishlistByUserId(int userId);
    }
}
