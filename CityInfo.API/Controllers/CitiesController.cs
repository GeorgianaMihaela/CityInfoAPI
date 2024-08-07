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

        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;

        private const int MAX_CITIES_PAGE_SIZE = 20;

        public CitiesController(ICityInfoRepository cityInfoRepository, IMapper mapper)
        {
            _cityInfoRepository = cityInfoRepository ??
                throw new ArgumentNullException(nameof(cityInfoRepository));

            _mapper = mapper ?? throw new ArgumentNullException(nameof(_mapper));
        }

        // filter for name when getting cities 
        // allow also to search for cities where any field can contain the searchString
        // implement paging 
        // important to have default values for page number and page size, so that if the consumer does not specify them 
        // they should default to something 
        [HttpGet]
        public async Task<ActionResult<IEnumerator<CityWithoutPointsOfInterestDTO>>> GetCities([FromQuery] string? name, [FromQuery] string? searchString,
            int pageNumber = 1, int pagesize = 10)
        {
            if (pagesize > MAX_CITIES_PAGE_SIZE)
            {
                pagesize = MAX_CITIES_PAGE_SIZE;
            }

            IEnumerable<City> cityEntities = await _cityInfoRepository.GetCitiesAsync(name, searchString, pageNumber, pagesize);

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
