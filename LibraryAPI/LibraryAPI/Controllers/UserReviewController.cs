using Microsoft.AspNetCore.Mvc;
using LibraryAPI.Models;
using LibraryAPI.Services.SUserReview;

namespace LibraryAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UserReviewController : ControllerBase {

        private readonly IUserReviewService userReviewService;

        public UserReviewController(IUserReviewService userReviewService) {
            this.userReviewService = userReviewService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll() {
            var userReviews = await userReviewService.GetAll();
            if (userReviews == null) {
                return StatusCode(500);
            }
            return Ok(userReviews);
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetAllByUser(int userId) {
            var userReviews = await userReviewService.GetAllByUser(userId);
            if (userReviews == null) {
                return StatusCode(500);
            }
            return Ok(userReviews);
        }

        [HttpGet("book")]
        public async Task<IActionResult> GetAllByBook(int bookId) {
            var userReviews = await userReviewService.GetAllByBook(bookId);
            if (userReviews == null) {
                return StatusCode(500);
            }
            return Ok(userReviews);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id) {
            var userReview = await userReviewService.GetById(id);
            if (userReview == null) {
                return StatusCode(500);
            }
            return Ok(userReview);
        }

        [HttpGet("{id}/exists")]
        public async Task<IActionResult> ExistsById([FromRoute] int id) {
            bool exists = await userReviewService.ExistsById(id);
            return Ok(exists);
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody] UserReview role) {
            await userReviewService.Insert(role);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] UserReview role) {
            await userReviewService.Update(role);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id) {
            await userReviewService.Delete(id);
            return Ok();
        }
    }
}
