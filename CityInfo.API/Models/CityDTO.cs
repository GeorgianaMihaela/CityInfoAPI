namespace CityInfo.API.Models
{
    public class CityDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string? Description { get; set; }

        public List<PointOfInterestDTO> PointsOfInterest { get; set; } = new List<PointOfInterestDTO>();
        // do not leave this not created to avoid null reference exception 

        public int NumberOfPointsOfInterest
        {
            get
            {
                return PointsOfInterest.Count;
            }
        }

    }
}
