using HotelListingAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace HotelListing.API.Data
{
    public class HotelListingDbContext : DbContext
    {
        public HotelListingDbContext (DbContextOptions options) : base(options)
        {

        }

        public DbSet<Hotel> Hotels {get;set;}
        public DbSet<Country> Countries {get;set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().HasData(
                new Country
                {
                    Id = 1, 
                    Name = "Jamaica",
                    ShortName = "JM"
                },
                new Country
                {
                     Id = 2,
                     Name = "Bahamas",
                     ShortName = "BS"
                },
                new Country
                {
                      Id = 3,
                      Name = "Caiman Island",
                      ShortName = "CI"
                }
           );

            modelBuilder.Entity<Hotel>().HasData(
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
                }

               );
        }
    }
}