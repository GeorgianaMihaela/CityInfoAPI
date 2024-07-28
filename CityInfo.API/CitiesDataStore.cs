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
                    Description = "Where we live", 
                    PointOfInterests = new List<PointOfInterestDTO>()
                    {
                        new PointOfInterestDTO()
                        {
                            Id = 1, 
                            Name = "Piata Sfatului", 
                            Description = "Most visited place in BV"
                        },
                        new PointOfInterestDTO()
                        {
                            Id = 2,
                            Name = "Strada Sforii",
                            Description = "Second most visited place in BV"
                        }
                    }
                },
                new CityDTO() {
                    Id = 2,
                    Name = "Slobozia",
                    Description = "Where  Vique is from",
                    PointOfInterests = new List<PointOfInterestDTO>()
                    {
                        new PointOfInterestDTO()
                        {
                            Id = 1,
                            Name = "Turnul Eiffel",
                            Description = "Most visited place in Slobozia"
                        },
                        new PointOfInterestDTO()
                        {
                            Id = 2,
                            Name = "KFC",
                            Description = "Second most visited place in Slobozia"
                        }
                    }
                },

                new CityDTO() {
                    Id = 3,
                    Name = "Pitesti",
                    Description = "Where I am born",
                    PointOfInterests = new List<PointOfInterestDTO>()
                    {
                        new PointOfInterestDTO()
                        {
                            Id = 1,
                            Name = "Centru",
                            Description = "Most visited place in Pitesti"
                        },
                        new PointOfInterestDTO()
                        {
                            Id = 2,
                            Name = "Parcul Lunca Argesului",
                            Description = "Second most visited place in Pitesti"
                        }
                    }
                }
            };
        }
    }
}
