using DataAccessLibrary.Models;

namespace DataAccessLibrary
{
    public interface ISqlCrud
    {
        Task AcceptInvitation(int id);
        Task<List<CityModel>> GetAllCities();
        Task<List<EventModel>> GetAllEvents();
        Task<List<InvitationModel>> GetAllInvitationsSentByUser(string userId);
        Task<List<InvitationModel>> GetAllInvitationsSentToUser(string userId);
        Task<List<PlaceModel>> GetAllPlaces();
        Task<List<RequestModel>> GetAllRequestsForEventId(int eventId);
        Task<List<RequestModel>> GetAllRequestsSentByUserId(string userId);
        Task<CityModel> GetCityById(int id);
        Task<EventModel> GetEventById(int id);
        Task<List<EventModel>> GetEventsByUserId(string userId);
        Task<PlaceModel> GetPlaceById(int id);
        Task InsertCity(CityModel model);
        Task InsertEvent(EventModel model);
        Task InsertInvitation(InvitationModel model);
        Task InsertPlace(PlaceModel model);
        Task InsertRequest(RequestModel model);
        Task<List<EventModel>> GetAllEventsEntered(string userId);
    }
}