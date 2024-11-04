using Microsoft.AspNetCore.Mvc;
using LibraryAPI.Models;
using LibraryAPI.Services.SBook;
using System.Web.Http;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using HttpPutAttribute = Microsoft.AspNetCore.Mvc.HttpPutAttribute;
using HttpDeleteAttribute = Microsoft.AspNetCore.Mvc.HttpDeleteAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using Newtonsoft.Json;

namespace LibraryAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase {

        private readonly IBookService bookService;

        public BookController(IBookService bookService) {
            this.bookService = bookService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll() {
            var books = await bookService.GetAll();
            if (books == null) {
                return StatusCode(500);
            }
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id) {
            var books = await bookService.GetById(id);
            if (books == null) {
                return StatusCode(500);
            }
            return Ok(books);
        }

        [HttpGet("{id}/exists")]
        public async Task<IActionResult> ExistsById([FromRoute] int id) {
            bool exists = await bookService.ExistsById(id);
            return Ok(exists);
        }

        [HttpGet("{id}/available")]
        public async Task<IActionResult> IsAvailable([FromRoute] int id) {
            bool available = await bookService.IsAvailable(id);
            return Ok(available);
        }

        [HttpGet("browse")]
        public async Task<IActionResult> GetPaginated(int pageSize, int pageNumber) {
            var books = await bookService.GetPaginated(pageSize, pageNumber);
            return Ok(books);
        }


        [HttpGet("featured")]
        public async Task<IActionResult> GetFeatured() {
            var books = await bookService.GetFeatured();
            return Ok(books);
        }

        [HttpGet("total")]
        public async Task<IActionResult> GetTotal() {
            var books = await bookService.GetTotal();
            return Ok(books);
        }

        [HttpPost("batch")]
        public async Task<IActionResult> GetAll([FromBody] List<int> bookIds) {
            var books = await bookService.GetAll(bookIds);
            if (books == null) {
                return StatusCode(500);
            }
            return Ok(books);
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody] Book role) {
            await bookService.Insert(role);
            return Ok();
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search([FromBody] string query) {
            var books = await bookService.Search(query);
            if (books == null) {
                return StatusCode(500);
            }
            return Ok(books);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] Book role) {
            await bookService.Update(role);
            return Ok();
        }

        [HttpPut("{id}/available")]
        public async Task<IActionResult> Checkout([FromRoute] int id, bool available) {
            await bookService.SetAvailable(id, available);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id) {
            await bookService.Delete(id);
            return Ok();
        }
    }
}
