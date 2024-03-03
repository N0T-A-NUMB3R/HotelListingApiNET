using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace HotelListingAPI.Data.Configurations
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasData(
               new Hotel
               {
                   Id = 1,
                   Name = "Sandels resort",
                   Address = "Sandels resort street 1",
                   CountryId = 1,
                   Rating = 4.5
               },
                new Hotel
                {
                    Id = 2,
                    Name = "Puly resort",
                    Address = "Puly resort street 21",
                    CountryId = 2,
                    Rating = 4.5
                },
                new Hotel
                {
                    Id = 3,
                    Name = "Hitoto Plaza",
                    Address = "Hitoto Plaza resort street 13",
                    CountryId = 3,
                    Rating = 4.5
                });
        }
    }
}