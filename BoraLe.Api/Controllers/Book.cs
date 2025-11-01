using Microsoft.AspNetCore.Mvc;
using BoraLe.Application.Interfaces;
using BoraLe.Domain.Entities;

namespace BoraLe.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _service;
        public BookController(IBookService service) => _service = service;

        [HttpPost("UpsertBook")]
        public async Task<IActionResult> UpsertBook([FromBody] UpsertBook book)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage);

                var errorMessage = string.Join(" ", errors);

                return BadRequest(new { message = errorMessage });
            }

            await _service.UpsertBook(book);
            return Ok(new { message = "Livro salvo com sucesso!" });
        }

        [HttpGet("GetBookByUser")]
        public async Task<IEnumerable<BooksUser>> GetBooksByUser([FromQuery] string userId)
        {
            return await _service.GetBooksByUser(userId);
        }

        [HttpDelete("DeleteBook")]
        public async Task<IActionResult> DeleteBook([FromQuery] string bookId)
        {
            try
            {
                await _service.DeleteBookAsync(bookId);
                return Ok(new { message = "Livro deletado com sucesso!" });
            }
            catch (Exception ex) 
            {
                return BadRequest(new { message = "Erro ao deletar livro" });
            }
        }
    }    
}
