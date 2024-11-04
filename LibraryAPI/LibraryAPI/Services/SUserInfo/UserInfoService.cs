using LibraryAPI.Dto;
using LibraryAPI.Models;
using LibraryAPI.Repositories.RUserInfo;
using System.Security.Cryptography;

namespace LibraryAPI.Services.SUserInfo
{
    public class UserInfoService : IUserInfoService {
        private readonly IUserInfoRepository userInfoRepository;
        public UserInfoService(IUserInfoRepository userInfoRepository) {
            this.userInfoRepository = userInfoRepository;
        }

        public async Task<UserInfo?> GetById(int id) {
            return await userInfoRepository.GetByIdAsync(id);
        }

        public async Task<List<UserInfo>> GetAll() {
            return await userInfoRepository.GetAllAsync();
        }

        public async Task<bool> ExistsById(int id) {
            return await userInfoRepository.ExistsByIdAsync(id);
        }

        public async Task<UserInfo?> GetByUsername(string username) {
            return await userInfoRepository.GetByUsernameAsync(username);
        }

        public async Task<bool> ExistsByUsername(string username) {
            return await userInfoRepository.ExistsByUsernameAsync(username);
        }

        public async Task Insert(UserInfo userInfo) {
            // Use md5 hash to ensure data security
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create()) {
                byte[] pwBytes = System.Text.Encoding.ASCII.GetBytes(userInfo.Password);
                byte[] hashBytes = md5.ComputeHash(pwBytes);

                userInfo.Password = Convert.ToHexString(hashBytes);
                await userInfoRepository.InsertAsync(userInfo);
            }
        }
        public async Task Update(UserInfo userInfo) {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create()) {
                byte[] pwBytes = System.Text.Encoding.ASCII.GetBytes(userInfo.Password);
                byte[] hashBytes = md5.ComputeHash(pwBytes);

                userInfo.Password = Convert.ToHexString(hashBytes);
                await userInfoRepository.UpdateAsync(userInfo);
            }
        }
        public async Task Delete(int id) {
            await userInfoRepository.DeleteAsync(id);
        }

        public async Task<UserInfo?> LoginWithCredentials(CredentialsDto credentials) {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create()) {
                byte[] pwBytes = System.Text.Encoding.ASCII.GetBytes(credentials.Password);
                byte[] hashBytes = md5.ComputeHash(pwBytes);

                credentials.Password = Convert.ToHexString(hashBytes);
                return await userInfoRepository.GetByCredentials(credentials);
            }
        }
    }
}
