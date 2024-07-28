using CityInfo.API.Models;

namespace CityInfo.API
{
    public class CitiesDataStore
    {
        public List<CityDTO> Cities { get; set; }

        public static CitiesDataStore Current { get; } = new CitiesDataStore();

        public CitiesDataStore()
        {
            // init dummy data 

            Cities = new List<CityDTO>()
            {
                new CityDTO()
                {
                    Id = 1,
                    Name = "Brasov",
                    Description = "Where we live"
                },
                new CityDTO() {
                    Id = 2,
                    Name = "Slobozia",
                    Description = "Where  Vique is from"
                },

                new CityDTO() {
                    Id = 3,
                    Name = "Pitesti",
                    Description = "Where I am born"
                }
            };
        }
    }
}
