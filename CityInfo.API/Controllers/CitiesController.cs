using AutoMapper;
using CityInfo.API.Entities;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class CitiesController : ControllerBase
    {
        // private readonly CitiesDataStore citiesDataStore;
        //public CitiesController(CitiesDataStore citiesDataStore)
        //{
        //    this.citiesDataStore = citiesDataStore;
        //}

        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;

        public CitiesController(ICityInfoRepository cityInfoRepository, IMapper mapper)
        {
            _cityInfoRepository = cityInfoRepository ??
                throw new ArgumentNullException(nameof(cityInfoRepository));

            _mapper = mapper ?? throw new ArgumentNullException(nameof(_mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerator<CityWithoutPointsOfInterestDTO>>> GetCities()
        {
            IEnumerable<City> cityEntities = await _cityInfoRepository.GetCitiesAsync();

            return Ok(_mapper.Map<IEnumerable<CityWithoutPointsOfInterestDTO>>(cityEntities));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCity(int id, bool includePointsOfInterest = false)
        {
            City? city = await _cityInfoRepository.GetCityAsync(id, includePointsOfInterest);
            if (city == null)
            {
                return NotFound();
            }

            if (includePointsOfInterest)
            {
                return Ok(_mapper.Map<CityDTO>(city));
            }

            return Ok(_mapper.Map<CityWithoutPointsOfInterestDTO>(city));
        }
    }
}
