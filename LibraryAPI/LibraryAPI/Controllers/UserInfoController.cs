using Microsoft.AspNetCore.Mvc;
using LibraryAPI.Models;
using LibraryAPI.Services.SUserInfo;
using LibraryAPI.Dto;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInfoController : ControllerBase {

        private readonly IUserInfoService userInfoService;

        public UserInfoController(IUserInfoService userInfoService) {
            this.userInfoService = userInfoService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll() {
            var userInfos = await userInfoService.GetAll();
            if (userInfos == null) {
                return StatusCode(500);
            }
            return Ok(userInfos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id) {
            var userInfos = await userInfoService.GetById(id);
            if (userInfos == null) {
                return StatusCode(500);
            }
            return Ok(userInfos);
        }

        [HttpGet("{id}/exists")]
        public async Task<IActionResult> ExistsById([FromRoute] int id) {
            bool exists = await userInfoService.ExistsById(id);
            return Ok(exists);
        }

        [HttpGet("username")]
        public async Task<IActionResult> GetByUsername(string username) {
            var userInfo = await userInfoService.GetByUsername(username);
            return Ok(userInfo);
        }

        [HttpGet("username/exists")]
        public async Task<IActionResult> ExistsByUsername(string username) {
            var userInfo = await userInfoService.ExistsByUsername(username);
            return Ok(userInfo);
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody] UserInfo userInfo) {
            await userInfoService.Insert(userInfo);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginWithCredentials([FromBody] CredentialsDto dto) {
            var validUser = await userInfoService.LoginWithCredentials(dto);
            if (validUser != null) {
                return Ok(validUser);
            }
            return StatusCode(401);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] UserInfo userInfo) {
            await userInfoService.Update(userInfo);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id) {
            await userInfoService.Delete(id);
            return Ok();
        }
    }
}
