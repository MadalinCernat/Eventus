using DataAccessLibrary.Models;

namespace DataAccessLibrary
{
    public partial class SqlCrud
    {
        public async Task<List<CityModel>> GetAllCities()
        {
            return await _db.LoadData<CityModel, dynamic>("dbo.spCity_GetAll", new { }, true);
        }

        public async Task<CityModel> GetCityById(int id)
        {
            return (await _db.LoadData<CityModel, dynamic>("dbo.spCity_GetById", new { Id = id }, true)).SingleOrDefault();
        }

        public async Task InsertCity(CityModel model)
        {
            var p = new
            {
                Name = model.Name
            };
            await _db.SaveData("dbo.spCity_Insert", p, true);
        }
    }
}