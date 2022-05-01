using System.Data;
using System.Data.SqlClient;
using Dapper;
using DataAccessLibrary.Models;
using Microsoft.Extensions.Caching.Memory;

namespace DataAccessLibrary
{
    public partial class SqlCrud
    {
        private const string placeCacheKey = "places";
        private async Task<List<PlaceModel>> ExecutePlaceCrudSql(string sql, object param = null)
        {
            using IDbConnection conn = new SqlConnection(_db.ConnectionString);
            return (await conn.QueryAsync<PlaceModel, CityModel, PlaceModel>(
                sql,
                (place, city) => { place.City = city; return place; },
                param: param ?? null,
                splitOn: "CityId",
                commandType: CommandType.StoredProcedure)).ToList();
        }

        public async Task<List<PlaceModel>> GetAllPlaces()
        {
            var output = _cache.Get <List<PlaceModel>>(placeCacheKey);

            if(output is null)
            {
                output = await ExecutePlaceCrudSql("dbo.spPlace_GetAll");
                _cache.Set(placeCacheKey, output, TimeSpan.FromMinutes(1));
            }

            return output;
        }

        public async Task<PlaceModel> GetPlaceById(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", id);
            return (await ExecutePlaceCrudSql("dbo.spPlace_GetById", parameters))?.FirstOrDefault();
        }

        public async Task InsertPlace(PlaceModel model)
        {
            var p = new
            {
                Name = model.Name,
                CityId = model.City.Id
            };
            await _db.SaveData("dbo.spPlace_Insert", p, true);
        }
    }
}