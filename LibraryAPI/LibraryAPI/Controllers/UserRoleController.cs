using Microsoft.AspNetCore.Mvc;
using LibraryAPI.Models;
using LibraryAPI.Services.SUserRole;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase {

        private readonly IUserRoleService userRoleService;

        public UserRoleController(IUserRoleService userRoleService) {
            this.userRoleService = userRoleService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll() {
            var roles = await userRoleService.GetAll();
            if (roles == null) {
                return StatusCode(500);
            }
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) {
            var roles = await userRoleService.GetById(id);
            if (roles == null) {
                return StatusCode(500);
            }
            return Ok(roles);
        }

        [HttpGet("{id}/exists")]
        public async Task<IActionResult> ExistsById(int id) {
            bool exists = await userRoleService.ExistsById(id);
            return Ok(exists);
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody] UserRole role) {
            await userRoleService.Insert(role);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] UserRole role) {
            await userRoleService.Update(role);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            await userRoleService.Delete(id);
            return Ok();
        }
    }
}
