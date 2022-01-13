
namespace DataAccessLibrary
{
    public interface ISqlDataAccess
    {
        Task<List<T>> LoadData<T, U>(string sql, U parameters, bool isStoredProcedure = false);
        Task SaveData<T>(string sql, T parameters, bool isStoredProcedure = false);
    }
}