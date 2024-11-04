using Microsoft.AspNetCore.Mvc;
using LibraryAPI.Models;
using LibraryAPI.Services.SBookLoan;

namespace LibraryAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class BookLoanController : ControllerBase {

        private readonly IBookLoanService bookLoanService;

        public BookLoanController(IBookLoanService bookLoanService) {
            this.bookLoanService = bookLoanService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll() {
            var roles = await bookLoanService.GetAll();
            if (roles == null) {
                return StatusCode(500);
            }
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id) {
            var roles = await bookLoanService.GetById(id);
            if (roles == null) {
                return StatusCode(500);
            }
            return Ok(roles);
        }

        [HttpGet("{id}/exists")]
        public async Task<IActionResult> ExistsById([FromRoute] int id) {
            bool exists = await bookLoanService.ExistsById(id);
            return Ok(exists);
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetAllByUserId(int userId) {
            var roles = await bookLoanService.GetAllByUserId(userId);
            if (roles == null) {
                return StatusCode(500);
            }
            return Ok(roles);
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody] BookLoan role) {
            await bookLoanService.Insert(role);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] BookLoan role) {
            await bookLoanService.Update(role);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id) {
            await bookLoanService.Delete(id);
            return Ok();
        }
    }
}
