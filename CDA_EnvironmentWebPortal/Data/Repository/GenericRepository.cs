using Azure;
using Azure.Core;
using CDA_EnvironmentWebPortal.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Security.Cryptography;
using System.Text;



namespace CDA_EnvironmentWebPortal.Data.Repository
{

public class GenericRepository : IGenericRepository
{
    private readonly string _connectionString;

    public GenericRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<IEnumerable<T>> GetAllAsync<T>()
    {
        using IDbConnection db = new SqlConnection(_connectionString);
        return await db.QueryAsync<T>($"SELECT * FROM {typeof(T).Name}");
    }

        public async Task<List<T>> GetAll<T>(string query, object parameters = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
             var result= await  connection.QueryAsync<T>(query, parameters);
                return result.ToList();
            }
        }

        public async Task<T> GetByIdAsync<T>(int id)
    {
        using IDbConnection db = new SqlConnection(_connectionString);
        return await db.QueryFirstOrDefaultAsync<T>($"SELECT * FROM {typeof(T).Name} WHERE Id = @Id", new { Id = id });
    }

    public async Task<int> InsertAsync<T>(T entity)
    {
            try
            {
                using IDbConnection db = new SqlConnection(_connectionString);

                // Get properties of the entity except for properties that are of type DateTime
                var properties = typeof(T).GetProperties()
                                           .Where(prop => prop.PropertyType != typeof(DateTime))
                                           .Select(prop => prop.Name);

                // Generate column names separated by commas
                string columns = string.Join(", ", properties);

                // Generate parameter names based on property names
                string parameters = string.Join(", ", properties.Select(prop => "@" + prop));

                string sql = $"INSERT INTO {typeof(T).Name} ({columns}) VALUES ({parameters})";



                //string sql = $"INSERT INTO {typeof(T).Name} VALUES (@PropertyName1, @PropertyName2, ...)"; // replace PropertyName1, PropertyName2, ... with actual property names
                return await db.ExecuteAsync(sql, entity);
            }
            catch (Exception ex)
            {

               // ErrorLog.LogTxt("InsertAsync", ex.Message);
                return 0;
            }
   
    }

    public async Task<int> UpdateAsync<T>(T entity)
    {
        using IDbConnection db = new SqlConnection(_connectionString);
        string sql = $"UPDATE {typeof(T).Name} SET PropertyName1 = @PropertyName1, PropertyName2 = @PropertyName2 WHERE Id = @Id"; // replace PropertyName1, PropertyName2, ... with actual property names
        return await db.ExecuteAsync(sql, entity);
    }

    public async Task<int> DeleteAsync<T>(int id)
    {
        using IDbConnection db = new SqlConnection(_connectionString);
        return await db.ExecuteAsync($"DELETE FROM {typeof(T).Name} WHERE Id = @Id", new { Id = id });
    }

        public async Task<IEnumerable<T>> ExecuteStoredProcedureAsync<T>(string storedProcedureName, object parameters = null)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            return await db.QueryAsync<T>(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
        }


        public async Task<IEnumerable<T>> GetByWhereAsync<T>(string tableName, string columnName, object columnValue)
        {
            try
            {
                using IDbConnection db = new SqlConnection(_connectionString);



                // Build SQL query with parameters
                var sqlQuery = $"SELECT * FROM {tableName} WHERE {columnName} = @ColumnValue";

                // Execute query asynchronously
                return await db.QueryAsync<T>(sqlQuery, new { ColumnValue = columnValue });

            }
            catch (Exception ex)
            {

             //   ErrorLog.LogTxt("GetByWhereAsynce", ex.Message);
                return new List<T>();

              
            }
         

        }

    
        public async Task<User> Register(User user, string password)
        {
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            using (var dbConnection = new SqlConnection(_connectionString))
            {
                var query = @"
                    INSERT INTO Users (Username, PasswordHash, PasswordSalt, Department, CNIC, Mobile, ZoneId, SectorId, SubSectorId, BeatId, ScaleTypeId, ScaleNoId, EmployeeTypeId, DesignationId, WingId, DirectorateId, DivisionId)
                    VALUES (@Username, @PasswordHash, @PasswordSalt, @Department, @CNIC, @Mobile, @ZoneId, @SectorId, @SubSectorId, @BeatId, @ScaleTypeId, @ScaleNoId, @EmployeeTypeId, @DesignationId, @WingId, @DirectorateId, @DivisionId);
                    SELECT CAST(SCOPE_IDENTITY() as int)";
                user.UserId = await dbConnection.QuerySingleAsync<int>(query, user);
                return user;
            }
        }

        public async Task<bool> UserExists(string username)
        {
            using (var dbConnection = new SqlConnection(_connectionString))
            {
                var query = "SELECT COUNT(1) FROM Users WHERE Username = @Username";
                return await dbConnection.ExecuteScalarAsync<bool>(query, new { Username = username });
            }
        }

        public async Task<IEnumerable<T>> GetByWhereAsync<T>(string tableName, IDictionary<string, object> columnValues)
        {
            try
            {
                using IDbConnection db = new SqlConnection(_connectionString);

                // Build SQL query with parameters
                var sqlQuery = $"SELECT * FROM {tableName} WHERE ";

                // Build WHERE clause dynamically
                var whereClause = string.Join(" AND ", columnValues.Select(kv => $"{kv.Key} = @{kv.Key}"));
                sqlQuery += whereClause;

                // Execute query asynchronously
                return await db.QueryAsync<T>(sqlQuery, columnValues);
            }
            catch (Exception ex)
            {
                // Log any exceptions
             //   ErrorLog.LogTxt("GetByWhereAsync", ex.Message);

                // Return an empty list if an error occurs
                return new List<T>();
            }
        }

