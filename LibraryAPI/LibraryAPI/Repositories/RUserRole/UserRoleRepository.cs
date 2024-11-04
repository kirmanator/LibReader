using Dapper;
using LibraryAPI.Config;
using LibraryAPI.Models;

namespace LibraryAPI.Repositories.RUserRole
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly ConnectionContext context;
        public UserRoleRepository(ConnectionContext context)
        {
            this.context = context;
        }

        public async Task<UserRole?> GetByIdAsync(int id)
        {
            using (var connection = context.CreateConnection())
            {

                string query = """
                SELECT * FROM user_role
                WHERE role_id = @id
                """;
                UserRole? role = await connection.QueryFirstOrDefaultAsync<UserRole>(query, new
                {
                    id
                });
                return role;
            }
        }

        public async Task<bool> ExistsByIdAsync(int id)
        {
            using (var connection = context.CreateConnection())
            {
                string query = """
                SELECT
                    CASE WHEN EXISTS( SELECT 1 FROM user_role WHERE role_id = @id ) THEN 1
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

        public async Task<List<UserRole>> GetAllAsync()
        {
            using (var connection = context.CreateConnection())
            {
                string query = """
                SELECT * FROM user_role
                """;
                IEnumerable<UserRole> roles = await connection.QueryAsync<UserRole>(query);
                return roles.ToList();
            }
        }

        public async Task<int> InsertAsync(UserRole entity)
        {
            using (var connection = context.CreateConnection())
            {
                string query = """
                INSERT INTO user_role (role_id, name)
                VALUES (@RoleId, @Name)
                """;
                return await connection.ExecuteAsync(query, entity);
            }
        }

        public async Task<int> UpdateAsync(UserRole entity)
        {
            using (var connection = context.CreateConnection())
            {
                string query = """
                UPDATE user_role
                SET name = @Name
                WHERE role_id = @RoleId
                """;
                return await connection.ExecuteAsync(query, entity);
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            using (var connection = context.CreateConnection())
            {
                string query = """
                DELETE FROM user_role WHERE role_id = @id
                """;
                return await connection.ExecuteAsync(query, new
                {
                    id
                });
            }
        }
    }
}

