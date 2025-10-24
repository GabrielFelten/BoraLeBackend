using BoraLe.Domain.Entities;

namespace BoraLe.Application.Interfaces
{
    public interface ICatalogService
    {
        Task<IEnumerable<Catalog>> ListCatalogAsync(List<string> Objectives, string genre = null, string title = null, string city = null);
    }
}
