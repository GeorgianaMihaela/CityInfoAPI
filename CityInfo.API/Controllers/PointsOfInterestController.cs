using CityInfo.API.Models;
using Microsoft.AspNetCore.Http;
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

        [HttpGet("{pointOfInterestId}")]
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
    }
}
