using Dapper;
using Microsoft.AspNetCore.Identity;
using System.Data;
using System.Runtime.CompilerServices;
using Trading_API.Interfaces;
using Trading_API.Requests;
using Trading_API.Resposes;

namespace Trading_API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        public UserRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }
        public async Task<int> LoginUser(LoginRequest loginRequest)
        {
            var hashedPassword = new PasswordHasher<object?>().HashPassword(null, loginRequest.Password);          
            var parameters = new DynamicParameters();
            parameters.Add(@"Email", loginRequest.Email);
            parameters.Add(@"Password", hashedPassword);
            parameters.Add(@"IdUser", dbType: DbType.Int32, direction: ParameterDirection.Output);
            using (var connection = _dbConnectionFactory.ConnectToDataBase())
            {               
                await connection.ExecuteAsync("UserLogin", parameters, commandType: CommandType.StoredProcedure);
                var result =  parameters.Get<int>("IdUser");
                return result;
                
            }
            
        }
        public async Task<int> RegisterUser(RegisterRequest registerRequest)
        {
            var hashedPassword = new PasswordHasher<object?>().HashPassword(null, registerRequest.Password);
            var parameters = new DynamicParameters();
            parameters.Add(@"Email", registerRequest.Email);
            parameters.Add(@"Password", hashedPassword);
            parameters.Add(@"FirstName", registerRequest.FirstName);
            parameters.Add(@"LastName", registerRequest.LastName);
            parameters.Add(@"SafeWord", registerRequest.SafeWord);
            parameters.Add(@"IdUser", dbType: DbType.Int32, direction: ParameterDirection.Output);
            using (var connection = _dbConnectionFactory.ConnectToDataBase())
            {
                await connection.ExecuteAsync("UserRegister", parameters, commandType: CommandType.StoredProcedure);

                var result = parameters.Get<int>("IdUser");
                return result;
            }
        }
        public async Task<int> GetUserRoleById(int IdUser)
        {
            var parameters = new DynamicParameters();
            parameters.Add(@"IdUser", IdUser);
            parameters.Add(@"Role", dbType: DbType.Int32, direction: ParameterDirection.Output);
            using (var connection = _dbConnectionFactory.ConnectToDataBase())
            {
                await connection.ExecuteAsync("GetRoleByUserId", parameters, commandType: CommandType.StoredProcedure);
                var result = parameters.Get<int>("Role");
                return result;
            }
        }
        public async Task<int> UpdatePassword(UpdatePasswordRequest updatePasswordRequest)
        {
            var parameters = new DynamicParameters();
            parameters.Add(@"IdUser", updatePasswordRequest.IdUser);
            parameters.Add(@"SafeWord", updatePasswordRequest.SafeWord);
            parameters.Add(@"NewPassword", updatePasswordRequest.Password);
            parameters.Add(@"StatusCode", dbType: DbType.Int32, direction: ParameterDirection.Output);
            using (var connection = _dbConnectionFactory.ConnectToDataBase())
            {
                await connection.ExecuteAsync("UpdatePassword", parameters, commandType: CommandType.StoredProcedure);
                var result = parameters.Get<int>("StatusCode");
                return result;
            }
        }
    }
}
