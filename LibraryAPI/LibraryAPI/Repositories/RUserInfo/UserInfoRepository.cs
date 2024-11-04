using Dapper;
using LibraryAPI.Config;
using LibraryAPI.Dto;
using LibraryAPI.Models;

namespace LibraryAPI.Repositories.RUserInfo
{
    public class UserInfoRepository : IUserInfoRepository
    {
        private readonly ConnectionContext context;
        public UserInfoRepository(ConnectionContext context)
        {
            this.context = context;
        }

        public async Task<UserInfo?> GetByIdAsync(int id)
        {
            using (var connection = context.CreateConnection())
            {

                string query = """
                SELECT * FROM user_info
                WHERE user_id = @id
                """;
                UserInfo? userInfo = await connection.QueryFirstOrDefaultAsync<UserInfo>(query, new
                {
                    id
                });
                return userInfo;
            }
        }

        public async Task<bool> ExistsByIdAsync(int id)
        {
            using (var connection = context.CreateConnection())
            {
                string query = """
                SELECT 
                    CASE WHEN EXISTS( SELECT 1 FROM user_info WHERE user_id = @id ) THEN 1
                    ELSE 0
                    END
                """;
                bool exists = await connection.QueryFirstAsync<bool>(query, new
                {
                    id
                });
                return exists;
            }
        }

        public async Task<List<UserInfo>> GetAllAsync()
        {
            using (var connection = context.CreateConnection())
            {
                string query = """
                SELECT * FROM user_info
                """;
                IEnumerable<UserInfo> userInfos = await connection.QueryAsync<UserInfo>(query);
                return userInfos.ToList();
            }
        }

        public async Task<UserInfo?> GetByUsernameAsync(string username)
        {
            using (var connection = context.CreateConnection())
            {
                string query = """
                SELECT * FROM user_info
                WHERE username = @username
                """;
                UserInfo? userInfo = await connection.QueryFirstOrDefaultAsync<UserInfo>(query, new {
                    username
                });
                return userInfo;
            }
        }

        public async Task<bool> ExistsByUsernameAsync(string username) {
            using (var connection = context.CreateConnection()) {
                string query = """
                SELECT 
                    CASE WHEN EXISTS( SELECT 1 FROM user_info WHERE username = @username ) THEN 1
                    ELSE 0
                    END
                """;
                bool exists = await connection.QueryFirstAsync<bool>(query, new {
                    username
                });
                return exists;
            }
        }

        public async Task<int> InsertAsync(UserInfo entity)
        {
            using (var connection = context.CreateConnection())
            {
                string query = """
                INSERT INTO user_info (username, password, role_id)
                VALUES (@UserName, @Password, @RoleId)
                """;
                return await connection.ExecuteAsync(query, entity);
            }
        }

        public async Task<int> UpdateAsync(UserInfo entity)
        {
            using (var connection = context.CreateConnection())
            {
                string query = """
                UPDATE user_info
                SET username = @Username,
                role_id = @RoleId
                WHERE user_id = @UserId
                """;
                return await connection.ExecuteAsync(query, entity);
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            using (var connection = context.CreateConnection())
            {
                string query = """
                DELETE FROM user_info WHERE user_id = @id
                """;
                return await connection.ExecuteAsync(query, new
                {
                    id
                });
            }
        }

        public async Task<UserInfo?> GetByCredentials(CredentialsDto credentials) {
            using (var connection = context.CreateConnection()) {

                string query = """
                SELECT * FROM user_info
                WHERE username = @Username
                AND password = @Password
                """;
                return await connection.QueryFirstOrDefaultAsync<UserInfo>(query, credentials);
            }
        }
    }
}

