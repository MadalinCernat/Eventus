using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public class SqlCrud
    {
        private readonly ISqlDataAccess _db;

        public SqlCrud(ISqlDataAccess db)
        {
            _db = db;
        }
        public Task<List<EventModel>> GetAllEvents()
        {
            return _db.LoadData<EventModel, dynamic>("dbo.spEvent_GetAll", new { }, true);
        }
    }
}
