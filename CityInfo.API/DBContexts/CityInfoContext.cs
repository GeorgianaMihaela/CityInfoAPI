using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.DBContexts
{
    public class CityInfoContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<PointOfInterest> PointOfInterests { get; set; }

        public CityInfoContext(DbContextOptions<CityInfoContext> options) : base(options) { }

        // used to provide data for seeding the database
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().HasData(
                new City("Slobozia")
                {
                    Id = 1,
                    Description = "The one with Eiffel Tower"
                },
                new City("Pitesti")
                {
                    Id = 2,
                    Description = "The one with Trivale Forest"
                },
                new City("Brasov")
                {
                    Id = 3,
                    Description = "The one with the nice center"
                }
                );

            modelBuilder.Entity<PointOfInterest>().HasData(
                new PointOfInterest("Eiffel Tower")
                {
                    Id = 1, 
                    CityId = 1,
                    Description = "Built by a baron"
                },
                new PointOfInterest("City Center")
                {
                    Id = 2,
                    CityId = 1,
                    Description = "Where you can walk"
                },
                new PointOfInterest("Trivale Forest")
                {
                    Id = 3,
                    CityId = 2,
                    Description = "Walk in the forest"
                },
                new PointOfInterest("Parcul Lunca Arges")
                {
                    Id = 4,
                    CityId = 2,
                    Description = "Walk around and bike"
                },
                new PointOfInterest("Piata Sfatului")
                {
                    Id = 5,
                    CityId = 3,
                    Description = "The main attraction"
                },
                new PointOfInterest("Muntele Tampa")
                {
                    Id = 6,
                    CityId = 3,
                    Description = "You can hike"
                }
                ); 
            base.OnModelCreating(modelBuilder);
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite("connectionstring"); 

        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}
