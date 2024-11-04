using LibraryAPI.Dto;
using LibraryAPI.Models;

namespace LibraryAPI.Repositories.RUserInfo
{
    public interface IUserInfoRepository : IGenericRepository<UserInfo>
    {
        Task<UserInfo?> GetByUsernameAsync(string username);
        Task<bool> ExistsByUsernameAsync(string username);
        Task<UserInfo?> GetByCredentials(CredentialsDto credentials);
    }
}
