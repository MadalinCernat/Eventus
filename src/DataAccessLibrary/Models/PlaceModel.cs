namespace DataAccessLibrary.Models
{
    public class PlaceModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CityModel City { get; set; }
    }
}