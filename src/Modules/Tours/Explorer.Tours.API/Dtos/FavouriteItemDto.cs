using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class FavouriteItemDto
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public double Price { get; set; }
        public long WishlistId { get; set; }
    }
}
