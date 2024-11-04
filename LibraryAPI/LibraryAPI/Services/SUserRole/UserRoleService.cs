using LibraryAPI.Models;
using LibraryAPI.Repositories.RUserRole;

namespace LibraryAPI.Services.SUserRole
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository userRoleRepository;
        public UserRoleService(IUserRoleRepository userRoleRepository)
        {
            this.userRoleRepository = userRoleRepository;
        }

        public async Task<UserRole?> GetById(int id)
        {
            return await userRoleRepository.GetByIdAsync(id);
        }

        public async Task<List<UserRole>> GetAll()
        {
            return await userRoleRepository.GetAllAsync();
        }

        public async Task<bool> ExistsById(int id)
        {
            return await userRoleRepository.ExistsByIdAsync(id);
        }

        public async Task Insert(UserRole userInfo)
        {
            await userRoleRepository.InsertAsync(userInfo);
        }
        public async Task Update(UserRole userInfo)
        {
            await userRoleRepository.UpdateAsync(userInfo);
        }
        public async Task Delete(int id)
        {
            await userRoleRepository.DeleteAsync(id);
        }

    }
}
