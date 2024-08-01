using AutoMapper;
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
            var cityEntities = await _cityInfoRepository.GetCitiesAsync();

            return Ok(_mapper.Map<IEnumerable<CityWithoutPointsOfInterestDTO>>(cityEntities));
        }

        [HttpGet("{id}")]
        public ActionResult<CityDTO> GetCity(int id)
        {
            // find city 
            // CityDTO? cityToReturn = citiesDataStore.Cities.FirstOrDefault(c => c.Id == id);
            // if (cityToReturn == null)
            // {
            //     return NotFound();
            // }
            // return Ok(cityToReturn);
            return Ok(); 
        }
    }
}
