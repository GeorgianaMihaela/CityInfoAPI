﻿using AutoMapper;
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
        //private readonly CitiesDataStore _dataStore;

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
            // find city 
            //CityDTO? cityToReturn = _dataStore.Cities.FirstOrDefault(c => c.Id == cityid);
            //if (cityToReturn == null)
            //{
            //    _logger.LogWarning($"City with id {cityid} was not found when accessing points of interest"); 
            //    return NotFound(); ;
            //}
            //return Ok(cityToReturn.PointsOfInterest);
            if (!await _cityInfoRepository.CityExistsAsync(cityid))
            {
                _logger.LogInformation($"City with ID {cityid} was not found when accessing points of interest");
                return NotFound();
            }

            IEnumerable<PointOfInterest> pointsOfInterestForCity = await _cityInfoRepository.GetPointsOfInterestForCityAsync(cityid);

            return Ok(_mapper.Map<IEnumerable<PointOfInterestDTO>>(pointsOfInterestForCity));
        }

        [HttpGet("{pointOfInterestId}", Name = "GetPointOfInterest")]
        public async Task<ActionResult<PointOfInterestDTO>> GetPointOfInterest(int cityid, int pointOfInterestId)
        {
            // return 404 when the city with the paased in city id does not exist 
            // or when the point of interest requested does not exist
            // find city 
            //CityDTO? cityToReturn = _dataStore.Cities.FirstOrDefault(c => c.Id == cityid);
            //if (cityToReturn == null)
            //{
            //    return NotFound(); ;
            //}

            //// find point of interest
            //PointOfInterestDTO? pointOfInterestDTO = cityToReturn.PointsOfInterest.FirstOrDefault(p => p.Id == pointOfInterestId);

            //if (pointOfInterestDTO == null)
            //{
            //    return NotFound(); ;
            //}

            //return Ok(pointOfInterestDTO);

            if(!await _cityInfoRepository.CityExistsAsync(cityid))
            {
                _logger.LogInformation($"City with ID {cityid} was not found when accessing a point of interest");
                return NotFound();
            }

            PointOfInterest? pointOfInterest = await _cityInfoRepository.GetPointOfInterestForCityAsync(cityid, pointOfInterestId);

            if (pointOfInterest == null) {
                _logger.LogInformation($"Point of interest with ID {pointOfInterestId} was not found when accessing a point of interest for the city with id {cityid}");
                return NotFound();
            }

            return Ok(_mapper.Map<PointOfInterestDTO>(pointOfInterest));
        }

        [HttpPost]
        public ActionResult<PointOfInterestDTO> CreatePointOfInterest(int cityId, PointOfInterestForCreationDTO pointOfInterest)
        {

            // find if the city exists and return Not found if it does not 
            //CityDTO? city = _dataStore.Cities.FirstOrDefault(c => c.Id == cityId);

            //if (city == null)
            //{
            //    return NotFound(); ;
            //}

            //// calculate the ID of the new point of interest 
            //int maxPointId = _dataStore.Cities.SelectMany(c => c.PointsOfInterest).Max(p => p.Id);

            //// map PointofInterestForCreationDTO to PointOfInterestDTO 
            //PointOfInterestDTO createdPointOfInterest = new PointOfInterestDTO()
            //{
            //    Id = ++maxPointId,
            //    Name = pointOfInterest.Name,
            //    Description = pointOfInterest.Description
            //};

            //// add the new point of interest to the list 
            //city.PointsOfInterest.Add(createdPointOfInterest);

            //// return a response with Location header 
            //return CreatedAtRoute("GetPointOfInterest", new
            //{
            //    cityId = cityId,
            //    pointOfInterestId = createdPointOfInterest.Id
            //},
            //createdPointOfInterest
            //);
            return Ok();
        }

        [HttpPut("{pointOfInterestId}")]
        public ActionResult UpdatePointOfInterest(int cityId, int pointOfInterestId, PointOfInterestForUpdateDTO pointOfInterest)
        {
            // if we cannot find the city, return not found 
            //CityDTO? city = _dataStore.Cities.FirstOrDefault(c => c.Id == cityId);
            //if (city == null)
            //{
            //    return NotFound(); ;
            //}

            //// check if we find the point of interest which we need to update 
            //// return not found id we do not find it 
            //PointOfInterestDTO? pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(c => c.Id == pointOfInterestId);
            //if (pointOfInterestFromStore == null)
            //{
            //    return NotFound();
            //}

            //// PUT should fully update the resource 
            //// if any field is not sent, we set it to default
            //pointOfInterestFromStore.Name = pointOfInterest.Name;
            //pointOfInterestFromStore.Description = pointOfInterest.Description;

            return NoContent();
        }

        [HttpPatch("{pointOfInterestId}")]
        public ActionResult PartiallyUpdatePointOfInterest(int cityId, int pointOfInterestId, JsonPatchDocument<PointOfInterestForUpdateDTO> jsonPatchDocument)
        {
            // look for the city and send not found in case it does not exist 
            //CityDTO? city = _dataStore.Cities.FirstOrDefault(c => c.Id == cityId);
            //if (city == null)
            //{
            //    return NotFound();
            //}

            //// apply the patch document 

            //// first find the point of interest to be updated 
            //// return Not Found if that does not exist 
            //PointOfInterestDTO? pointOfInterestFromStore = city?.PointsOfInterest.FirstOrDefault(c => c.Id == pointOfInterestId);
            //if (pointOfInterestFromStore == null)
            //{
            //    return NotFound();
            //}

            //// map the PointOfInterestDTO to <PointOfInterestForUpdateDTO
            //PointOfInterestForUpdateDTO pointOfInterestToPatch = new PointOfInterestForUpdateDTO()
            //{
            //    Name = pointOfInterestFromStore.Name,
            //    Description = pointOfInterestFromStore.Description
            //};

            //// now we can apply the patch document 
            //jsonPatchDocument.ApplyTo(pointOfInterestToPatch, ModelState);

            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //// make sure that the name is not null after applying the patch document 
            //if (!TryValidateModel(pointOfInterestToPatch))
            //{
            //    return BadRequest(ModelState);
            //}

            //pointOfInterestFromStore.Name = pointOfInterestToPatch.Name;
            //pointOfInterestFromStore.Description = pointOfInterestToPatch.Description;


            return NoContent();
        }

        [HttpDelete("{pointOfInterestId}")]
        public IActionResult DeletePointOfInterest(int cityId, int pointOfInterestId)
        {
            // look for the city and send not found in case it does not exist 
            //CityDTO? city = _dataStore.Cities.FirstOrDefault(c => c.Id == cityId);
            //if (city == null)
            //{
            //    return NotFound();
            //}

            //// first find the point of interest to be deleted
            //// return Not Found if that does not exist 
            //PointOfInterestDTO? pointOfInterestFromStore = city?.PointsOfInterest.FirstOrDefault(c => c.Id == pointOfInterestId);
            //if (pointOfInterestFromStore == null)
            //{
            //    return NotFound();
            //}

            //city?.PointsOfInterest.Remove(pointOfInterestFromStore);

            return NoContent();
        }
    }
}
