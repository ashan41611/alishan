using CDA_EnvironmentWebPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDA_EnvironmentWebPortal.Data.Repository
{
    public interface IGenericRepository
    {
        Task<List<T>> GetAll<T>(string query, object parameters = null);
        Task<IEnumerable<T>> GetAllAsync<T>();
        Task<T> GetByIdAsync<T>(int id);
        Task<int> InsertAsync<T>(T entity);
        Task<int> UpdateAsync<T>(T entity);
        Task<int> DeleteAsync<T>(int id);
        Task<IEnumerable<T>> ExecuteStoredProcedureAsync<T>(string storedProcedureName, object parameters = null);
        Task<IEnumerable<T>> GetByWhereAsync<T>(string tableName, string columnName, object columnValue);
        Task<IEnumerable<T>> GetByWhereAsync<T>(string tableName, IDictionary<string, object> columnValues);
		Task<User> Login(string username, string password);
        Task<User> Register(User user, string password);
        Task<bool> UserExists(string username);
        Task<List<GetDutyRoasterVm>> GetDutyRoaster();
        Task<List<GetDutyRoasterVm>> GetDutyRoasterV1(DataTablesRequest request);
        Task<List<Zone>> GetZone(DataTablesRequest request);



        Task<List<GetEmployee>> GetDutyRoasterEmployeeV1(DataTablesRequest request,int SID);




    }
}