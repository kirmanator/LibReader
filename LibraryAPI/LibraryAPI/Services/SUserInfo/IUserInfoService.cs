using LibraryAPI.Dto;
using LibraryAPI.Models;

namespace LibraryAPI.Services.SUserInfo
{
    public interface IUserInfoService : IGenericService<UserInfo>
    {
        Task<UserInfo?> GetByUsername(string username);
        Task<bool> ExistsByUsername(string username);
        Task<UserInfo?> LoginWithCredentials(CredentialsDto dto);
    }
}
