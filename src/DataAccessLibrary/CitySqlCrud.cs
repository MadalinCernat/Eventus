using DataAccessLibrary.Models;
using Microsoft.Extensions.Caching.Memory;

namespace DataAccessLibrary
{
    public partial class SqlCrud
    {
        private const string cityCacheKey = "cities";
        public async Task<List<CityModel>> GetAllCities()
        {
            var output = _cache.Get <List<CityModel>>(cityCacheKey);
            if(output is null)
            {
                output = await _db.LoadData<CityModel, dynamic>("dbo.spCity_GetAll", new { }, true);
                _cache.Set(cityCacheKey, output, TimeSpan.FromSeconds(30));
            }

            return output;
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