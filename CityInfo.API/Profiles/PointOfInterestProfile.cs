using AutoMapper;
using CityInfo.API.Entities;
using CityInfo.API.Models;

namespace CityInfo.API.Profiles
{
    public class PointOfInterestProfile : Profile
    {
        public PointOfInterestProfile()
        {
            CreateMap<PointOfInterest, PointOfInterestDTO>();
            CreateMap<PointOfInterestForCreationDTO, PointOfInterest>(); 
            CreateMap<PointOfInterestForUpdateDTO, PointOfInterest>();
            CreateMap<PointOfInterest, PointOfInterestForUpdateDTO>();
        }
    }
}
