
namespace DataAccessLibrary
{
    public interface ISqlDataAccess
    {
        string ConnectionString { get; set; }
        Task<List<T>> LoadData<T, U>(string sql, U parameters, bool isStoredProcedure = false);
        Task SaveData<T>(string sql, T parameters, bool isStoredProcedure = false);
    }
}