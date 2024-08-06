using AutoMapper;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using CityInfo.API.Entities;

namespace CityInfo.API.Controllers
{
    [Route("api/cities/{cityid}/[controller]")]
    [ApiController]
    public class PointsOfInterestController : ControllerBase
    {
        // constructor injection is the prefered way to request dependencies 
        private readonly ILogger<PointsOfInterestController> _logger;
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;

        public PointsOfInterestController(ILogger<PointsOfInterestController> logger,
            ICityInfoRepository cityInfoRepository,
            IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            //_dataStore = dataStore;
            _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PointOfInterestDTO>>> GetPointsOfInterest(int cityid)
        {
            // return 404 when the city with the paased in city id does not exist 
            if (!await _cityInfoRepository.CityExistsAsync(cityid))
            {
                _logger.LogInformation($"City with ID {cityid} was not found when accessing points of interest");
                return NotFound();
            }

            IEnumerable<PointOfInterest> pointsOfInterestForCity = await _cityInfoRepository.GetPointsOfInterestForCityAsync(cityid);

            // map the points of interest entities to points of interest DTOs
            // and return OK
            return Ok(_mapper.Map<IEnumerable<PointOfInterestDTO>>(pointsOfInterestForCity));
        }

        [HttpGet("{pointOfInterestId}", Name = "GetPointOfInterest")]
        public async Task<ActionResult<PointOfInterestDTO>> GetPointOfInterest(int cityid, int pointOfInterestId)
        {
            // return 404 when the city with the paased in city id does not exist 
            // or when the point of interest requested does not exist

            if (!await _cityInfoRepository.CityExistsAsync(cityid))
            {
                _logger.LogInformation($"City with ID {cityid} was not found when accessing a point of interest");
                return NotFound();
            }

            PointOfInterest? pointOfInterest = await _cityInfoRepository.GetPointOfInterestForCityAsync(cityid, pointOfInterestId);

            if (pointOfInterest == null)
            {
                _logger.LogInformation($"Point of interest with ID {pointOfInterestId} was not found when accessing a point of interest for the city with id {cityid}");
                return NotFound();
            }

            return Ok(_mapper.Map<PointOfInterestDTO>(pointOfInterest));
        }

        [HttpPost]
        public async Task<ActionResult<PointOfInterestDTO>> CreatePointOfInterest(int cityId, PointOfInterestForCreationDTO pointOfInterest)
        {

            // find if the city exists and return Not found if it does not 
            if (!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                return NotFound();
            }

            PointOfInterest? finalPointOfInterest = _mapper.Map<PointOfInterest>(pointOfInterest);

            // add the point of interest 
            await _cityInfoRepository.AddPointOfInterestForCityAsync(cityId, finalPointOfInterest);

            await _cityInfoRepository.SaveChangesAsync();

            PointOfInterestDTO createdPointOfInterestToReturn = _mapper.Map<PointOfInterestDTO>(finalPointOfInterest);

            return CreatedAtRoute("GetPointOfInterest",
                new
                {
                    cityId = cityId,
                    pointOfInterestId = createdPointOfInterestToReturn.Id
                }, createdPointOfInterestToReturn
                );
        }

        [HttpPut("{pointOfInterestId}")]
        public async Task<ActionResult> UpdatePointOfInterest(int cityId, int pointOfInterestId, PointOfInterestForUpdateDTO pointOfInterest)
        {
            // find if the city exists and return Not found if it does not 
            if (!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                return NotFound();
            }

            // check if the point of interest which we want to update exists
            // if not, return Not found 
            PointOfInterest? pointOfInterestEntity = await _cityInfoRepository.GetPointOfInterestForCityAsync(cityId, pointOfInterestId);
            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }

            _mapper.Map(pointOfInterest, pointOfInterestEntity);

            await _cityInfoRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{pointOfInterestId}")]
        public async Task<ActionResult> PartiallyUpdatePointOfInterest(int cityId, int pointOfInterestId, JsonPatchDocument<PointOfInterestForUpdateDTO> jsonPatchDocument)
        {
            // find our entity and map it to a DTO, then apply the patch document to that DTO 
            // check if the city exists, return Not found if not
            if (!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                return NotFound();
            }

            // check if the point of interest which we want to update exists
            // if not, return Not found 
            PointOfInterest? pointOfInterestEntity = await _cityInfoRepository.GetPointOfInterestForCityAsync(cityId, pointOfInterestId);
            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }

            // map the entity to PointOfInterestToUpdateDTO 
            PointOfInterestForUpdateDTO pointOfInterestToPatch = _mapper.Map<PointOfInterestForUpdateDTO>(pointOfInterestEntity);

            // apply the patch document to the DTO 
            jsonPatchDocument.ApplyTo(pointOfInterestToPatch, ModelState);

            // apply validation on the DTO 
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(pointOfInterestToPatch))
            {
                return BadRequest(ModelState);
            }

            // now the DTO (the model) is valid 

            // map the changes back to the entity 
            _mapper.Map(pointOfInterestToPatch, pointOfInterestEntity);

            // persist to the database
            await _cityInfoRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{pointOfInterestId}")]
        public async Task<IActionResult> DeletePointOfInterest(int cityId, int pointOfInterestId)
        {
            // check if the city exists, return Not found if not
            if (!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                return NotFound();
            }

            // check if the point of interest which we want to delete exists
            // if not, return Not found 
            PointOfInterest? pointOfInterestEntity = await _cityInfoRepository.GetPointOfInterestForCityAsync(cityId, pointOfInterestId);
            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }

            // delete the point of interest 
            _cityInfoRepository.DeletePointOfInterest(pointOfInterestEntity); 

            // persist the changes to the DB
            await _cityInfoRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
