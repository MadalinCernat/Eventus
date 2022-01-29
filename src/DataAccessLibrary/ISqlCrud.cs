using DataAccessLibrary.Models;

namespace DataAccessLibrary
{
    public interface ISqlCrud
    {
        Task<List<EventModel>> GetAllEvents();
        Task<List<PlaceModel>> GetAllPlaces();
    }
}