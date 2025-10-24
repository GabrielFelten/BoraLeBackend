using Microsoft.AspNetCore.Mvc;
using BoraLe.Application.Interfaces;
using BoraLe.Domain.Entities;

namespace BoraLe.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly ICatalogService _service;
        public CatalogController(ICatalogService service) => _service = service;

        [HttpGet("ListCatalogAsync")]
        public async Task<IEnumerable<Catalog>> ListCatalogAsync(
            [FromQuery] List<string> Objectives, 
            [FromQuery] string genre = null, 
            [FromQuery] string title = null, 
            [FromQuery] string city = null)
        {
            return await _service.ListCatalogAsync(Objectives, genre, title, city);
        }
    }
}
