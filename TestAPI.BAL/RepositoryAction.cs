using Microsoft.Extensions.Configuration;
using TestAPI.BAL.Interface;
using TestAPI.DAL;
using Dapper;
using Microsoft.Data.SqlClient;

namespace TestAPI.BAL
{
    public class RepositoryAction : IRecordRepository
    {
        private readonly string _connectionString;

        public RepositoryAction(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("APIConnection");
        }

        public async Task<IEnumerable<Record>> GetUserAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            string sql = "SELECT * FROM [Users]";
            return await connection.QueryAsync<Record>(sql);
        }

        public async Task<int> InsertUserAsync(Record user)
        {
            using var connection = new SqlConnection(_connectionString);
            string sql = "INSERT INTO Users (FirstName, LastName, Email) VALUES (@FirstName, 'test', @Email);";
            return await connection.ExecuteAsync(sql, new { user.FirstName, user.Email });
        }
        public async Task<int> UpdateUserAsync(Record user)
        {
            using var connection = new SqlConnection(_connectionString);
            string sql = "UPDATE Users SET FirstName = @FirstName, Email = @Email WHERE Id = @Id;";
            return await connection.ExecuteAsync(sql, new { user.Id, user.FirstName, user.Email });
        }
        public async Task<Record?> GetUserByIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            string sql = "SELECT * FROM User WHERE Id = @Id";
            return await connection.QueryFirstOrDefaultAsync<Record>(sql, new { Id = id });
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString); 
            string sql = "DELETE FROM Users WHERE Id = @Id";
            int Isdeleted = await connection.ExecuteAsync(sql, new { Id = id });
            return Isdeleted > 0;
        }

    }
}
