using CityInfo.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [Route("api/cities/{cityid}/[controller]")]
    [ApiController]
    public class PointsOfInterestController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<PointOfInterestDTO>> GetPointsOfInterest(int cityid)
        {
            // return 404 when the city with the paased in city id does not exist 
            // find city 
            CityDTO? cityToReturn = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityid);
            if (cityToReturn == null)
            {
                return NotFound(); ;
            }
            return Ok(cityToReturn.PointOfInterests);
        }

        [HttpGet("{pointOfInterestId}", Name = "GetPointOfInterest")]
        public ActionResult<IEnumerable<PointOfInterestDTO>> GetPointOfInterest(int cityid, int pointOfInterestId)
        {
            // return 404 when the city with the paased in city id does not exist 
            // or when the point of interest requested does not exist
            // find city 
            CityDTO? cityToReturn = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityid);
            if (cityToReturn == null)
            {
                return NotFound(); ;
            }

            // find point of interest
            PointOfInterestDTO? pointOfInterestDTO = cityToReturn.PointOfInterests.FirstOrDefault(p => p.Id == pointOfInterestId);

            if (pointOfInterestDTO == null)
            {
                return NotFound(); ;
            }

            return Ok(pointOfInterestDTO);
        }

        [HttpPost]
        public ActionResult<PointOfInterestDTO> CreatePointOfInterest(int cityId, PointOfInterestForCreationDTO pointOfInterest)
        {
            // find if the city exists and return Not found if it does not 
            CityDTO? city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound(); ;
            }

            // calculate the ID of the new point of interest 
            int maxPointId = CitiesDataStore.Current.Cities.SelectMany(c=> c.PointOfInterests).Max(p => p.Id);

            // map PointofInterestForCreationDTO to PointOfInterestDTO 
            PointOfInterestDTO createdPointOfInterest = new PointOfInterestDTO()
            {
                Id = ++maxPointId,
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description
            }; 

            // add the new point of interest to the list 
            city.PointOfInterests.Add(createdPointOfInterest);

            // return a response with Location header 
            return CreatedAtRoute("GetPointOfInterest", new
            {
                cityId = cityId,
                pointOfInterestId = createdPointOfInterest.Id
            }, 
            createdPointOfInterest
            ); 
        }
    }
}
