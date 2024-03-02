using HotelListing.API.Data;
using HotelListingAPI.Contracts;
using HotelListingAPI.Data;

namespace HotelListingAPI.Repository
{
    public class HotelRepository : GenericRepository<Hotel>, IHotelRepository
    {
        public HotelRepository(HotelListingDbContext context) : base(context)
        {
        }
    }
}