		public async Task<User> Login(string username, string password)
		{
			using (var dbConnection =  new SqlConnection(_connectionString))
			{
				var query = "SELECT * FROM Users WHERE Username = @Username";
				var user = await dbConnection.QuerySingleOrDefaultAsync<User>(query, new { Username = username });

				if (user == null || !VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
					return null;

				return user;
			}
		}
        public async Task<List<GetDutyRoasterVm>> GetDutyRoaster() {


            using (var dbConnection = new SqlConnection(_connectionString)) {

                var query = "GetEmployeeDutyRoster";

                var result = await dbConnection.QueryAsync<GetEmployee>(query,null, commandType: CommandType.StoredProcedure);

            //    var result = await dbConnection.QueryAsync<GetEmployee, GetDutyRoasterVm, GetEmployee>(
            //   query,
            //(employee, supervisor) =>
            //{
            //    var emp = new GetEmployee
            //    {
            //        EID = employee.EID,
            //        EmployeeName = employee.EmployeeName,
            //        EmployeeCNIC = employee.EmployeeCNIC,
            //        EmployeeMobile = employee.EmployeeMobile,
            //        EmployeeType = employee.EmployeeType,
            //        Zone = employee.Zone,
            //        Sector = employee.Sector,
            //        SubSector = employee.SubSector,
            //        Beat = employee.Beat,
            //        ScaleType = employee.ScaleType,
            //        ScaleNo = employee.ScaleNo,
            //        Wing = employee.Wing,
            //        Directorate = employee.Directorate,
            //        Division = employee.Division,
            //        SID = employee.SID
            //    };

            //    if (supervisor != null)
            //    {
            //        supervisor.Members ??= new List<GetEmployee>();
            //        supervisor.Members.Add(emp);
            //    }

            //    return emp;
            //},
            //commandType: CommandType.StoredProcedure,
            //splitOn: "SupervisorID"
            //    );
                var dutyRoaster = result
             .GroupBy(e => e.SID)
             .Select(g => new GetDutyRoasterVm
             {
                 SupervisorID = g.Key,
                 SupervisorName = g.First().SupervisorName,
                 SupervisorDepartment = g.First().SupervisorDepartment,
                 SupervisorCNIC = g.First().SupervisorCNIC,
                 SupervisorMobile = g.First().SupervisorMobile,
                 SupervisorZone = g.First().SupervisorZone,
                 SupervisorSector = g.First().SupervisorSector,
                 SupervisorSubSector = g.First().SupervisorSubSector,
                 SupervisorBeat = g.First().SupervisorBeat,
                 SupervisorScaleType = g.First().SupervisorScaleType,
                 SupervisorWing = g.First().SupervisorWing,
                 SupervisorDirectorate = g.First().SupervisorDirectorate,
                 SupervisorDivision = g.First().SupervisorDivision,
                 Members = g.ToList()
             }).ToList();

                return dutyRoaster;

            }
         

        }

            public async Task<List<GetDutyRoasterVm>> GetDutyRoasterV1(DataTablesRequest request) {

            var start = request.Start;
            var length = request.Length;
            var search = request.Search?.Value;

            List<GetDutyRoasterVm> response;
            using (var dbConnection = new SqlConnection(_connectionString)) {

                var query = "GetEmployeeDutyRosterV1";

                var parameters = new { Start = start, Length = length, Search = search};

                var result = await dbConnection.QueryAsync<GetDutyRoasterVm>(query, parameters, commandType: CommandType.StoredProcedure);
                 response = result.ToList();
             

                  

             
            }


            return response;

        }   
              public async Task<List<Zone>> GetZone(DataTablesRequest request)
        {

            var start = request.Start;
            var length = request.Length;
            var search = request.Search?.Value;

            List<Zone> response;
            using (var dbConnection = new SqlConnection(_connectionString)) {

                var query = "GetAllZone";

                var parameters = new { Start = start, Length = length, Search = search};

                var result = await dbConnection.QueryAsync<Zone>(query, parameters, commandType: CommandType.StoredProcedure);
                 response = result.ToList();
             

                  

             
            }


            return response;

        }   
        


        
        public async Task<List<GetEmployee>> GetDutyRoasterEmployeeV1(DataTablesRequest request, int SID)
        {

            var start = request.Start;
            var length = request.Length;
            //var search = request.Search?.Value;

            List<GetEmployee> response;
            using (var dbConnection = new SqlConnection(_connectionString)) {

                var query = "GetMemberDutyRoster";

                var parameters = new { Start = start, Length = length, @SupervisorId = SID};

                var result = await dbConnection.QueryAsync<GetEmployee>(query, parameters, commandType: CommandType.StoredProcedure);
                 response = result.ToList();
             

                  

             
            }


            return response;

        }


        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
		{
			using (var hmac = new HMACSHA512(storedSalt))
			{
				var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
				return computedHash.SequenceEqual(storedHash);
			}
		}
	}



}