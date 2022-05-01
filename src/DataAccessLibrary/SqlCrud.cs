using Microsoft.Extensions.Caching.Memory;

namespace DataAccessLibrary
{
    public partial class SqlCrud : ISqlCrud
    {
        private readonly ISqlDataAccess _db;
        private readonly IMemoryCache _cache;

        public SqlCrud(ISqlDataAccess db, IMemoryCache cache)
        {
            _db = db;
            _cache = cache;
        }
    }
}
