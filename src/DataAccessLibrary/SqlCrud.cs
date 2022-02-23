namespace DataAccessLibrary
{
    public partial class SqlCrud : ISqlCrud
    {
        private readonly ISqlDataAccess _db;

        public SqlCrud(ISqlDataAccess db)
        {
            _db = db;
        }
    }
}
