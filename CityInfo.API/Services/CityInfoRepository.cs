using CityInfo.API.DBContexts;
using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System.Linq;

namespace CityInfo.API.Services
{
    public class CityInfoRepository : ICityInfoRepository
    {
        // this class deals with the DB operations and uses the Db context 
        // this is built according to the Repository pattern

        private readonly CityInfoContext _context;

        // constructor injection here of the DB context
        public CityInfoRepository(CityInfoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // return all the cities and does not contain the points of interest
        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            return await _context.Cities.OrderBy(c => c.Name).ToListAsync();
        }

        // filter cities by name and allow searching
        public async Task<IEnumerable<City>> GetCitiesAsync(string? name, string? searchString)
        {
            // if the name is null or empty, return all cities 
            // also if the searchString is empty or null, return all cities 
            if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(searchString))
            {
                return await GetCitiesAsync();
            }

            IQueryable<City> cityCollection = _context.Cities as IQueryable<City>;

            // add our filter first 
            if (!string.IsNullOrWhiteSpace(name))
            {
                name = name.Trim(); // get rid of unwanted spaces 
                cityCollection = cityCollection.Where(c => c.Name == name); // filter the cities 
            }

            // implement the search 
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                searchString = searchString.Trim();
                // search for the string in all the fields, only if the description is not null 
                cityCollection = cityCollection.Where(a => a.Name.Contains(searchString)
                || a.Description != null && a.Description.Contains(searchString));
            }

            // return a list with the filtered objects 
            return await cityCollection.OrderBy(c => c.Name).ToListAsync();
        }

        // return a certain city and the consumer can decide if the points of interest are returned or not
        public async Task<City?> GetCityAsync(int cityId, bool includePointsOfInterest)
        {
            if (includePointsOfInterest)
            {
                // the Include from LINQ returns related entities 
                return await _context.Cities.Include(c => c.PointsOfInterest)
                    .Where(c => c.Id == cityId).FirstOrDefaultAsync();
            }

            return await _context.Cities
                .Where(c => c.Id == cityId).FirstOrDefaultAsync();
        }

        // return a certain point of interest for a city
        public async Task<PointOfInterest?> GetPointOfInterestForCityAsync(int cityId, int pointOfInterestId)
        {
            return await _context.PointOfInterests
                 .Where(p => p.CityId == cityId && p.Id == pointOfInterestId)
                 .FirstOrDefaultAsync();
        }

        // return all the points of interest for a city 
        public async Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityId)
        {
            return await _context.PointOfInterests
                .Where(p => p.CityId == cityId).ToListAsync();
        }

        public async Task<bool> CityExistsAsync(int cityId)
        {
            return await _context.Cities.AnyAsync(c => c.Id == cityId);
        }

        public async Task AddPointOfInterestForCityAsync(int cityId, PointOfInterest pointOfInterest)
        {
            City? city = await GetCityAsync(cityId, false);

            if (city != null)
            {
                city.PointsOfInterest.Add(pointOfInterest);
                // does not add to the DB yet, just add it to the context
                // to persist, we need to save changes 
            }
        }

        // this is where we persist changes to the DB
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

        public void DeletePointOfInterest(PointOfInterest pointOfInterest)
        {
            // no need to be async because Delete is not I/O operation 
            // just call remove on the point of interest DB set 
            _context.PointOfInterests.Remove(pointOfInterest);
        }
    }
}
