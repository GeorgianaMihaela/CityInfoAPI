using AutoMapper;
using CityInfo.API.Entities;
using CityInfo.API.Models;

namespace CityInfo.API.Profiles
{
    public class CityProfile : Profile
    {
        // we need this Profile for auto mapper 
        public CityProfile()
        {
            CreateMap<City, CityWithoutPointsOfInterestDTO>();
            CreateMap<City, CityDTO>();
        }
    }
}
