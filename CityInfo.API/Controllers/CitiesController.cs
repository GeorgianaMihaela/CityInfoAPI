using CityInfo.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class CitiesController : ControllerBase
    {
        private readonly CitiesDataStore citiesDataStore;
        public CitiesController(CitiesDataStore citiesDataStore)
        {
            this.citiesDataStore = citiesDataStore;
        }
        [HttpGet]
        public ActionResult<IEnumerator<CityDTO>> GetCities()
        {
            return Ok(citiesDataStore.Cities);
        }
        [HttpGet("{id}")]
        public ActionResult<CityDTO> GetCity(int id)
        {
            // find city 
            CityDTO? cityToReturn = citiesDataStore.Cities.FirstOrDefault(c => c.Id == id);
            if (cityToReturn == null)
            {
                return NotFound();
            }
            return Ok(cityToReturn);
        }
    }
}
