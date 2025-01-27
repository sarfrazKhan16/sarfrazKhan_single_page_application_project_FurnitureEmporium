using System.Data;
using System.Threading.Tasks;
using Dapper;
using FurnitureEmporium.Models.Entity;
using FurnitureEmporium.Models.Interface;
using Microsoft.Data.SqlClient;

namespace FurnitureEmporium.Models.Repository
{
    public class UserRepository : IUserinterface
    {
        private readonly IDbConnection _dbConnection;

        public UserRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<bool> RegisterUserAsync(UserClass user)
        {
            if (await CheckIfEmailExistsAsync(user.Email))
            {
                return false; // Email already exists
            }

            string sql = "INSERT INTO UserTable (Username, Email, Password) VALUES (@Username, @Email, @Password)";
            var result = await _dbConnection.ExecuteAsync(sql, user);
            return result > 0;
        }

        public async Task<bool> CheckIfEmailExistsAsync(string email)
        {
            string sql = "SELECT COUNT(1) FROM UserTable WHERE Email = @Email";
            int count = await _dbConnection.ExecuteScalarAsync<int>(sql, new { Email = email });
            return count > 0;
        }

        public async Task<bool> AuthenticateUserAsync(string email, string password)
        {
            string sql = "SELECT COUNT(1) FROM UserTable WHERE Email = @Email AND Password = @Password";
            int count = await _dbConnection.ExecuteScalarAsync<int>(sql, new { Email = email, Password = password });
            return count > 0;
        }
        public async Task<string?> GetUserNameByEmailAsync(string email)
        {
            string sql = "SELECT Username FROM UserTable WHERE Email = @Email";
            
            return await _dbConnection.QueryFirstOrDefaultAsync<string>(sql, new { Email = email });
        }
        public async Task<int> GetUserIdByEmailAsync(string email)
        {
            string sql = "SELECT Id FROM UserTable WHERE Email = @Email";
            return await _dbConnection.QueryFirstOrDefaultAsync<int>(sql, new { Email = email });
        }

    }
}
